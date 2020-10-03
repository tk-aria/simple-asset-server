using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

public sealed class NewBehaviourScript : MonoBehaviour
{
    async void Start()
    {
        var result = await DownloadManifest();
        Debug.Log($"result: <{result}>");
        
        Debug.Log("Start Download");

        string uri =
            "https://drive.google.com/uc?id=1sO6Qy0LRjQ0MBKZfTYJT5vbN9sIWo885&export=download&access_token=ya29.a0AfH6SMBcdA8_NGqMgCnfEzh7Y9FB-JQtZ8a2jBGWJStXpifFex-0gxfbJPlCUiJB-zBYWTBa6M0tPRjwbUPy6qVXR2zWLwCcSZb_X8kUsHP3un6nwEdiswt94wzHJ1a0AUEU6FfZuUSoFFXWmnoqB9em4UkIV_cKY0A";
            //"https://drive.google.com/uc?id=1sO6Qy0LRjQ0MBKZfTYJT5vbN9sIWo885&export=download&access_token=ya29.a0AfH6SMD0TAq8Yz0VVnh_Ta6HSSaadEQ9t2SLl3svi5XBO3j6see72-fGGfEUWU5qQTBgSRcScGszFfov4sG7xVE67GEpqWDyRD0tEp-55bY_GqsaoZt0cExZCEqDniwfnjvushjw4Qz03FJcbJ7oYKMc877uDCXgyOoM";
            //"https://drive.google.com/uc?id=1sO6Qy0LRjQ0MBKZfTYJT5vbN9sIWo885&export=download&access_token=ya29.a0AfH6SMBeco-hIGzMaNvp2aMIBfhyurdTkVEMuhMO1A1q8jG4hMmiGX7rhzjNE6OPnscSIIXN0omnOXyKPfUgToua3EkfUFU9rN0J-Jzfp_8pJPzuJm2KfEnSb9Z676f4jW60fCeH6mm6NNG8HeLVHeRWwexQQ5GGZ7ME";
        //テクスチャをダウンロード
        var texture = await DownloadTexture(result) as Texture2D;
        
        var png = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes( $"{Application.dataPath}/sample.png", png );

        // テクスチャセット
        gameObject.GetComponent<Renderer>().material.mainTexture = texture;
    }

    async UniTask<string> DownloadManifest()
    {
        string text = "";
        var req =
            UnityWebRequest.Get(
                "https://script.google.com/macros/s/AKfycby4d5YE4p82-rWLJOre58YY-KF5qz4mKjydcw7nJm8Fsn0Q7kY/exec");
        string oauthToken =
            //"ya29.a0AfH6SMBd5rAGIp3WxaOmurcPk52yy6mqgsuB9XMeyFrIHr2i6KjAJdoMaU9dwL8W8MElZ4fYWBMey3tQwQIWZrrprbIDQhfW9iUA_KVlC_1O-9UmIEhrhplgs_eMtqg-HQx1PKq35IF_U-XqojqgagqyUubMT2zGWA3D";
            //"ya29.a0AfH6SMCJv_ny6RBmrDcFwROI21D2UOdp9YYYAlchj52CYLIAG8vggXOk938swRx7Y6Rl6fwiCCl96JpgxkGw7ynz521QGgnJzAm205EeoiD3EV7o04Wthv2Sa0IXWkkHZClB13OpPg6UKl5aEf9x7Pxd552P-dLg4y1g";
            //"ya29.a0AfH6SMAlUNnWjsjfYX8woM9DlSmOufffLqWKiuNVbuzrb--MRD3y2whS2f8LduQf1daiLs7nVxoewVkCV8UPdYooBvMyGDEIw0O32-JjLXI7TfCmF8n50ASknGHTQCz0txsopm9M_vpx7auVwUaqiq472htlzcqsDt4S";
            "ya29.a0AfH6SMAK2ce1_2C-ndUAkZ9GWMWVsxIbMmzSRVl_CXnv8tiIAJbSFFOnrSkclot15h2Mf2hiq6Z5cVV02mPSq1YJ6kHUk_ycaEtMb62Qb9Zmqpn3KlJpjUTmg_a-3dBJHXInzkyQlRb4S51HwGrbzDVHsyRi6_7sNQn2";
        req.SetRequestHeader("Authorization",$"Bearer {oauthToken}");

        //https://drive.google.com/uc?id=1sO6Qy0LRjQ0MBKZfTYJT5vbN9sIWo885&export=download&access_token=ya29.a0AfH6SMCZl5KIczabmv8WboGrAvd4IrFxFF6ZXy1LO2X7zQLQ0UiATPQsBKkXdCEQAQGVReqnmSRRHFLM-8tBdOL2_ULvvZPXr5CE_Sah4uyot0QHWVq5k1P6qFUBhWE6h0hL8yysCnnCHwbLkLwkjAnAJL0QVP_T07gP
        
        await req.SendWebRequest();
        
        if (req.isNetworkError) {
            Debug.Log(req.error);
        } else {
            if (req.responseCode == 200) {
                // UTF8文字列として取得する
                text = req.downloadHandler.text;
 
                Debug.Log("responce: " + text);
                
                // バイナリデータとして取得する
               //byte[] results = request.downloadHandler.data;
            }
        }

        return text;
    }
    
    async UniTask<Texture> DownloadTexture(string uri)
    {
        // 適当に画像のURL

        var req = UnityWebRequestTexture.GetTexture(uri);

        await req.SendWebRequest();
        
        if (req.isNetworkError) {
            Debug.Log(req.error);
        } else {
            if (req.responseCode == 200)
            {

                Debug.Log($"success: {req.responseCode} => {req.downloadHandler.text}");

                // バイナリデータとして取得する
                //byte[] results = request.downloadHandler.data;
            }
        }

        return DownloadHandlerTexture.GetContent(req);
    }
    
    void Update()
    {
        
    }
}
