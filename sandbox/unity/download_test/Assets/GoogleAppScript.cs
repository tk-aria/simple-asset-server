using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

namespace SimpleAssetServer
{
  /// <summary>
  /// 
  /// </summary>
  public sealed class GoogleAppScript
  {
    string scriptId;
    string token;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scriptId"></param>
    /// <param name="token"></param>
    public GoogleAppScript(string scriptId, string token)
    {
      this.scriptId = scriptId;
      this.token = token;
    }

    /// <summary>
    ///  
    /// </summary>
    /// <returns></returns>
    public async UniTask<string> Get()
    {
        var req = UnityWebRequest.Get($"https://script.google.com/macros/s/{scriptId}/exec");
        req.SetRequestHeader("Authorization",$"Bearer {token}");
        
        await req.SendWebRequest();
        
        if (req.isNetworkError) {
            Debug.Log(req.error);
        } else {
            if (req.responseCode == 200) {
                var text = req.downloadHandler.text;
                Debug.Log("responce: " + text);
            }
        }

        return req.downloadHandler.text;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="payload"></param>
    /// <returns></returns>
    public async UniTask<string> Ppst(string payload)
    {
        var req = UnityWebRequest.Post($"https://script.google.com/macros/s/{scriptId}/exec", payload);
        req.SetRequestHeader("Authorization",$"Bearer {token}");
        
        await req.SendWebRequest();
        
        if (req.isNetworkError) {
            Debug.Log(req.error);
        }
        else
        {
            if (req.responseCode == 200)
            {
                var text = req.downloadHandler.text;
                Debug.Log("responce: " + text);
            }
        }

        return req.downloadHandler.text;
    }
  }

}