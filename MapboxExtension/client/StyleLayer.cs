using System.Collections;
using System.Collections.Generic;
using Mapbox.Json;
using UnityEngine;

public class Metadata
{
}

public class Layout
{

    [JsonProperty("visibility")]
    public string visibility { get; set; }
}

public class Paint
{

    [JsonProperty("line-blur")]
    public int line_blur { get; set; }

    [JsonProperty("line-color")]
    public string line_color { get; set; }

    [JsonProperty("line-offset")]
    public int line_offset { get; set; }

    [JsonProperty("line-width")]
    public int line_width { get; set; }

    [JsonProperty("line-dasharray")]
    public IList<int> line_dasharray { get; set; }

    [JsonProperty("line-opacity")]
    public int line_opacity { get; set; }

    [JsonProperty("line-translate")]
    public IList<int> line_translate { get; set; }

    [JsonProperty("line-gap-width")]
    public int line_gap_width { get; set; }
}

public class Style
{

    [JsonProperty("id")]
    public string id { get; set; }

    [JsonProperty("type")]
    public string type { get; set; }

    [JsonProperty("metadata")]
    public Metadata metadata { get; set; }

    [JsonProperty("source")]
    public string source { get; set; }

    [JsonProperty("source-layer")]
    public string source_layer { get; set; }

    [JsonProperty("minzoom")]
    public int minzoom { get; set; }

    [JsonProperty("maxzoom")]
    public int maxzoom { get; set; }

    [JsonProperty("layout")]
    public Layout layout { get; set; }

    [JsonProperty("paint")]
    public Paint paint { get; set; }
}

