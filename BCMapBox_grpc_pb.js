// GENERATED CODE -- DO NOT EDIT!

'use strict';
var grpc = require('grpc');
var BCMapBox_pb = require('./BCMapBox_pb.js');

function serialize_BCMapBox_MapReply(arg) {
  if (!(arg instanceof BCMapBox_pb.MapReply)) {
    throw new Error('Expected argument of type BCMapBox.MapReply');
  }
  return new Buffer(arg.serializeBinary());
}

function deserialize_BCMapBox_MapReply(buffer_arg) {
  return BCMapBox_pb.MapReply.deserializeBinary(new Uint8Array(buffer_arg));
}

function serialize_BCMapBox_MapRequestDatasetId(arg) {
  if (!(arg instanceof BCMapBox_pb.MapRequestDatasetId)) {
    throw new Error('Expected argument of type BCMapBox.MapRequestDatasetId');
  }
  return new Buffer(arg.serializeBinary());
}

function deserialize_BCMapBox_MapRequestDatasetId(buffer_arg) {
  return BCMapBox_pb.MapRequestDatasetId.deserializeBinary(new Uint8Array(buffer_arg));
}

function serialize_BCMapBox_MapRequestFeature(arg) {
  if (!(arg instanceof BCMapBox_pb.MapRequestFeature)) {
    throw new Error('Expected argument of type BCMapBox.MapRequestFeature');
  }
  return new Buffer(arg.serializeBinary());
}

function deserialize_BCMapBox_MapRequestFeature(buffer_arg) {
  return BCMapBox_pb.MapRequestFeature.deserializeBinary(new Uint8Array(buffer_arg));
}

function serialize_BCMapBox_MapRequestFeatureId(arg) {
  if (!(arg instanceof BCMapBox_pb.MapRequestFeatureId)) {
    throw new Error('Expected argument of type BCMapBox.MapRequestFeatureId');
  }
  return new Buffer(arg.serializeBinary());
}

function deserialize_BCMapBox_MapRequestFeatureId(buffer_arg) {
  return BCMapBox_pb.MapRequestFeatureId.deserializeBinary(new Uint8Array(buffer_arg));
}

function serialize_BCMapBox_MapRequestGlobeId(arg) {
  if (!(arg instanceof BCMapBox_pb.MapRequestGlobeId)) {
    throw new Error('Expected argument of type BCMapBox.MapRequestGlobeId');
  }
  return new Buffer(arg.serializeBinary());
}

function deserialize_BCMapBox_MapRequestGlobeId(buffer_arg) {
  return BCMapBox_pb.MapRequestGlobeId.deserializeBinary(new Uint8Array(buffer_arg));
}

function serialize_BCMapBox_MapRequestGlobeStyle(arg) {
  if (!(arg instanceof BCMapBox_pb.MapRequestGlobeStyle)) {
    throw new Error('Expected argument of type BCMapBox.MapRequestGlobeStyle');
  }
  return new Buffer(arg.serializeBinary());
}

function deserialize_BCMapBox_MapRequestGlobeStyle(buffer_arg) {
  return BCMapBox_pb.MapRequestGlobeStyle.deserializeBinary(new Uint8Array(buffer_arg));
}

function serialize_BCMapBox_MapRequestTilesetId(arg) {
  if (!(arg instanceof BCMapBox_pb.MapRequestTilesetId)) {
    throw new Error('Expected argument of type BCMapBox.MapRequestTilesetId');
  }
  return new Buffer(arg.serializeBinary());
}

function deserialize_BCMapBox_MapRequestTilesetId(buffer_arg) {
  return BCMapBox_pb.MapRequestTilesetId.deserializeBinary(new Uint8Array(buffer_arg));
}

function serialize_BCMapBox_MapRequestToken(arg) {
  if (!(arg instanceof BCMapBox_pb.MapRequestToken)) {
    throw new Error('Expected argument of type BCMapBox.MapRequestToken');
  }
  return new Buffer(arg.serializeBinary());
}

function deserialize_BCMapBox_MapRequestToken(buffer_arg) {
  return BCMapBox_pb.MapRequestToken.deserializeBinary(new Uint8Array(buffer_arg));
}


