//流程=======>features添加到dataset,datasets添加到tileset,tileset添加到style.
//我们的style就是地图的样式,
//样式中由很多tileset组成
//每个tileset有时由很多dataset组成
//dataset由很多feature组成
//目前mapboxsdk 还不完善, js能够调用的最多,但也不全.比如将dataset添加到tileset的函数就没有.(可能以后会添加)
//但是python cli中有这个功能函数,我又用nodejs调用的python cli.
//所以服务机器需要有nodejs和python3环境.(这些环境会在打包成安装程序的时候加入)(如果以后js端添加了这个功能,可以把python去掉)
//因为没有向style添加tileset的方法.所以需要事先在map stdio上手动添加.(以后说不定也会加上这个方法,现在tileset的方法在官网还是preview)

var MapboxClient = require('mapbox');
var async = require('async');
//python 安装mapboxcli 模块,node调用cli,更新tileset
var cmd = require('node-cmd');

function ReadToken(token) {
    return client = new MapboxClient(token);
}

// var style = {
//   'version': 8,
//   'name': 'My Awesome Style',
//   'metadata': {},
//   'sources': {},
//   'layers': [],
//   'glyphs': 'mapbox://fonts/mapbox/{fontstack}/{range}.pbf'
// };
/**
 * @method 创建style
 */
function CreateStyle() {
    client.createStyle(style, function (err, createdStyle) {
        console.log(createdStyle);
    });
}

/**
 * @method ReadStyle
 */
function ReadStyle(styleId) {
    client.readStyle(styleId, function (err, style) {
        if (!err) console.log(style);
    });
}

/**
 * @method style列表
 */
function ListStyle() {
    client.listStyles(function (err, styles) {
        console.log(styles);
        // [{ version: 8,
        //  name: 'Light',
        //  center: [ -77.0469979435026, 38.898634927602814 ],
        //  zoom: 12.511766533145998,
        //  bearing: 0,
        //  pitch: 0,
        //  created: '2016-02-09T14:26:15.059Z',
        //  id: 'STYLEID',
        //  modified: '2016-02-09T14:28:31.253Z',
        //  owner: '{username}' },
        //  { version: 8,
        //  name: 'Dark',
        //  created: '2015-08-28T18:05:22.517Z',
        //  id: 'STYILEID',
        //  modified: '2015-08-28T18:05:22.517Z',
        //  owner: '{username}' }]
    });
}

/**
 * @method 删除style
 * cj6em0dc01pnu2rpjwu4x3c6s
 */
function DeleteStyle(styleId) {
    client.deleteStyle(styleId, function (err) {
        if (!err) console.log('deleted!');
    });
}

/**
 * @method 嵌入style
 * cj6fvezqi2w3q2spg2brumwvd
 */
function EmbedStyle(styleId) {
    var url = client.embedStyle(styleId);
    console.log(url);
}


/**
 * @method UpdateStyle
 */
function UpdateStyle(style, styleId) {
    return(client.updateStyle(style, styleId, function (err, createdStyle) {

        console.log(createdStyle);

    }));
}


/**
 * @method 创建临时token
 */
function CreateTemporaryToken() {
    client.createToken('2016-09-15T19:27:53.000Z', ["styles:read", "fonts:read"], function (err, createdToken) {
        console.log(createdToken);
    });
}

/**
 * @method 创建token
 */
function CreateToken() {
    client.createToken('My top secret project', ["styles:read", "fonts:read"], function (err, createdToken) {
        console.log(createdToken);
    });
}

/**
 * @method RetrieveToken
 */
function RetrieveToken(token) {
    client.retrieveToken(token, function (err, tokenResponse) {
        console.log(tokenResponse);
    });
}

/**
 * @method UpdateTokenAuthorization
 */
function UpdateTokenAuthorization(authId) {
    client.updateTokenAuthorization(authId, ["styles:read", "fonts:read"], function (err, updatedToken) {
        console.log(updatedToken);
    });
}

/**
 * @method 删除授权token
 * cj1g6hmsy00072qqncsgy3kuf
 */
