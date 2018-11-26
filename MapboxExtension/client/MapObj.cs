using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DG.Tweening;
using Mapbox.Examples;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "MapObj", menuName = "Installers/MapObj")]
public class MapObj : ScriptableObjectInstaller<MapObj>
{
  public MapId.Settings mapId;

  public override void InstallBindings()
  {
    Container.BindInstance(mapId);
  }
}

public class MapId
{
  private readonly Settings _settings;

  public MapId(Settings settings)
  {
    _settings = settings;
  }

  [Serializable]
  public class Settings
  {
    public string ip;
    public string datasetId;
    public string feature;
    public TextAsset globeStyle;
    public TextAsset outdoorStyle;
    public string tilesetId;
    public string token;

    public string globeId;
    public string outdoorId;
  }
}


public class GlobeMap : IInitializable, ITickable, IDisposable
{
  public Settings _setting;
  private ZoomMap.Settings _zoomMap;
  private RaycastHit hit;
  private ToZoomSignal _toZoomSignal;
  GlobeMap(ZoomMap.Settings zoomMap, Settings settings, ToZoomSignal toZoomSignal)
  {
    _setting = settings;
    _zoomMap = zoomMap;
    _toZoomSignal = toZoomSignal;
  }

  public void Initialize()
  {
    _toZoomSignal += ToZoom;
  }

  public void Tick()
  {
    if (_zoomMap.isGlobe)
    {
      if (_zoomMap.mapCamera.fieldOfView <= 58 && Input.GetAxis("Mouse ScrollWheel") > 0)
      {
        _toZoomSignal.Fire();
      }
      else if (_zoomMap.mapCamera.fieldOfView >= 100)
      {
        _setting.earth.SetActive(true);
        _setting.atmoshere.transform.localScale = new Vector3(1030, 1030, 1030);
      }
      else if (_zoomMap.mapCamera.fieldOfView < 100 && _zoomMap.mapCamera.fieldOfView > 60)
      {
        _setting.earth.SetActive(false);
        _setting.atmoshere.transform.localScale = new Vector3(1015, 1015, 1015);
      }
    }
  }

  void ToZoom()
  {
    Vector2 v = new Vector2(Screen.width / 2, Screen.height / 2);
    Vector2d v2 = new Vector2d();
    if (Physics.Raycast(Camera.main.ScreenPointToRay(v), out hit))
    {
      v2 = Conversions.GeoFromGlobePosition(hit.point, 1000);
    }
    _zoomMap.mapCamera.transform.localPosition = new Vector3(0, 200, 0);
    _zoomMap.mapCamera.transform.localEulerAngles = new Vector3(90, 0, 0);

    _zoomMap.zoomMap.gameObject.SetActive(true);
    _zoomMap.zoomMap.SetZoom(4);
    _zoomMap.zoomMap.Initialize(v2, 4);
    _zoomMap.globeMap.gameObject.SetActive(false);
    _zoomMap.isZoom = true;
    _zoomMap.isGlobe = false;
    _zoomMap.viewCamera.gameObject.SetActive(true);
    _zoomMap.mapCamera.fieldOfView = 60;
  }

  [Serializable]
  public class Settings
  {
    [Header("-------- earth --------")] public GameObject globeMap;
    public GameObject atmoshere;
    public GameObject earth;
  }

  public void Dispose()
  {
    _toZoomSignal -= ToZoom;
  }
}

