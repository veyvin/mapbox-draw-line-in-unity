using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using CsvHelper;
using Mapbox.Map;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Modifiers;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;
using Zenject;

public class PutNZTTowerFactory : GameObjectFactory
{
}
public class PutZXTTowerFactory : GameObjectFactory
{
}
public class PutTower : MonoBehaviour
{

  //铁塔保存的经纬度坐标
  private List<Vector2d> latlon = new List<Vector2d>();

  public List<TowerInfo> towers = new List<TowerInfo>();
  public List<TowerItem> towerList = new List<TowerItem>();
  public AbstractMap _mapController;

  [Inject] private GlobalData.Settings _globalsettings;

  //    [Inject] private PutTowerFactory _putTowerFactory;
  //
  //    PutTower(PutTowerFactory putTowerFactory)
  //    {
  //        _putTowerFactory = putTowerFactory;
  //    }

  public List<GameObject> ActivetowerList = new List<GameObject>();
  private int i = 14;
  private bool isLoad;

  private void Start()
  {

  }

  public void PutTowerInMap()
  {
    int idnum = 0;
    // int towerIndex=0;
    towers.Clear();
    ActivetowerList.Clear();
    towerList.Clear();
    foreach (var VARIABLE in _globalsettings.towerInfosList.data)
    {

      TowerInfo towerInfo = new TowerInfo();

      ++idnum;
      towerInfo.id = idnum;
      towerInfo.tower_code = VARIABLE.tower_code;
      towerInfo.type = VARIABLE.tower_type;
      towerInfo.latlon = new Vector2d(VARIABLE.tower_position_x, VARIABLE.tower_position_y);
      towerInfo.towerId = VARIABLE.towerId;
      towerInfo.towerState = VARIABLE.towerStatus;
      towerInfo.jichu = VARIABLE.jichu;
      towerInfo.zuta = VARIABLE.zuta;
      towerInfo.jiaxian = VARIABLE.jiaxian;
      towers.Add(towerInfo);
    }

    isLoad = true;
  }


  void Update()
  {
    if (isLoad)
    {

      if (_mapController.AbsoluteZoom == 16)
      {
        if (i != _mapController.AbsoluteZoom)
        {
          foreach (var item in towers)
          {
            Destroy(ActivetowerList.Find((GameObject s) => s.GetComponent<TowerID>().id == item.id));
            ActivetowerList.Remove(ActivetowerList.Find((GameObject s) =>
                s.GetComponent<TowerID>().id == item.id));
          }
        }
        i = _mapController.AbsoluteZoom;
        foreach (var item in towers)
        {
          Vector2d r = Conversions.LatitudeLongitudeToTileId(item.latlon.x, item.latlon.y,
              _mapController.AbsoluteZoom);


          bool flag = true;
          foreach (var VARIABLE in ActivetowerList)
          {
            if (VARIABLE.GetComponent<TowerID>().id == item.id)
            {
              flag = false;
            }
          }
          foreach (var item1 in transform.GetComponentsInChildren<Transform>())
          {
            if (item1.GetComponent<MeshCollider>() == null)
            {
              if (item1.tag == "Tile")
              {
                item1.gameObject.AddComponent<MeshCollider>();
              }
            }
          }
          if (_mapController.MapVisualizer.ActiveTiles.ContainsKey(new UnwrappedTileId(_mapController.AbsoluteZoom,
                  (int)r.x,
                  (int)r.y)) && flag)
          {


            Debug.Log("在范围内实例化");
            //在范围内实例化
            GameObject go;
            StartCoroutine(WaiforTower());
            if (item.type == "NZT")
            {
              go = PutObj.CreateBuildMap(_mapController, "nzt",
                  item.latlon, _mapController.AbsoluteZoom);
            }
            else
            {
              go = PutObj.CreateBuildMap(_mapController, "zxt",
                  item.latlon, _mapController.AbsoluteZoom);

            }
            go.AddComponent<TowerID>().id = item.id;
            go.GetComponent<TowerID>().towerId = item.towerId;
            go.GetComponent<TowerID>().towerState = item.towerState;
            go.GetComponent<TowerID>().jichu = item.jichu;
            go.GetComponent<TowerID>().zuta = item.zuta;
            go.GetComponent<TowerID>().jiaxian = item.jiaxian;
            ActivetowerList.Add(go);
          }

          if (!_mapController.MapVisualizer.ActiveTiles.ContainsKey(new UnwrappedTileId(_mapController.AbsoluteZoom,
                  (int)r.x,
                  (int)r.y)) && !flag)
          {

            Debug.Log("不在范围内,删除");

            Destroy(ActivetowerList.Find((GameObject s) => s.GetComponent<TowerID>().id == item.id));
            ActivetowerList.Remove(ActivetowerList.Find((GameObject s) =>
                s.GetComponent<TowerID>().id == item.id));
            //删除不在范围内的hudUI
            //Destroy(HUDController.Instance.UHD_UIList.Find((GameObject s) => s.GetComponent<CircleItem>().HUD_ID == item.id));
            //HUDController.Instance.UHD_UIList.Remove(HUDController.Instance.UHD_UIList.Find((GameObject s) =>
            // s.GetComponent<CircleItem>().HUD_ID == item.id));

          }

        }


      }
      else
      {
        foreach (var item in towers)
        {
          Destroy(ActivetowerList.Find((GameObject s) => s.GetComponent<TowerID>().id == item.id));
          ActivetowerList.Remove(ActivetowerList.Find((GameObject s) =>
              s.GetComponent<TowerID>().id == item.id));
        }
      }

    }

  }

  IEnumerator WaiforTower()
  {
    yield return new WaitForEndOfFrame();
  }
}