using System.Collections;
using System.Collections.Generic;
using Mapbox.Json;
using Mapbox.Utils;

using UnityEngine;
using Zenject;

public class MapConf
{
  // public TextAsset txt_style;
  public static string json_feature;

  public static string json_style_globe;
  public static string json_style_outdoor;
 
  public static string projectId;

  [Inject] MapId.Settings map;
  [Inject] private GlobalData.Settings globalSettings;
  private Features features;


  public void FeaturesJson()
  {
    JsonSerializerSettings jsetting = new JsonSerializerSettings();
    jsetting.NullValueHandling = NullValueHandling.Ignore;
    json_feature = JsonConvert.SerializeObject(features, Formatting.Indented, jsetting);
  }


  public Features FeatureJson(List<List<Vector2d>> fsList)
  {
    features = new Features();
    features.features = new List<Feature>();

    foreach (var item in fsList)
    {
      Feature feature = new Feature();
      feature.type = "Feature";
      Properties properties = new Properties();
      //properties.stroke = "#555555";
      //properties.stroke_width = 5;
      //properties.stroke_opacity = 5;
      //properties.fill = "#aa0000";
      //properties.stroke_opacity = 0.5;
      properties.name = globalSettings.projectName;
      feature.properties = properties;
      Geometry geometry = new Geometry();
      geometry.type = "LineString";
      geometry.coordinates = new List<object>();
      foreach (var VARIABLE in item)
      {
        List<double> coor = new List<double>();
        coor.Add(VARIABLE.x);
        coor.Add(VARIABLE.y);
        geometry.coordinates.Add(coor);
      }
      feature.geometry = geometry;
      features.features.Add(feature);
    }
    
    features.type = "FeatureCollection";


    return features;
  }

  public void StyleJson()
  {
    Style style = new Style();
    style.id = "data";
    style.type = "line";
    Metadata metadata = new Metadata();
    style.metadata = metadata;
    style.source = "mapbox://veyvin.data";
    style.source_layer = "data";
    style.minzoom = 3;
    style.maxzoom = 18;
    Layout layout = new Layout();
    layout.visibility = "visible";
    style.layout = layout;
    Paint paint = new Paint();
    paint.line_blur = 0;
    paint.line_color = "hsl(0, 0%, 0%)";
    paint.line_offset = 0;
    paint.line_width = 5;
    int[] a = {100, 0};
    paint.line_dasharray = a;
    paint.line_opacity = 1;
    int[] b = {0, 0};
    paint.line_translate = b;
    paint.line_gap_width = 0;
    style.paint = paint;

    JsonSerializerSettings jsetting = new JsonSerializerSettings();
    jsetting.NullValueHandling = NullValueHandling.Ignore;

    string json2 = JsonConvert.SerializeObject(style, jsetting);
    json_style_globe = map.globeStyle.text.Replace("##########", json2);
    json_style_outdoor= map.outdoorStyle.text.Replace("##########", json2);
  }
}