public class ZoomChangeDSignal : Signal<ZoomChangeDSignal> { }
public class ToGlobalSignal : Signal<ToGlobalSignal> { }
public class ToZoomSignal : Signal<ToZoomSignal> { }
public class ZoomMap : IInitializable, ITickable, IDisposable
{
  private readonly Settings _settings;
  [Inject] private gRPCClient gRpcClient;
  [Inject] GlobalData.Settings _globalDataSettings;
  private ZoomChangeDSignal _zoomChangeDSignal;
  private ToGlobalSignal _toGlobalSignal;
  public ZoomMap(Settings settings, ZoomChangeDSignal zoomChangeDSignal, ToGlobalSignal toGlobalSignal)
  {
    _settings = settings;
    _zoomChangeDSignal = zoomChangeDSignal;
    _toGlobalSignal = toGlobalSignal;
  }
  int i = 0;
  public void Initialize()
  {
    GlobalVar.Instance.zoom = _settings.zoomMap.AbsoluteZoom;
    GlobalVar.Instance.currentZoom = _settings.zoomMap.AbsoluteZoom;

    _globalDataSettings.afterZoom = _globalDataSettings.afterZoom = _settings.zoomMap.AbsoluteZoom;
    #region client

    _settings.clearBtn.onClick.AddListener(() => { Mapbox.Unity.MapboxAccess.Instance.ClearCache(); });
    //_settings.DrawBtn.onClick.AddListener(() =>
    //{
    //    gRpcClient.OnClick();
    //});

    #endregion

    #region mapbox

    _settings.outdoorBtn.onClick.AddListener(() =>
    {
      _settings.zoomMap.GetComponentInChildren<ChangeStyle>().ChangeStyleInruntime("outdoor");
    });
    _settings.stateliteBtn.onClick.AddListener(() =>
    {
      _settings.zoomMap.GetComponentInChildren<ChangeStyle>().ChangeStyleInruntime("statelite");
    });

    #endregion

    #region MainUI

    _settings.to3DMapBtn.onClick.AddListener(() =>
    {
      _settings.webUI.gameObject.SetActive(false);
      _settings.mapboxPanel.gameObject.SetActive(true);
      _settings.zoomMap.gameObject.SetActive(true);
      _settings.zoomMap.GetComponentInChildren<QuadTreeTileProvider>().enabled = true;
    });

    _settings.to2DScheduleBtn.onClick.AddListener(() =>
    {
      _settings.webUI.gameObject.SetActive(true);
      _settings.mapboxPanel.gameObject.SetActive(false);
      //_settings.Mapbox.gameObject.SetActive(false);
    });

    #endregion

    _zoomChangeDSignal += ChangeDSignal;
    _toGlobalSignal += ToGloabl;


    bool is2dor3d = false;
    _settings.twoorThree.onClick.AddListener((() =>
    {
      if (is2dor3d)
      {
        _settings.twoorThree.GetComponent<Image>().sprite = _settings.D2;
        _globalDataSettings.is2or3 = false;
        _settings.viewCamera.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);
        _settings.viewCamera.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
      }
      else
      {
        _settings.twoorThree.GetComponent<Image>().sprite = _settings.D3;
        _zoomChangeDSignal.Fire();
      }
      is2dor3d = !is2dor3d;
      _globalDataSettings.is2or3 = is2dor3d;
    }));
  }

  void ChangeDSignal()
  {

    _globalDataSettings.is2or3 = true;
    _settings.viewCamera.transform.DOLocalRotate(new Vector3(-48 + 4f * (16 - _settings.zoomMap.AbsoluteZoom), 0, 0), 0.5f);
    _settings.viewCamera.transform.DOLocalMove(new Vector3(0, -96f + 8 * (16 - _settings.zoomMap.AbsoluteZoom), 72f - 6 * (16 - _settings.zoomMap.AbsoluteZoom)), 0.5f);

  }

  public void Setlatlon(Vector2d v2d)
  {
    _settings.zoomMap.SetCenterLatitudeLongitude(v2d);
  }

  [Serializable]
  public partial class Settings
  {
    [Header("-------- client --------")] public Button clearBtn;
    public Button DrawBtn;
  }

  public partial class Settings
  {
    [Header("-------- mapbox  --------")] public Button outdoorBtn;
    public Button stateliteBtn;
    public ReloadMap reloadMap;
    public Text centerLatTxt;
    public Text centerLonTxt;
    public Button twoorThree;
    public Sprite D2;
    public Sprite D3;
  }

  public partial class Settings
  {
    [Header("-------- panel  --------")]
    public RectTransform mapboxPanel;
    public RectTransform drawLinePanel;
    public RectTransform mainUI;
  }

  public partial class Settings
  {
    [Header("-------- MapboxObj  --------")]
    public AbstractMap zoomMap;
    public AbstractMap globeMap;
    public Camera mapCamera;
    public Camera viewCamera;
  }


  public partial class Settings
  {
    [Header("-------- to3DMap  --------")]
    public Button to3DMapBtn;
    public Button to2DScheduleBtn;
    public RectTransform webUI;
  }

  public partial class Settings
  {
    [Header("-------- control --------")]
    public bool isGlobe;
    public bool isZoom;
  }

  public void Tick()
  {
    _settings.centerLatTxt.text =
        _settings.zoomMap.CenterLatitudeLongitude.x.ToString();
    _settings.centerLonTxt.text =
        _settings.zoomMap.CenterLatitudeLongitude.y.ToString();

    if ((_settings.zoomMap.Zoom - 4) < Constants.EpsilonFloatingPoint && _settings.isGlobe == false)
    {
      _toGlobalSignal.Fire();
    }
  }

  void ToGloabl()
  {
    _settings.zoomMap.gameObject.SetActive(false);

    Vector2d zoomcenter = _settings.zoomMap.CenterLatitudeLongitude;
    _settings.globeMap.Initialize(zoomcenter, 4);
    _settings.globeMap.gameObject.SetActive(true);

    GameObject instance = new GameObject("center");
    instance.transform.position = Conversions.GeoToWorldGlobePosition(zoomcenter, 1000);

    instance.transform.rotation = Quaternion.FromToRotation(Vector3.up, instance.transform.position);
    instance.transform.localScale = Vector3.one * 10;
    instance.transform.SetParent(_settings.globeMap.transform);


    _settings.mapCamera.transform.position = instance.transform.position;
    _settings.mapCamera.transform.localEulerAngles = instance.transform.localEulerAngles;

    _settings.mapCamera.transform.position += _settings.mapCamera.transform.up * 400;
    _settings.mapCamera.transform.LookAt(instance.transform);
    Object.Destroy(instance);
    _settings.isGlobe = true;
    _settings.isZoom = false;
    _settings.viewCamera.gameObject.SetActive(false);
  }

  public void Dispose()
  {
    _zoomChangeDSignal -= ChangeDSignal;
    _toGlobalSignal -= ToGloabl;
  }
}

public class GlobalPrefab
{
  private Settings _settings;

  GlobalPrefab(Settings settings)
  {
    _settings = settings;
  }

  [Serializable]
  public class Settings
  {
    public GameObject noteItem;
    public GameObject nztPrefab;
    public GameObject zxtPrefab;
  }
}

public class GlobalData
{
  private Settings _settings;

  GlobalData(Settings settings)
  {
    _settings = settings;
  }

  [Serializable]
  public class Settings
  {
    public TowerInfosList towerInfosList;
    public string projectName;
    public string featureId;
    public GameObject loadingUI;
    public bool is2or3;
    public int preZoom;
    public int afterZoom;

  }
}