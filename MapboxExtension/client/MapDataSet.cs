using System.Collections.Generic;
using Mapbox.Json;


public class Geometry
{

    [JsonProperty("type")]
    public string type { get; set; }

    [JsonProperty("coordinates")]
    public IList<object> coordinates { get; set; }
}


public class Properties
{
  [JsonProperty("name")]
  public string name { get; set; }

    [JsonProperty("title")]
    public string title { get; set; }

    [JsonProperty("description")]
    public string description { get; set; }

    [JsonProperty("marker-size")]
    public string marker_size { get; set; }

    [JsonProperty("marker-symbol")]
    public string marker_symbol { get; set; }

    [JsonProperty("marker-color")]
    public string marker_color { get; set; }

    [JsonProperty("stroke")]
    public string stroke { get; set; }

    [JsonProperty("stroke-opacity")]
    public double? stroke_opacity { get; set; }

    [JsonProperty("stroke-width")]
    public int? stroke_width { get; set; }

    [JsonProperty("fill")]
    public string fill { get; set; }

    [JsonProperty("fill-opacity")]
    public double? fill_opacity { get; set; }
}

public class Feature
{

    [JsonProperty("type")]
    public string type { get; set; }

    [JsonProperty("geometry")]
    public Geometry geometry { get; set; }

    [JsonProperty("properties")]
    public Properties properties { get; set; }
}

public class Features
{
    [JsonProperty("features")]
    public List<Feature> features { get; set; }
    [JsonProperty("type")]
    public string type { get; set; }
}

enum FeaturesType
{
    POINT,
    MULTIPOINT,
    LINESTRING,
    MULTILINESTRING,
    POLYGON,
    MULTIPOLYGON,
    GEOMETRYCOLLECTION
}