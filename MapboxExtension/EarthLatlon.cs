using System.Collections;
using System.Collections.Generic;
using Mapbox.Map;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.UI;

public class EarthLatlon : MonoBehaviour
{

    AbstractMap _map;
    public Camera cam;
    Vector2d _currentLatitudeLongitude;
    public Text latlon;
    void Start()
    {
        _map = FindObjectOfType<AbstractMap>();
    }
    void Update()
    {
        RaycastHit hit;
        Ray _ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out hit))
        {
            float xPos = hit.point.x;
            float yPos = hit.point.y;
            float zPos = hit.point.z;
            float radius = Mathf.Sqrt(Mathf.Pow(xPos, 2) + Mathf.Pow(yPos, 2) + Mathf.Pow(zPos, 2));
            //纬线
            float latitude = Mathf.Asin(yPos / radius);
            //经线
            float longitude = Mathf.Atan2(zPos, xPos);
            latlon.text = "纬度" + (latitude * Mathf.Rad2Deg) + "经度" + (180 + Mathf.Rad2Deg * longitude);

           // Debug.Log("xpos" + xPos + "ypos" + yPos + "zpos" + zPos + "纬度" + (latitude * Mathf.Rad2Deg) + "经度" + (180+Mathf.Rad2Deg * longitude));
        }
    }
}
