using System;
using UnityEngine;

namespace SimpleAssetServer
{
    [Serializable]
    public sealed class Resource
    {
        [SerializeField] public string signature;
        [SerializeField] public string url;
        [SerializeField] public byte[] body;
    }
}