using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using UnityEngine;
using UnityEngine.UI;

public class ChangeZoom : MonoBehaviour
{
    public AbstractMap _mapController;
  
    public void ChangeZoomInruntime()
    {
       // _mapController._mapVisualizer.DisposeAllTiles();
       _mapController.Initialize(_mapController.CenterLatitudeLongitude,8);
        //_mapController.Zoom = 8;
        //_mapController.Start();
        //_mapController._mapVisualizer.LoadAllTiles();
    }

    void OnGUI()
    {
        
        if (GUILayout.Button("�ı�zoom."))
            ChangeZoomInruntime();
    }
    //public void ChangeZoomWithSlider()
    //{
    //    int num = 6 + (Convert.ToInt32(_slider.value) - 1) * 2;
    //    _mapController._mapVisualizer.DisposeAllTiles();
        
    //    _mapController.Zoom = num;
 
    //    _mapController.Start();
    //    _mapController._mapVisualizer.LoadAllTiles();
       
    //}
    void Update()
    {
        Debug.Log(_mapController.CenterLatitudeLongitude.x);
    }
}
