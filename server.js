//流程=======>features添加到dataset,datasets添加到tileset,tileset添加到style.
//我们的style就是地图的样式,
//样式中由很多tileset组成
//每个tileset有时由很多dataset组成
//dataset由很多feature组成
//目前mapboxsdk 还不完善, js能够调用的最多,但也不全.比如将dataset添加到tileset的函数就没有.(可能以后会添加)
//但是python cli中有这个功能函数,我又用nodejs调用的python cli.
//所以服务机器需要有nodejs和python3环境.(这些环境会在打包成安装程序的时候加入)(如果以后js端添加了这个功能,可以把python去掉)
//因为没有向style添加tileset的方法.所以需要事先在map stdio上手动添加.(以后说不定也会加上这个方法,现在tileset的方法在官网还是preview)

var bcmap = require('./BCMap');
var messages = require('./BCMapBox_pb');
var services = require('./BCMapBox_grpc_pb');
var grpc = require('grpc');
//这些数据都是 从C#端传输过来的
// var token = 'sk.eyJ1IjoidmV5dmluIiwiYSI6ImNqMWc2aG1zeTAwMDcycXFuY3NneTNrdWYifQ.x6iZEnvBPZKvMA2cM4nt-A';
// var datasetId = 'cj7a2zlv0087y2wo8e6ld9g2c';
// var tilesetId = 'veyvin.data';
// var styleId = 'cj6fvezqi2w3q2spg2brumwvd';
var token;
var datasetId;
var tilesetId;
var globeId;
var globe_style;
// var outdoorId;
// var outdoor_style;
var featureId;
var feature;

/**
 * GRPC.
 */
var reply


function TransToken(call, callback) {
    reply = new messages.MapReply();

    var promise = new Promise(function (resovle, reject) {
        resovle(token = call.request.getToken()
        );
    }).then(function () {
        bcmap.ReadToken(token)
    }).then(function () {
        reply.setMessage("token");
        callback(null, reply);
    });
}

function TransFeature(call, callback) {
    reply = new messages.MapReply();

    var promise = new Promise(function (resovle, reject) {
        resovle(feature = call.request.getFeature()
        );
        if (typeof feature === 'string') {
            feature = JSON.parse(feature);
        }
    }).then(function () {
        reply.setMessage("feature");
        callback(null, reply);
    });
}
function TransFeatureId(call,callback){
    reply=new messages.MapReply();
    var promise=new Promise(function (resolve,reject) {
        resolve(featureId=call.request.getFeatureid())
    }).then(function () {
        reply.setMessage("featureId");
        callback(null,reply);
    });
}

function TransDataSetId(call, callback) {
    reply = new messages.MapReply();

    var promise = new Promise(function (resovle, reject) {
        resovle(datasetId = call.request.getDatasetid()

        );
    })
    //     .then(function () {
    //     return new Promise(function (resovle, reject) {
    //         resovle(bcmap.ListFeatures(datasetId));
    //     });
    // })
        .then(function () {
        if(featureId!=null){
            bcmap.DeleteFeature(featureId,datasetId);
        }
    })
    //     .then(function (value) {
    //     for (var keys in value.entity.features) {
    //         //删除所有特性
    //         console.log('删除所有特征');
    //         bcmap.DeleteFeature(value.entity.features[keys].id, datasetId);
    //     }
    // })
        .then(function () {
        return new Promise(function (resolve, reject) {
            for (var i = 0; i < feature.features.length; i++) {
                resolve(bcmap.InsertFeature(datasetId, feature.features[i]));
            }
        });
    }).then(function (value) {
        reply.setMessage("datasetid"+value["entity"]["id"]);
        callback(null, reply);
    });
}

function TransTilesetId(call, callback) {
    reply = new messages.MapReply();

    var promise = new Promise(function (resovle, reject) {
        resovle(tilesetId = call.request.getTilesetid()
        );
    }).then(function () {
        bcmap.InsertTileSet(token, datasetId, tilesetId);
    }).then(function () {
        reply.setMessage("tilesetid");
        callback(null, reply);
    });
}

function TransGlobeId(call, callback) {
    reply = new messages.MapReply();

    var promise = new Promise(function (resovle, reject) {
        resovle(globeId = call.request.getGlobeid()
        );
    }).then(function () {
        reply.setMessage("globeid");
        callback(null, reply);
    });
}

function TransGlobeStyle(call, callback) {
    reply = new messages.MapReply();

    var promise = new Promise(function (resovle, reject) {
        resovle(globe_style = call.request.getGlobestyle()
        );
        globe_style = JSON.parse(globe_style);
    }).then(function () {
        new Promise(function (resovle, reject) {
            resovle(bcmap.UpdateStyle(globe_style, globeId));
            console.log("更新globe完成");
        });
    }).then(function () {
        reply.setMessage("globestyle");
        callback(null, reply);
    });
}
//
// function TransOutdoorId(call, callback) {
//     reply = new messages.MapReply();
//
//     var promise = new Promise(function (resovle, reject) {
//         resovle(outdoorId = call.request.getOutdoorid()
//         );
//     }).then(function () {
//         reply.setMessage("outdoorid");
//         callback(null, reply);
//     });
// }
//
// function TransOutdoorStyle(call, callback) {
//     reply = new messages.MapReply();
//
//     var promise = new Promise(function (resovle, reject) {
//         resovle(outdoor_style = call.request.getOutdoorstyle(),
//         );
//         outdoor_style = JSON.parse(outdoor_style);
//     }).then(function () {
//         new Promise(function (resovle, reject) {
//             resovle(bcmap.UpdateStyle(outdoor_style, outdoorId));
//             console.log("更新outdoor完成");
//         });
//     }).then(function () {
//         reply.setMessage("outdoorstyle");
//         callback(null, reply);
//     });
// }

/**
 * 启动服务
 */
function mainTest() {
    var server = new grpc.Server();
    server.addService(services.BCMapService, {
        transToken: TransToken,
        transFeature: TransFeature,
        transDataSetId: TransDataSetId,
        transTilesetId: TransTilesetId,
        transGlobeId: TransGlobeId,
        transGlobeStyle: TransGlobeStyle,
        transFeatureId:TransFeatureId,
        // transOutdoorId: TransOutdoorId,
        // transOutdoorStyle: TransOutdoorStyle,
    });
    server.bind('0.0.0.0:9008', grpc.ServerCredentials.createInsecure());
    server.start();
}


mainTest();
