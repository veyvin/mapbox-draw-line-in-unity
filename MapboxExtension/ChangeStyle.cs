using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using UnityEngine;

public class ChangeStyle : MonoBehaviour
{

    private AbstractMap _mapController;
    public Camera camera;

    void Start()
    {
        
        _mapController = FindObjectOfType<AbstractMap>();
    }
    public void ChangeStyleInruntime(string type)
    {
        GlobalVar.Instance.zoom = _mapController.AbsoluteZoom;
        // _mapController._mapVisualizer.DisposeAllTiles();
        switch (type)
        {
            case "statelite":
               
                ((MapImageFactory)_mapController.MapVisualizer.Factories[1]).MapId =
                    "mapbox://styles/veyvin/cj9ds1com7agm2rs1n1wkqool";
                //_mapController.SetZoom(); = 14;
                
                break;
            case "outdoor":
                ((MapImageFactory)_mapController.MapVisualizer.Factories[1]).MapId =
                    "mapbox://styles/veyvin/cj3l76yyb00032rqo9dvckmte";

                break;
        }
        //要改
        Camera.main.transform.position = camera.transform.position+new Vector3(0,0.00001f,0);
        _mapController.Initialize(_mapController.CenterLatitudeLongitude, _mapController.AbsoluteZoom);
      
        //_mapController._mapVisualizer.LoadAllTiles();

    }

}

