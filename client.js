//测试用的,客户端在unity端
//
// var messages = require('./BCMapBox_pb');
// var services = require('./BCMapBox_grpc_pb');
//
// var grpc = require('grpc');
//
// function main() {
//     var client = new services.BCMapClient('localhost:9008',
//         grpc.credentials.createInsecure());
//     var request = new messages.MapRequest();
//      //var user;
//     // if (process.argv.length >= 3) {
//     //     user = process.argv[2];
//     // } else {
//     //     token = '1';
//     //     datasetId = '2';
//     //     tilesetId = '3';
//     //     styleId = '4';
//     // }
//     request.setToken('123');
//     client.trans(request, function (err, response) {
//         console.log('Greeting:', response.getMessage());
//     });
// }
//
// main();
