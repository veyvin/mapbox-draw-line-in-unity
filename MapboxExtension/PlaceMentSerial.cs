using System;
using Mapbox.Utils;
using UnityEngine;

[Serializable]
public class PlaceMentSerial
{
    [SerializeField] private GameObject go;
    [SerializeField] private string name;
    [SerializeField] private Vector2d latlon;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Vector3 scale;

    public string Name
    {
        get { return name; }

        set { name = value; }
    }

    public Vector2d Latlon
    {
        get { return latlon; }

        set { latlon = value; }
    }

    public Vector3 Rotation
    {
        get { return rotation; }

        set { rotation = value; }
    }

    public Vector3 Scale
    {
        get { return scale; }

        set { scale = value; }
    }

    public GameObject Go
    {
        get { return go; }

        set { go = value; }
    }
}