function DeleteTokenAuthorization(authId) {
    client.deleteTokenAuthorization(authId, function (err) {
        console.log(err);
    });
}

/**
 * @method ListTokens
 */
function ListTokens() {
    client.listTokens(function (err, tokens) {
        console.log(tokens);
        // [{ client: 'api'
        //  note: 'Default Public Token',
        //  usage: 'pk',
        //  id: 'TOKENID',
        //  default: true,
        //  scopes: ['styles:tiles','styles:read','fonts:read','datasets:read'],
        //  created: '2016-02-09T14:26:15.059Z',
        //  modified: '2016-02-09T14:28:31.253Z',
        //  token: 'pk.TOKEN' }]
    });
}


/**
 * @method 创建数据集
 */
function CreateDataset() {
    client.createDataset({name: 'foo', description: 'bar'}, function (err, dataset) {
        console.log(dataset);
        // {
        //   "owner": {account},
        //   "id": {dataset id},
        //   "name": "foo",
        //   "description": "description",
        //   "created": {timestamp},
        //   "modified": {timestamp}
        // }
    });
}

/**
 * @method 删除数据集
 */
function DeleteDataset(datasetId) {
    client.deleteDataset(datasetId, function (err) {
        if (!err) console.log('deleted!');
    });
}


/**
 * @method UpdateDataset
 */
function UpdateDataset(datasetId) {
    return(client.updateDataset(datasetId, options, function (err, dataset) {
        console.log(dataset);

        // {
        //   "owner": {account},
        //   "id": "dataset-id",
        //   "name": "foo",
        //   "description": {dataset description},
        //   "created": {timestamp},
        //   "modified": {timestamp}
        // }
    }));
}

/**
 * @method 删除数据集
 */
function ReadDataset(datasetId) {
    client.readDataset(datasetId, function (err, dataset) {
        console.log(dataset);
        // {
        //   "owner": {account},
        //   "id": "dataset-id",
        //   "name": {dataset name},
        //   "description": {dataset description},
        //   "created": {timestamp},
        //   "modified": {timestamp}
        // }
    });
}

/**
 * @method 数据集列表
 */
function ListDatasets() {
    client.listDatasets(function (err, datasets) {
        console.log(datasets);
        // [
        //   {
        //     "owner": {account},
        //     "id": {dataset id},
        //     "name": {dataset name},
        //     "description": {dataset description},
        //     "created": {timestamp},
        //     "modified": {timestamp}
        //   },
        //   {
        //     "owner": {account},
        //     "id": {dataset id},
        //     "name": {dataset name},
        //     "description": {dataset description},
        //     "created": {timestamp},
        //     "modified": {timestamp}
        //   }
        // ]
    });
}


/**
 * @method 删除特征
 */
function DeleteFeature(featureId, datasetId) {
    return(client.deleteFeature(featureId, datasetId, function (err, feature) {
        if (!err) console.log('deleted!');
    }));
}

/**
 * 特征集列表
 * // {
        //   "type": "FeatureCollection",
        //   "features": [
        //     {
        //       "id": {feature id},
        //       "type": "Feature",
        //       "properties": {feature properties}
        //       "geometry": {feature geometry}
        //     },
        //     {
        //       "id": {feature id},
        //       "type": "Feature",
        //       "properties": {feature properties}
        //       "geometry": {feature geometry}
        //     }
        //   ]
        // }
 */
function ListFeatures(datasetId) {
    return (client.listFeatures(datasetId, function (err, collection) {
        console.log(collection);
    }));
}

/**
 * @method ReadFeature
 */
function ReadFeature(featureId, datasetId) {
    client.readFeature(featureId, datasetId, function (err, feature) {
        console.log(feature);
        // {
        //   "id": "feature-id",
        //   "type": "Feature",
        //   "properties": {
        //     "name": "Null Island"
        //   },
        //   "geometry": {
        //     "type": "Point",
        //     "coordinates": [0, 0]
        //   }
        // }
    });
}


