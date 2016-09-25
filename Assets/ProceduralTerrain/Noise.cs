using UnityEngine;
using System.Collections;

public static class Noise {
	public static float[,] GenerateNoiseMap(int width, int height, float scale){
		float[,] noiseMap = new float[width,height];

		scale = (scale <= 0) ? 0.0001f : scale;

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				float sampleX = i/scale;
				float sampleY = j/scale;

				float perlin = Mathf.PerlinNoise (sampleX, sampleY);
				noiseMap [i, j] = perlin;
			}
		}

		return noiseMap;
	}

}
