using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoateGlobe : MonoBehaviour {
    public Camera MainCamera;
    public float ZoomMin;      //滚轮的最小值
    public  float ZoomMax;      //滚轮的最大值
    private float normalDistance;   //设置摄像机的景深值
    private float MouseWheelSencitivity = 10.0f;    //鼠标灵敏度,就是缩放的速度的快慢
 
    private float axisX;
    private float axisY;
    public float speed = 6f;
    private float tempSpeed;
 
    private bool RoationOnly;


    void Start () 
    {
        normalDistance =32.0f;
        ZoomMin = 20.0f;
        ZoomMax = 100.0f;
        RoationOnly = true;
    }
     
     
    void Update () 
    {
        //Roation();
        Zoom();
        this.transform.Rotate(new Vector3(axisY, axisX, 0) * Rigid(), Space.World);     //物体旋转的方法
    }
 
 
    //自动旋转物体的方法，放在Update中调用
    void Roation()
    {
        if (RoationOnly)
        {
            gameObject.transform.Rotate(Vector3.up * Time.deltaTime * 10);
        }
    }
 
    /****
    *鼠标滚轮缩放物体的方法
     * 
     * **/
    void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (normalDistance >= ZoomMin && normalDistance <= ZoomMax)
            {
                normalDistance -= Input.GetAxis("Mouse ScrollWheel") * MouseWheelSencitivity;
            }
            if (normalDistance < ZoomMin)
            {
                normalDistance = ZoomMin;
            }
            if (normalDistance > ZoomMax)
            {
                normalDistance = ZoomMax;
            }
 
            MainCamera.fieldOfView = normalDistance;

        }
    }
  /***
   * 
   * 鼠标左键控制物体360°旋转+惯性
   * **/
    float Rigid()
    {
        if (Input.GetMouseButton(0))
        {
            RoationOnly = false;    //当鼠标按下的时候，停止自动旋转
 
            axisX = Input.GetAxis("Mouse X");
            axisY = Input.GetAxis("Mouse Y");
            if (tempSpeed < speed)
            {
                tempSpeed -= speed * Time.deltaTime * 5;
            }
            else
            {
                tempSpeed = speed;
            }
        }
        else
        {
            RoationOnly = true;     //当鼠标没有按下的时候，恢复自动旋转
            if (tempSpeed > 0)
            {
                tempSpeed += speed * Time.deltaTime;
            }
            else
            {
                tempSpeed = 0;
            }
        }
        return tempSpeed;
    }
}