/**
 * @method InsertFeature
 *     // var feature = {
    //           "type": "Feature",
    //           "geometry": {
    //               "type": "Polygon",
    //               "coordinates": [
    //                   [
    //                       100.0,
    //                       102.0
    //                   ],
    //                   [
    //                       100.0,
    //                       102.0
    //                   ]
    //               ]
    //           },
    //           "properties": {
    //               "stroke": "#555555",
    //               "stroke-opacity": 0.5,
    //               "stroke-width": 5,
    //               "fill": "#aa0000",
    //               "fill-opacity": 0.0
    //           }
    // };
 // 'cj7a2zlv0087y2wo8e6ld9g2c'
 */
function InsertFeature(datasetId, feature) {
    return(client.insertFeature(feature, datasetId, function (err, features) {
        console.log("sdasd");
        console.log(features);
        console.log('插入特征');

    }));
}


/**
 * @method GeocodeForward
 */
function GeocodeForward() {
    mapboxClient.geocodeForward('Paris, France', function (err, res) {
        console.log(res);
        // res is a GeoJSON document with geocoding matches
    });
    // using the proximity option to weight results closer to texas
    mapboxClient.geocodeForward('Paris, France', {
        proximity: {latitude: 33.6875431, longitude: -95.4431142}
    }, function (err, res) {
        console.log(res);
        // res is a GeoJSON document with geocoding matches
    });
    // using the bbox option to limit results to a portion of Washington, D.C.
    mapboxClient.geocodeForward('Starbucks', {
        bbox: [-77.083056, 38.908611, -76.997778, 38.959167]
    }, function (err, res) {
        console.log(res);
        // res is a GeoJSON document with geocoding matches
    });
}

/**
 * @method GeocodeReverse
 */
function GeocodeReverse() {

    mapboxClient.geocodeReverse(
        {latitude: 33.6875431, longitude: -95.4431142},
        function (err, res) {
            console.log(res);
            // res is a GeoJSON document with geocoding matches
        });
}

/**
 * @method GetDirections
 */
function GetDirections() {
    mapboxClient.getDirections(
        [
            {latitude: 33.6, longitude: -95.4431},
            {latitude: 33.2, longitude: -95.4431}],
        function (err, res) {
            console.log(res);
            // res is a document with directions
        });

    // With options
    mapboxClient.getDirections([
        {latitude: 33.6875431, longitude: -95.4431142},
        {latitude: 33.6875431, longitude: -95.4831142}
    ], {
        profile: 'walking',
        alternatives: false,
        geometry: 'polyline'
    }, function (err, results) {
        console.log(results);
    });
}

/**
 * @method GetMatrix
 */
function GetMatrix() {
    mapboxClient.getMatrix([{
        longitude: -122.42,
        latitude: 37.78
    },
        {
            longitude: -122.45,
            latitude: 37.91
        },
        {
            longitude: -122.48,
            latitude: 37.73
        }], {}, function (err, results) {
        console.log(results);
    });

    // With options
    mapboxClient.getMatrix([{
        longitude: -122.42,
        latitude: 37.78
    },
        {
            longitude: -122.45,
            latitude: 37.91
        },
        {
            longitude: -122.48,
            latitude: 37.73
        }], {profile: 'walking'}, {}, function (err, results) {
        console.log(results);
    });
}

/**
 * @method GetStaticURL
 */
function GetStaticURL() {
    var url = mapboxClient.getStaticURL('mapbox', 'streets-v10', 600, 400, {
        longitude: 151.22,
        latitude: -33.87,
        zoom: 11
    }, {
        markers: [{longitude: 151.22, latitude: -33.87}],
        before_layer: 'housenum-label'
    });
}

/**
 * @method GetTilestats
 */
