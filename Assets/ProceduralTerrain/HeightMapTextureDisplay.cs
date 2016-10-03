using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class HeightMapTextureDisplay : TextureMapDisplay
{
    public override void DisplayTexture()
    {
        if (LandscapeGenerator.Instance.noiseMap != null)
        {
            Debug.Log("Displaying height map");
            TextureGenerator.TextureFromHeightMap(ref textureMap, LandscapeGenerator.Instance.heightMap);
            DrawTexture(textureMap);
        }
        else
        {
            Debug.LogError("Texture not created");
        }
    }
}

