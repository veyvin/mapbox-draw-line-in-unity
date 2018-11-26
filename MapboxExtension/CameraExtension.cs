using UnityEngine;
using System.Collections;
using Mapbox.Map;
using Mapbox.Unity;
using Mapbox.Unity.Map;

using Mapbox.Utils;
using UnityEngine.UI;
public class CameraExtension : MonoBehaviour
{
  // -------------------------- Configuration --------------------------
  //slippy移动速度
  public float panSpeed = 20f;
  //缩放速度
  public float zoomSpeed = 50.0f;
  //旋转速度
  public float rotationSpeed = 50.0f;
  //乘的数 
  public float mousePanMultiplier = 0.1f;
  public float mouseRotationMultiplier = 0.2f;
  public float mouseZoomMultiplier = 1;
  //缩放的距离
  public float minZoomDistance = 25f;
  public float maxZoomDistance = 1000f;
  public float smoothingFactor = 1000f;
  public float goToSpeed = 1000f;
  //是否使用键盘输入
  public bool useKeyboardInput = false;
  //是否使用鼠标输入
  public bool useMouseInput = true;
  //todo:适应地形高度
  public bool adaptToTerrainHeight = false;
  //增加缩小速度
  public bool increaseSpeedWhenZoomedOut = false;
  //正确缩小比例
  public bool correctZoomingOutRatio = false;
  //平滑处理
  public bool smoothing = false;

  //是否允许图像边缘移动
  public bool allowScreenEdgeMovement = false;
  //距离边界的距离
  public int screenEdgeSize = 10;
  //边缘移动的速度
  public float screenEdgeSpeed = 1.0f;
  //跟随的目标,暂时没用
  //public GameObject objectToFollow;
  public Vector3 cameraTarget;

  //当前相机距离地面的距离
  private float currentCameraDistance;

  //上一次鼠标的位置
  private Vector3 lastMousePos;
  //todo:是否自动移动
  private bool doingAutoMovement = false;

  //是否允许地图级别缩放
  public bool isZoomlevel;



  void Start()
  {

    currentCameraDistance = 0;
    lastMousePos = Vector3.zero;
  }

  void Update()
  {
    UpdateRotation();
    UpdatePosition();
    lastMousePos = Input.mousePosition;
  }

  private void UpdateRotation()
  {
    float deltaAngleH = 0.0f;
    float deltaAngleV = 0.0f;

    if (useKeyboardInput)
    {
      if (Input.GetKey(KeyCode.Q))
      {
        deltaAngleH = 1.0f;
      }
      if (Input.GetKey(KeyCode.E))
      {
        deltaAngleH = -1.0f;
      }
    }

    if (useMouseInput)
    {
      if (Input.GetMouseButton(2) && !Input.GetKey(KeyCode.LeftShift))
      {
        Vector3 deltaMousePos = Input.mousePosition - lastMousePos;
        deltaAngleH += deltaMousePos.x * mouseRotationMultiplier;
        deltaAngleV -= deltaMousePos.y * mouseRotationMultiplier;
      }
    }
    transform.localEulerAngles = new Vector3(Mathf.Min(80.0f, Mathf.Max(25f, transform.localEulerAngles.x + deltaAngleV * Time.deltaTime * rotationSpeed)), transform.localEulerAngles.y + deltaAngleH * Time.deltaTime * rotationSpeed, transform.localEulerAngles.z);
  }

  private void UpdatePosition()
  {
    transform.localPosition = cameraTarget;

    transform.Translate(Vector3.up * currentCameraDistance);
  }

 
}