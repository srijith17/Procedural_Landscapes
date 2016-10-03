using System;
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class NoiseMapDisplay : TextureMapDisplay
{
    public override void DisplayTexture()
    {
        if (LandscapeGenerator.Instance.noiseMap != null)
        {
            Debug.Log("Displaying Noise Map...");
            TextureGenerator.TextureFromNoiseMap(ref textureMap, LandscapeGenerator.Instance.noiseMap);
            DrawTexture(textureMap);
        }
        else
        {
            Debug.LogError("Texture not created");
        }
    }

}
