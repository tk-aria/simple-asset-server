using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.IO;
using System.Text;

public class DownloadTest : MonoBehaviour
{
   async void Start()
   {
       var result = await DownloadManifest(
           "AKfycby4d5YE4p82-rWLJOre58YY-KF5qz4mKjydcw7nJm8Fsn0Q7kY",
           "ya29.a0AfH6SMCk8bN5ganGlXdOITyBSQeG9Ad9EiRoDLfVWZ9dRXjklZa3JxO-xtMOmrEH3gITa7eHfMxdJv0UGQpDXL2yVlgqx1JVk0pFEF64efPpP2oMritkfxpDGdb9iaiGhzzzPjWRYaymZVF6Q56NJlBsU9TuOnoO5sf7");
        Debug.Log($"result: <{result}>");

        var res = JsonUtility.FromJson<SimpleAssetServer.Resource>(result);
        
        FileStream fs = new FileStream($"{Application.dataPath}/Download/{res.signature}", FileMode.Create);
        BinaryWriter bw = new BinaryWriter(fs);
        
        bw.Write(res.body);

        bw.Close();
        fs.Close();
        
#if UNITY_EDITOR
       
       UnityEditor.AssetDatabase.Refresh();
       
#endif // UNITY_EDITOR END.
    }

    async UniTask<string> DownloadManifest(string scriptId, string token)
    {
        string text = "";
        var googleApp = new SimpleAssetServer.GoogleAppScript(scriptId, token);
        var res= await googleApp.Get();

        return res;
    }
    
    /*
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
    */
}
