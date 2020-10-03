using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

public class DownloadTest : MonoBehaviour
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
        var texture = await DownloadTexture(uri) as Texture2D;
        
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
        string token =
            "ya29.a0AfH6SMBu4igLKgUSU1MgivJbVmlc7IcQVRugvwNtAW0EHyOvqkxX_Vpr0yfskVAUUqQ_NTZ689QGI3e_uP3Gt60ePskny0I1z4185P78479xM8a18Vj0CNXzSD_xnmX0oXA4aVOO3PhGg2rHsAFfGQgnVHkUW7RBCA_n";

            req.SetRequestHeader("Authorization",$"Bearer {token}");
        
        await req.SendWebRequest();
        
        if (req.isNetworkError) {
            Debug.Log(req.error);
        } else {
            if (req.responseCode == 200) {
                text = req.downloadHandler.text;
                Debug.Log("responce: " + text);
            }
        }

        return text;
    }
    
    async UniTask<Texture> DownloadTexture(string uri)
    {
        var req = UnityWebRequestTexture.GetTexture(uri);

        await req.SendWebRequest();
        
        if (req.isNetworkError) {
            Debug.Log(req.error);
        } else {
            if (req.responseCode == 200)
            {
                Debug.Log($"success: {req.responseCode} => {req.downloadHandler.text}");
            }
        }

        return DownloadHandlerTexture.GetContent(req);
    }
}
