using UnityEngine;
using System.Collections;

public static class TextureGenerator
{

    public static void TextureFromColourMap(ref Texture2D texture, Color[] colourMap, int width, int height)
    {
        texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colourMap);
        texture.Apply();
        return;
    }


    public static void TextureFromNoiseMap(ref Texture2D texture, float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Color[] colourMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }

        TextureFromColourMap(ref texture, colourMap, width, height);
        return;
    }

    public static void TextureFromHeightMap(ref Texture2D texture, float[,] heightMap)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        float maximumHeight = MaximumValue(heightMap);

        Color[]  colourMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y] / maximumHeight);
            }
        }

        TextureFromColourMap(ref texture, colourMap, width, height);
        return;
    }

    public static float MaximumValue(float[,] array)
    {
        float maxValue = float.MinValue;
        for (int y = 0; y < array.GetLength(0); y++)
        {
            for (int x = 0; x < array.GetLength(1); x++)
            {
                if (array[x, y] > maxValue)
                    maxValue = array[x, y];
            }

        }
        return maxValue;
    }
}