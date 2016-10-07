using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class NoiseMapOptions
{
    public int seed = 2;
    public float scale = 25;
    public int octaves = 12;
    [Range(0, 1.0f)]
    public float persistance = 0.5f;
    public float lacunarity = 4;
    public Vector2 offset;
}

public static class NoiseMapGenerator
{

    public static bool Generate(ref float[,] noiseMap, NoiseMapOptions noiseMapOptions)
    {
        Debug.Log("Generating Noise Map...");
        int mapHeight = noiseMap.GetLength(0);
        int mapWidth = noiseMap.GetLength(1);

        noiseMapOptions.scale = (noiseMapOptions.scale <= 0) ? 0.0001f : noiseMapOptions.scale;

        System.Random prng = new System.Random(noiseMapOptions.seed);
        Vector2[] octaveOffsets = new Vector2[noiseMapOptions.octaves];
        for (int i = 0; i < noiseMapOptions.octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + noiseMapOptions.offset.x;
            float offsetY = prng.Next(-100000, 100000) + noiseMapOptions.offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < noiseMapOptions.octaves; i++)
                {
                    float sampleX = (x - halfWidth) / noiseMapOptions.scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / noiseMapOptions.scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= noiseMapOptions.persistance;
                    frequency *= noiseMapOptions.lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);                
            }
        }

        return true;
    }

}
