'use strict'

/**
 *
 *
 * @export
 * @class AssetServer
 */
export default class AssetServer { // AssetProvisionServer

    private driveId: string;

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
    public Get() : GoogleAppsScript.Content.TextOutput {
        const filename: string = "naxalive_img.png";
        const fileId = this.FindFileIdFromGoogleDriveStorage(this.driveId, "sample.png");
        const requestUrl = `${fileId.getDownloadUrl()}&access_token=${ScriptApp.getOAuthToken()}`;
        //const body = fileId.getBlob().getDataAsString();
        const body = fileId.getBlob().getBytes();

        const payload = {
          'signature': filename,
          'url': requestUrl,
          'body': body
        };

        const res: string = JSON.stringify(payload);
        console.log(res);

        return ContentService.createTextOutput(res);

        //Logger.log(requestUrl);
        //var responce =  UrlFetchApp.fetch(requestUrl).getContentText();
        //return ContentService.createTextOutput(requestUrl);
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

const server = new AssetServer(PropertiesService.getScriptProperties().getProperty("DRIVE_ID"));

export function doGet(): GoogleAppsScript.Content.TextOutput {
    return server.Get();
}

export function doPost(payload: any): GoogleAppsScript.Content.TextOutput {
    return ContentService.createTextOutput("Hello World");
}
