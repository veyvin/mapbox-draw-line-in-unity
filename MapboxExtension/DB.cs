using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LiteDB;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration;
using Mapbox.Utils;
using UnityEngine;

public static class DB
{
    public static void SaveDB(string name, Vector2d lanlon, Vector3 rotation, Vector3 scale)
    {
        string strLanLon = lanlon.x + "|" + lanlon.y;
        string strRotation = rotation.x + "|" + rotation.y + "|" + rotation.z;
        string strScale = scale.x + "|" + scale.y + "|" + scale.z;

        using (var db = new LiteDatabase(Path.Combine(Application.streamingAssetsPath, "mylitedb.db")))
        {
            var placements = db.GetCollection<Placement>("placements");

            var placement = new Placement()
            {
                Name = name,
                Latlon = strLanLon,
                Rotation = strRotation,
                Scale = strScale
            };
            placements.Insert(placement);

            var result = placements.FindAll();

            foreach (var VARIABLE in result)
            {
                Debug.Log(VARIABLE.Name);
                Debug.Log(VARIABLE.Latlon);
                Debug.Log(VARIABLE.Rotation);
                Debug.Log(VARIABLE.Scale);
            }
        }
    }

    //创建物体
    public static void SelectDB(AbstractMap _mapController)
    {
        using (var db = new LiteDB.LiteDatabase(Path.Combine(Application.streamingAssetsPath, "mylitedb.db")))
        {
            var placements = db.GetCollection<Placement>("placements");
            var result = placements.FindAll();

            foreach (var VARIABLE in result)
            {
                Debug.Log(VARIABLE.Name);
                string[] l = SplitStr(VARIABLE.Latlon);
                string[] r = SplitStr(VARIABLE.Rotation);
                string[] s = SplitStr(VARIABLE.Scale);
                Vector2d v2D = new Vector2d(Convert.ToDouble(l[0]), Convert.ToDouble(l[1]));
                Vector3 rotation = new Vector3(Convert.ToSingle(r[0]), Convert.ToSingle(r[1]), Convert.ToSingle(r[2]));
                Vector3 scale = new Vector3(Convert.ToSingle(s[0]), Convert.ToSingle(s[1]), Convert.ToSingle(s[2]));
                PlaceMentSerial placeMentSerial = new PlaceMentSerial()
                {
                    Name = VARIABLE.Name,
                    Latlon = v2D,
                    Rotation = rotation,
                    Scale = scale
                };

                PutObj.CreateBuildMap(_mapController, VARIABLE.Name, v2D, rotation, scale);
                Debug.Log(MapData.Placements);
                MapData.Placements.Add(placeMentSerial);
                foreach (var VARIABLE1 in MapData.Placements)
                {
                    Debug.Log(VARIABLE1.Name);
                    Debug.Log(VARIABLE1.Rotation);
                    Debug.Log(VARIABLE1.Scale);
                }
            }
        }
    }
    /// <summary>
    /// 在litedb数据库中删除collection 表
    /// </summary>
    /// <param name="litedb">数据库</param>
    /// <param name="collection">表</param>
    static void DropCollection(LiteDatabase litedb, string collection)
    {
        litedb.DropCollection(collection);
    }

    //分割字符串
    static string[] SplitStr(string str)
    {
        string[] s = str.Split('|');
        return s;
    }
}
