using System;
using System.Collections.Generic;
using Mapbox.Json;

public class MapStyle
{
  public int version { get; set; }
  public string name { get; set; }
  public DateTime created { get; set; }
  public string id { get; set; }
  public DateTime modified { get; set; }
  public string owner { get; set; }
  public string visibility { get; set; }
  public IList<double> center { get; set; }
  public double? zoom { get; set; }
  public int? bearing { get; set; }
  public int? pitch { get; set; }
}

public class TowerLatLon
{
  [JsonProperty("lat")]
  public double Lat { get; set; }
  [JsonProperty("lon")]
  public double Lon { get; set; }
}

public class ListTowerLatlon
{
  [JsonProperty("data")]
  public List<TowerLatLon> latlons { get; set; }
  [JsonProperty("projectId")]
  public string projectId { get; set; }
}