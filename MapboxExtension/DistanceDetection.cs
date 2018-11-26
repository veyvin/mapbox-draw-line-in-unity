using Mapbox.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DistanceDetection  {
    public static int ManhattanTo(this Vector2 v, Vector2 t)
    {
        return (int)Math.Abs(v.x - t.x) + (int)Math.Abs(v.y - t.y);
    }

    public static int ManhattanTo(this Vector2d v, Vector2d t)
    {
        return (int)Math.Abs(v.x - t.x) + (int)Math.Abs(v.y - t.y);
    }

}
