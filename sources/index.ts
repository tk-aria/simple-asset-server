import { doGet, doPost } from "./service/assetServer";

declare const global: {
  [x: string]: unknown;
};

global.doGet = doGet;
global.doPost = doPost;
