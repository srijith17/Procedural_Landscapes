using System;
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class RegionMapDisplay : TextureMapDisplay
{
    public override void DisplayTexture()
    {
        if (LandscapeGenerator.Instance.regionMap != null)
        {
            Debug.Log("Displaying Noise Map...");
            TextureGenerator.TextureFromRegionMap(ref textureMap, LandscapeGenerator.Instance.regionMap);
            DrawTexture(textureMap);
        }
        else
        {
            Debug.LogError("Texture not created");
        }
    }

    public static Color GetDisplayColor(RegionMapGenerator.RegionType region)
    {
        switch (region)
        {
            case RegionMapGenerator.RegionType.Unassigned:
                return Color.white;
            case RegionMapGenerator.RegionType.Water:
                return Color.blue;
            case RegionMapGenerator.RegionType.Urban:
                return Color.black;
            default:
                return Color.gray;
        }
    }
}
