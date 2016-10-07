using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class HeightMapOptions
{
    public float heightMultiplier;
    public AnimationCurve heightCurve;
}


public class HeightMapGenerator : MonoBehaviour
{

    public enum DrawMode { NoiseMap, ColourMap, Mesh };
    public DrawMode drawMode;

    const int mapChunkSize = 250;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public TerrainType[] regions;
    private readonly MeshGeneratorOptions meshGeneratorOptions = new MeshGeneratorOptions();

    ///HeightMapGenerator.Generate(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize)

    //public void GenerateMap()
    //{
    //    float[,] noiseMap = NoiseMapGenerator.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

    //    Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
    //    for (int y = 0; y < mapChunkSize; y++)
    //    {
    //        for (int x = 0; x < mapChunkSize; x++)
    //        {
    //            float currentHeight = noiseMap[x, y];
    //            for (int i = 0; i < regions.Length; i++)
    //            {
    //                if (currentHeight <= regions[i].height)
    //                {
    //                    colourMap[y * mapChunkSize + x] = regions[i].colour;
    //                    break;
    //                }
    //            }
    //        }
    //    }

    //    MapDisplay display = FindObjectOfType<MapDisplay>();
        //if (drawMode == DrawMode.NoiseMap)
        //{
        //    display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        //}
    //    else if (drawMode == DrawMode.ColourMap)
    //    {
    //        display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize));
    //    }
    //    else if (drawMode == DrawMode.Mesh)
    //    {
    //        display.DrawMesh();
    //    }
    //}

    void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }

    public static void Generate(ref float[,] heightMap, float[,] noiseMap, HeightMapOptions heightMapOptions)
    {
        Debug.Log("Generating Height Map...");

        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        for (int y = 0; y < height; y ++)
        {
            for (int x = 0; x < width; x ++)
            {
                heightMap[x,y] = heightMapOptions.heightCurve.Evaluate(noiseMap[x, y])* heightMapOptions.heightMultiplier;
            }
        }
    }
}


[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}