function GetTilestats(tilesetId) {
    client.getTilestats(tilesetId, function (err, info) {
        console.log(info);
        // {
        //   "layerCount": {layer count},
        //   "layers": [
        //     {
        //       "layer": {layer name},
        //       "geometry": {dominant geometry},
        //       "count": {feature count},
        //       "attributeCount": {attribute count}
        //       "attributes": [
        //         {
        //           "attribute": {attribute name},
        //           "type": {attribute type},
        //           "count": {unique value count},
        //           "min": {minimum value if type is number},
        //           "max": {maximum value if type is number},
        //           "values": [{...unique values}]
        //         }
        //       ]
        //     }
        //   ]
        // }
    });
}

/**
 * @method ListTilesets
 */
function ListTilesets() {
    client.listTilesets(function (err, tilesets) {
        console.log(tilesets);
    });
}

/**
 * @method PutTilestats
 */
function PutTilestats(titlesetId) {

    client.putTilestats(titlesetId, function (err, stats) {
        console.log(stats);
        // {
        //   "account": {account}
        //   ... see stats example above (for Tilestats#getTilestats)
        // }
    });
}

/**
 * @method Tilequery
 */
function Tilequery(mapId) {
    client.tilequery(mapId, [-77, 38], {}, function (err, response) {
        console.log(response);
    });
}

/**
 * @method ListScopes
 */
function ListScopes() {
    client.listScopes(function (err, scopes) {
        console.log(scopes);
    });
}

/**
 * @method Matching
 */
function Matching() {
    mapboxClient.matching([
        [-95.4431142, 33.6875431],
        [-95.0431142, 33.6875431],
        [-95.0431142, 33.0875431],
        [-95.0431142, 33.0175431],
        [-95.4831142, 33.6875431]
    ], {
        overview: 'full'
    }, function (err, res) {
        console.log(res);
        // res is a match response object
    });
}

/**
 * @method ReadFontGlyphRanges
 */
function ReadFontGlyphRanges() {
    client.readFontGlyphRanges('Arial Unicode', 0, 255, function (err, ranges) {
        if (!err) console.log(ranges);
    });
}

/**
 *@method ReadSprite
 */
function ReadSprite(styleId) {
    client.readSprite(styleId, {
        retina: true
    }, function (err) {
        if (!err) console.log('deleted!');
    });
}

//添加环境变量 或者改成 mapbox --access-token  TOKEN datasets create-tileset datasetid tileid
function InsertTileSet(token,datasetId, tilesetId) {
    var s ='mapbox --access-token '+token+ ' datasets create-tileset ' + datasetId + ' ' + tilesetId;

    cmd.get(s, function (err, data, stderr) {
        console.log('插入Tileset');
        console.log(data);
    });
}


module.exports.ReadToken = ReadToken;
module.exports.createToken = CreateToken;
module.exports.CreateTemporaryToken = CreateTemporaryToken;
module.exports.DeleteTokenAuthorization = DeleteTokenAuthorization;
module.exports.ListTokens = ListTokens;
module.exports.RetrieveToken = RetrieveToken;
module.exports.UpdateTokenAuthorization = UpdateTokenAuthorization;

module.exports.CreateStyle = CreateStyle;
module.exports.DeleteStyle = DeleteStyle;
module.exports.EmbedStyle = EmbedStyle;
module.exports.ListStyle = ListStyle;
module.exports.ReadStyle = ReadStyle;
module.exports.UpdateStyle = UpdateStyle;

module.exports.GetTilestats = GetTilestats;
module.exports.ListTilesets = ListTilesets;
module.exports.PutTilestats = PutTilestats;
module.exports.Tilequery = Tilequery;
module.exports.InsertTileSet = InsertTileSet;
module.exports.CreateDataset = CreateDataset;
module.exports.DeleteDataset = DeleteDataset;
module.exports.ListDatasets = ListDatasets;
module.exports.ReadDataset = ReadDataset;
module.exports.UpdateDataset = UpdateDataset;

module.exports.DeleteFeature = DeleteFeature;
module.exports.InsertFeature = InsertFeature;
module.exports.ListFeatures = ListFeatures;
module.exports.ReadFeature = ReadFeature;

