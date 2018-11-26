using System.Collections.Generic;
using BCMapBox;
using Grpc.Core;
using UnityEngine.UI;
using Zenject;


public class gRPCClient
{
  [Inject] private MapId.Settings map;
  [Inject] private GlobalData.Settings globalDataSettings;

  private MapReply reply;
  // Use this for initialization

  public void TransData()
  {
    Mapbox.Unity.MapboxAccess.Instance.ClearCache();
    var channel = new Channel(map.ip, ChannelCredentials.Insecure);

    var client = new BCMap.BCMapClient(channel);

    var datasetId = map.datasetId;
    var tilesetId = map.tilesetId;
    var token = map.token;
    var globe_style = MapConf.json_style_globe;
    var feature = MapConf.json_feature;
    var globeId = map.globeId;
    var ModififeatureId = globalDataSettings.featureId;
    //var outdoorId = map.outdoorId;
    //var outdoor_style = MapConf.json_style_outdoor;
    reply = client.TransToken(
        new MapRequestToken
        {
          Token = token
        });

    //如果返回字符串 "token",继续下一步操作,发送feature
    if (reply.Message == "token")
    {
      globalDataSettings.loadingUI.GetComponentInChildren<Text>().text = "向nodejs传输token完成";
      reply = client.TransFeature(
        new MapRequestFeature
        {
          Feature = feature
        });
    }
    //发送完feature,发送datasetid
    if (reply.Message == "feature" && ModififeatureId != null)
    {
      globalDataSettings.loadingUI.GetComponentInChildren<Text>().text = "向nodejs传输feature完成";
      reply = client.TransFeatureId(new MapRequestFeatureId
      {
        FeatureId = ModififeatureId
      });
    }
    if (reply.Message == "featureId" || (reply.Message == "feature" && ModififeatureId == null))
    {
      globalDataSettings.loadingUI.GetComponentInChildren<Text>().text = "向nodejs传输featureId完成";
      reply = client.TransDataSetId(
        new MapRequestDatasetId
        {
          DatasetId = datasetId
        });
    }

    //发送完datasetid,发送tilesetid
    if (reply.Message.Contains("datasetid"))
    {
      string featureId = reply.Message.Replace("datasetid", "");
      globalDataSettings.loadingUI.GetComponentInChildren<Text>().text = "向nodejs传输datasetid完成";
      //向数据库提交featureId
      InsertProjectFeatureId(featureId);
      reply = client.TransTilesetId(
        new MapRequestTilesetId
        {
          TilesetId = tilesetId
        });
    }

    //发送完tilesetid,发送 globeid

    if (reply.Message == "tilesetid")
    {
      globalDataSettings.loadingUI.GetComponentInChildren<Text>().text = "向nodejs传输tilesetid完成";
      reply = client.TransGlobeId(
        new MapRequestGlobeId
        {
          GlobeId = globeId
        });
    }
    //发送完globeid,发送globestyle
    if (reply.Message == "globeid")
    {
      globalDataSettings.loadingUI.GetComponentInChildren<Text>().text = "向nodejs传输globeid完成";
      reply = client.TransGlobeStyle(
        new MapRequestGlobeStyle
        {
          GlobeStyle = globe_style
        });
    }
    //发送完globestyle,加载完成
    if (reply.Message == "globestyle")
    {
      globalDataSettings.loadingUI.GetComponentInChildren<Text>().text = "向nodejs传输globestyle完成";
      globalDataSettings.loadingUI.SetActive(false);
      UnityEngine.Debug.Log("传输完成,一分钟后再开始加载地图");
      //reply = client.TransOutdoorId(
      //  new MapRequestOutdoorId
      //  {
      //    OutdoorId = outdoorId
      //  });
    }
    ////发送完outdoorid,发送outdoorstyle
    //if (reply.Message == "outdoorid")
    //{
    //  reply = client.TransOutdoorStyle(
    //    new MapRequestOutdoorStyle
    //    {
    //      OutdoorStyle = outdoor_style
    //    });
    //}

    ////发送完成
    //if (reply.Message == "outdoorstyle")
    //{
    //  UnityEngine.Debug.Log("over");
    //}

    channel.ShutdownAsync().Wait();

  }

  /// <summary>
  /// 插入featureId
  /// </summary>
  /// <param name="featureId"></param>
  public void InsertProjectFeatureId(string featureId)
  {
    Dictionary<string, string> dict = new Dictionary<string, string>();
    dict.Add("project_id", MapConf.projectId);
    dict.Add("featureId", featureId);
    ServerController.Instance.QueryDataByFrom(ServerController.UrlDictionary[MyEnum.QueryType.InsertProjectFeatureId], dict, MyEnum.QueryType.InsertProjectFeatureId);
  }
}