using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Mapbox.Json;
using UnityEngine;

public class TileLayer
{
    [JsonProperty("tilejson")]
    public string tilejson { get; set; }

   
    [JsonProperty("name")]
    public string name { get; set; }

    
    [JsonProperty("description")]
    public string description { get; set; }

  
    [JsonProperty("version")]
    public string version { get; set; }

    
    [JsonProperty("attribution")]
    public string attribution { get; set; }

    
    [JsonProperty("template")]
    public string template { get; set; }

    [JsonProperty("legend")]
    public string legend { get; set; }

    
    [JsonProperty("scheme")]
    public string scheme { get; set; }

    [JsonProperty("tiles")]
    public IList<string> tiles { get; set; }

    
    [JsonProperty("grids")]
    public IList<string> grids { get; set; }

   
    [JsonProperty("data")]
    public IList<string> data { get; set; }

    [JsonProperty("minzoom")]
    public int minzoom { get; set; }

    [JsonProperty("maxzoom")]
    public int maxzoom { get; set; }

    [JsonProperty("bounds")]
    public IList<double> bounds { get; set; }

    [JsonProperty("center")]
    public IList<double> center { get; set; }
}
