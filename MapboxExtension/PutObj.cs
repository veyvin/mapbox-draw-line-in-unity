using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration;
using Mapbox.Unity.MeshGeneration.Data;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using ModestTree;
using UnityEngine;
using Zenject;

public static class PutObj
{
    [Inject] private static ZoomMap.Settings zoomSettings;
    public static GameObject CreateBuildMap(AbstractMap _mapController, string prefabname, Vector2d latlon, Vector3 rotation, Vector3 scale)
    {

        GameObject go1 = (GameObject)Resources.Load(prefabname);
        GameObject parent = GameObject.Find("Map");
        GameObject go = UnityEngine.Object.Instantiate(go1, parent.transform);
        go.transform.position = Conversions.GeoToWorldPosition(latlon,

            _mapController.CenterMercator,
            _mapController.WorldRelativeScale).ToVector3xz();
        go.transform.localEulerAngles = rotation;
        go.transform.localScale = scale;

        return go;
    }

    public static GameObject CreateBuildMap(AbstractMap _mapController, string prefabname, Vector2d latlon,int zoom)
    {
        GameObject go1 = (GameObject)Resources.Load(prefabname);

        GameObject parent = GameObject.Find("ZoomMap");
        GameObject go = UnityEngine.Object.Instantiate(go1, parent.transform);


        Vector3 v3 = Conversions.GeoToWorldPosition(latlon,
                                     _mapController.CenterMercator, _mapController.WorldRelativeScale).ToVector3xz();

        var mecter = Conversions.LatLonToMeters(latlon);
        var ss = Conversions.MetersToTilePosotion(mecter, zoom);
        var x = Mathf.CeilToInt(ss.x) - ss.x;
        var y = Mathf.CeilToInt(ss.y)-ss.y;
        //GameObject.Find("ZoomMap")

        IEnumerable<UnityTile> unityTiles = from tilename in parent.GetComponentsInChildren<UnityTile>()
            where tilename.name == zoom+ "/" + Mathf.CeilToInt(ss.x) + "/" + Mathf.CeilToInt(ss.y)
                                            select tilename;
        var unityTile = unityTiles.FirstOrDefault();

        float height = 0;
        if (unityTile != null)
        {
             height=unityTile.QueryHeightData((float)x, (float)y);
        }

        //float beishu = 1 / _mapController.WorldRelativeScale;
        go.transform.position = new Vector3(v3.x , v3.y, v3.z);
        return go;

    }
}
