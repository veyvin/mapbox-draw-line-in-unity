using System.Collections;
using System.Collections.Generic;
using CsvHelper.Configuration;
using Mapbox.Utils;
using UnityEngine;

public  class TowerInfo
{
    public int id { get; set; }
    public string tower_code { get;set; }
    public Vector2d latlon { get; set; }
    public string type { get; set; }
    public int towerId { get; set; }
    public int towerState { get; set; }
    public int jichu { get; set; }
    public int zuta { get; set; }
    public int jiaxian { get; set; }
}
