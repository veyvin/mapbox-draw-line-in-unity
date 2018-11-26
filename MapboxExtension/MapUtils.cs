using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUtils  {

    public void CleanCache()
    {
        Mapbox.Unity.MapboxAccess.Instance.ClearCache();
    }
}
