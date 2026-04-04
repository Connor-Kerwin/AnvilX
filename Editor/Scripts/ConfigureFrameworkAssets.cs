using System;
using UnityEditor;
using UnityEngine;

namespace AnvilX
{
    internal class ConfigureFrameworkAssets
    {
        [MenuItem("AnvilX/Configure Framework Assets")]
        public static void Configure()
        {
            var paths = AssetDatabase.FindAssets("t:AnvilX.FrameworkAssets");
            if (paths.Length == 0)
            {
                throw new Exception(
                    "Could not find an instance of the framework assets class to setup, something went badly wrong!");
            }

            var frameworkAssetPath = AssetDatabase.GUIDToAssetPath(paths[0]);
            var frameworkAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(frameworkAssetPath);
            
            var preloadedAssets = PlayerSettings.GetPreloadedAssets();
            foreach (var asset in preloadedAssets)
            {
                // Asset is already in preloaded assets
                if (asset == frameworkAsset)
                {
                    return;
                }
            }

            // Inject the asset into the preloaded assets list
            var newAssets = new UnityEngine.Object[preloadedAssets.Length + 1];
            preloadedAssets.CopyTo(newAssets, 0);
            newAssets[^1] = frameworkAsset;
            
            PlayerSettings.SetPreloadedAssets(newAssets);
            
            Debug.Log("Framework asset assigned to preloaded assets");
        }
    }
}