module.exports.GeocodeForward = GeocodeForward;
module.exports.GeocodeReverse = GeocodeReverse;
module.exports.GetDirections = GetDirections;
module.exports.GetMatrix = GetMatrix;
module.exports.GetStaticURL = GetStaticURL;
module.exports.ReadFontGlyphRanges = ReadFontGlyphRanges;
module.exports.ReadSprite = ReadSprite;

module.exports.ListScopes = ListScopes;
module.exports.Matching = Matching;

// //这些数据都是 从C#端传输过来的
// var token = 'sk.eyJ1IjoidmV5dmluIiwiYSI6ImNqMWc2aG1zeTAwMDcycXFuY3NneTNrdWYifQ.x6iZEnvBPZKvMA2cM4nt-A';
// var datasetId = 'cj7a2zlv0087y2wo8e6ld9g2c';
// var tilesetId = 'veyvin.data';
// var styleId = 'cj6fvezqi2w3q2spg2brumwvd';
// var options = {name: 'foo'};

// var style =
// {
//   "version": 8,
//   "name": "globe",
//   "metadata": {
//       "mapbox:autocomposite": true,
//       "mapbox:type": "default",
//       "mapbox:groups": {
//           "e0c63d22cd4ea376f4bd78e6334da9c0": {
//               "name": "Group"
//           }
//       }
//   },
//   "center": [
//       89.87702401694287,
//       27.537305273309414
//   ],
//   "zoom": 3.4005633456882483,
//   "bearing": 0,
//   "pitch": 0,
//   "light": {
//       "color": "hsl(0, 0%, 100%)",
//       "anchor": "map"
//   },
//   "sources": {
//       "mapbox": {
//           "url": "mapbox://mapbox.satellite",
//           "type": "raster",
//           "tileSize": 256
//       },
//       "composite": {
//           "url": "mapbox://mapbox.mapbox-streets-v7",
//           "type": "vector"
//       }
//   },
//   "sprite": "mapbox://sprites/veyvin/cj6ecwjk60vh42rljp72r6r6r",
//   "glyphs": "mapbox://fonts/veyvin/{fontstack}/{range}.pbf",
//   "layers": [
//       {
//           "id": "background",
//           "type": "background",
//           "paint": {
//               "background-color": "rgb(4,7,14)"
//           }
//       },
//       {
//           "id": "satellite",
//           "type": "raster",
//           "source": "mapbox",
//           "source-layer": "mapbox_satellite_full"
//       },
//       {
//           "id": "admin",
//           "type": "line",
//           "metadata": {
//               "mapbox:group": "e0c63d22cd4ea376f4bd78e6334da9c0"
//           },
//           "source": "composite",
//           "source-layer": "admin",
//           "minzoom": 1,
//           "filter": [
//               "all",
//               [
//                   "==",
//                   "admin_level",
//                   2
//               ],
//               [
//                   "==",
//                   "disputed",
//                   0
//               ],
//               [
//                   "==",
//                   "maritime",
//                   0
//               ]
//           ],
//           "layout": {
//               "visibility": "visible"
//           },
//           "paint": {
//               "line-width": 2
//           }
//       },
//       {
//           "id": "country-label",
//           "type": "symbol",
//           "metadata": {
//               "mapbox:group": "e0c63d22cd4ea376f4bd78e6334da9c0"
//           },
//           "source": "composite",
//           "source-layer": "country_label",
//           "minzoom": 1,
//           "maxzoom": 6,
//           "filter": [
//               "in",
//               "scalerank",
//               1,
//               2
//           ],
//           "layout": {
//               "visibility": "visible",
//               "text-field": "{name_zh}",
//               "text-font": [
//                   "DIN Offc Pro Medium",
//                   "Arial Unicode MS Regular"
//               ],
//               "text-max-width": {
//                   "base": 1,
//                   "stops": [
//                       [
//                           0,
//                           5
//                       ],
//                       [
//                           3,
//                           6
//                       ]
//                   ]
//               },
//               "text-size": 20
//           },
//           "paint": {
//               "text-color": "hsl(0, 0%, 0%)",
//               "text-halo-color": "rgb(255,255,255)",
//               "text-halo-width": 1.25
//           }
//       }
//   ],
//   "id": "cj6ecwjk60vh42rljp72r6r6r",
//   "owner": "veyvin",
//   "visibility": "public",
//   "draft": false
// };
//     {
//         "version": 8,
//         "name": "New Style Name",
//         "metadata": {},
//         "sources": {
//             "mapbox": {
//                 "url": "mapbox://mapbox.satellite",
//                 "type": "raster",
//                 "tileSize": 256
//             },
//             "composite": {
//                 "url": "mapbox://mapbox.mapbox-streets-v7,veyvin.data",
//                 "type": "vector"
//             }
//         },
//         "sprite": "mapbox://sprites/veyvin/veyvin.data",
//         "glyphs": "mapbox://fonts/veyvin/{fontstack}/{range}.pbf",
//         "layers": [{
//             "id": "data",
//             "type": "line",
//             "source": "composite",
//             "source-layer": "data",
//             "layout": {
//                 "visibility": "visible"
//             },
//             "paint": {}
//         }],
//         "owner": "veyvin",
//         "visibility": "public",
//         "draft": true
//     };
// //
// // //token
// function fn1() {
//     var p = new Promise(function (resovle, reject) {
//         setTimeout(function () {
//             resovle(ReadToken(token));
//         });
//     });
//     return p;
// }
//
// function fn2() {
//     var p = new Promise(function (resovle, reject) {
//         resovle(ListFeatures(datasetId));
//     });
//     return p;
// }
//
// function fn3() {
//     var p = new Promise(function (resovle, reject) {
//         //进行中--》完成
//         //进行中--> 失败
//         resovle('3');
//
//     });
//     return p;
// }
//
// fn1()
//     .then(function (data) {
//         console.log(data);
//         return fn2();
//     })
//     .then(function (data2) {
//         for (var keys in data2.entity.features) {
//             //删除所有特性
//             console.log(data2.entity.features[keys].id);
//         }
//         return fn3();
//     })
//     .then(function (data3) {
//         console.log(data3);
//
//     });
// // Promise.all([fn1(),fn2()]).then(function (result) {
// //     console.log(result);
// // })