var BCMapService = exports.BCMapService = {
  transToken: {
    path: '/BCMapBox.BCMap/TransToken',
    requestStream: false,
    responseStream: false,
    requestType: BCMapBox_pb.MapRequestToken,
    responseType: BCMapBox_pb.MapReply,
    requestSerialize: serialize_BCMapBox_MapRequestToken,
    requestDeserialize: deserialize_BCMapBox_MapRequestToken,
    responseSerialize: serialize_BCMapBox_MapReply,
    responseDeserialize: deserialize_BCMapBox_MapReply,
  },
  transDataSetId: {
    path: '/BCMapBox.BCMap/TransDataSetId',
    requestStream: false,
    responseStream: false,
    requestType: BCMapBox_pb.MapRequestDatasetId,
    responseType: BCMapBox_pb.MapReply,
    requestSerialize: serialize_BCMapBox_MapRequestDatasetId,
    requestDeserialize: deserialize_BCMapBox_MapRequestDatasetId,
    responseSerialize: serialize_BCMapBox_MapReply,
    responseDeserialize: deserialize_BCMapBox_MapReply,
  },
  transTilesetId: {
    path: '/BCMapBox.BCMap/TransTilesetId',
    requestStream: false,
    responseStream: false,
    requestType: BCMapBox_pb.MapRequestTilesetId,
    responseType: BCMapBox_pb.MapReply,
    requestSerialize: serialize_BCMapBox_MapRequestTilesetId,
    requestDeserialize: deserialize_BCMapBox_MapRequestTilesetId,
    responseSerialize: serialize_BCMapBox_MapReply,
    responseDeserialize: deserialize_BCMapBox_MapReply,
  },
  transGlobeStyle: {
    path: '/BCMapBox.BCMap/TransGlobeStyle',
    requestStream: false,
    responseStream: false,
    requestType: BCMapBox_pb.MapRequestGlobeStyle,
    responseType: BCMapBox_pb.MapReply,
    requestSerialize: serialize_BCMapBox_MapRequestGlobeStyle,
    requestDeserialize: deserialize_BCMapBox_MapRequestGlobeStyle,
    responseSerialize: serialize_BCMapBox_MapReply,
    responseDeserialize: deserialize_BCMapBox_MapReply,
  },
  transFeature: {
    path: '/BCMapBox.BCMap/TransFeature',
    requestStream: false,
    responseStream: false,
    requestType: BCMapBox_pb.MapRequestFeature,
    responseType: BCMapBox_pb.MapReply,
    requestSerialize: serialize_BCMapBox_MapRequestFeature,
    requestDeserialize: deserialize_BCMapBox_MapRequestFeature,
    responseSerialize: serialize_BCMapBox_MapReply,
    responseDeserialize: deserialize_BCMapBox_MapReply,
  },
  transGlobeId: {
    path: '/BCMapBox.BCMap/TransGlobeId',
    requestStream: false,
    responseStream: false,
    requestType: BCMapBox_pb.MapRequestGlobeId,
    responseType: BCMapBox_pb.MapReply,
    requestSerialize: serialize_BCMapBox_MapRequestGlobeId,
    requestDeserialize: deserialize_BCMapBox_MapRequestGlobeId,
    responseSerialize: serialize_BCMapBox_MapReply,
    responseDeserialize: deserialize_BCMapBox_MapReply,
  },
  transFeatureId: {
    path: '/BCMapBox.BCMap/TransFeatureId',
    requestStream: false,
    responseStream: false,
    requestType: BCMapBox_pb.MapRequestFeatureId,
    responseType: BCMapBox_pb.MapReply,
    requestSerialize: serialize_BCMapBox_MapRequestFeatureId,
    requestDeserialize: deserialize_BCMapBox_MapRequestFeatureId,
    responseSerialize: serialize_BCMapBox_MapReply,
    responseDeserialize: deserialize_BCMapBox_MapReply,
  },
};

exports.BCMapClient = grpc.makeGenericClientConstructor(BCMapService);
