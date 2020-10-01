'use strict'

/**
 *
 *
 * @export
 * @class AssetServer
 */
export default class AssetServer {

    private driveId: string = "";

    /**
     * Creates an instance of AssetServer.
     * @param {string} driveId
     * @memberof AssetServer
     */
    public constructor(driveId :string) {
        this.driveId = driveId;
    }

    /**
     * return server list.
     *
     * @returns {*}
     * @memberof LoginServer
     */
    public Get() : number[] {
        const fileId = this.FindFileIdFromGoogleDriveStorage(this.driveId, "naxalive_img.png");
        const requestUrl = `${fileId.getDownloadUrl()}&access_token=${ScriptApp.getOAuthToken()}`;
        return UrlFetchApp.fetch(requestUrl).getContent();
    }

    /**
     * @param {*} payload
     * @returns {*} response
     * @memberof LoginServer
     */
    public Post(payload: any) : GoogleAppsScript.Content.TextOutput {
        /*
       const data = JSON.parse(payload.postData.getDataAsString());
       const result = this.serverLogic[data.method](data.param);
       const response = ContentService.createTextOutput();
       response.setMimeType(ContentService.MimeType.JSON);
       response.setContent(result);
       */
       return ContentService.createTextOutput("hello world!");
    }

    private DriveUtil_GetContentsUrl() {
        var folderName = 'Dev';
        var fileName = 'simple.wasm';

        //ファイル名からフォルダIDとファイルIDを取得
        var folderId = DriveApp.getFoldersByName(folderName).next().getId();
        var fileId = DriveApp.getFolderById(folderId).getFilesByName(fileName).next()

        var url = fileId.getDownloadUrl();
        var wkToken = '&access_token=' + ScriptApp.getOAuthToken();

        console.log(url);

        return url + wkToken;
    }

    private FindFileIdFromGoogleDriveStorage(driveId: string, fileName: string): GoogleAppsScript.Drive.File {

        const contents: GoogleAppsScript.Drive.FileIterator = DriveApp.getFolderById(driveId).getFiles();

        var result:GoogleAppsScript.Drive.File;
        while (contents.hasNext()) {
            const file = contents.next();
            Logger.log("dump contents" + file.getName());

            if (file.getName() === fileName) {
                result = file;
                break;
            }
        }

        return result;
    }
}

const server = new AssetServer("1Ov0vgZRKFdnZ5MKAYJ8Q8AnNFTow3pD1");

export function doGet(): number[] {
    Logger.log("get request");
    return server.Get();
}

export function doPost(payload: any): GoogleAppsScript.Content.TextOutput {
    return ContentService.createTextOutput("Hello World");
}
