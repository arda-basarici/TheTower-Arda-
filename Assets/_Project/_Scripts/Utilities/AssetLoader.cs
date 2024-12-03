using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;


// Using AssetLoader class to load assets from Resources folder because if we switch to Addressables in the future, we can change the implementation of AssetLoader class without changing the code that uses it.
public static class AssetLoader
{
    public static T LoadAsset<T>(string path) where T : Object
    {
        T asset = Resources.Load<T>(path);
        if (asset == null)
        {
            Debug.LogError($"Asset not found at path: {path}");
        }
        return asset;
    }

    public static T[] LoadAll<T>(string path) where T : Object
    {
        T[] assets = Resources.LoadAll<T>(path);

       if(assets.Length == 0)
        {
            Debug.LogError($"No assets found at path: {path}");
            return null;
        }   
       return assets;
    }
}