// var map = async.auto({
//     //读取token
//     readToken: function () {
//         ReadToken(token);
//     },
//     //首先遍历dataset下所有的feature;
//     listFeatures: function (callback) {
//
//         //callback(null,bcmap.ListFeatures(datasetId));
//
//         // ListFeatures(datasetId);
//         callback(null, function (callbck) {
//             callbck(null,ListFeatures(datasetId));
//         });
//
//     },
//     //首先删除所有feature..===为什么先全部删除,考虑到某些塔的位置有问题,在unity中微调了,而mapbox目前并没有提供更新的某个feature的函数.故先删除全部的,再添加所需要的
//     deleteFeature: ['listFeatures', function (results, callback) {
//         console.log(JSON.stringify(results));
//         //根据遍历的所有featureId
//         //bcmap.DeleteFeature(featureId,datasetId);
//     }],
//     //向dataset中添加feature
//     insertFeature: function () {
//         //bcmap.InsertFeature(datasetId, feature);
//     },
//     //向tileset添加dataset
//     insertDataset: function () {
//         //bcmap.InsertTileSet(datasetId, tilesetId);
//     },
//     updateStyle: function () {
//         // bcmap.UpdateStyle(style, styleId);
//     }
// }, function (err, result) {
//
//     console.log(result);
// });

//
// //读取所有的datasets
// function () {
//     bcmap.ListDatasets();
// }
// function test() {
//
// }
//
// test();
//创建dataset
//var dataset=bcmap.CreateDataset();gei
//cj7a2zlv0087y2wo8e6ld9g2c
//添加feature
//bcmap.InsertFeature();
//e48518774d724345ae1f20661da707a1
//添加到tileset
// bcmap.PutTilestats();
//添加到style更新
//bcmap.GetTilestats();

//bcmap.UpdateStyle();
//bcmap.ReadStyle();