using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndlessTerrain : MonoBehaviour {

	public const float maxViewDst = 300;
	public Transform viewer;

	public static Vector2 viewerPosition;
	int chunkSize;
	int chunksVisibleInViewDst;

	Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();

	void Start() {
		chunkSize = NoiseMapGenerator.mapChunkSize - 1;
		chunksVisibleInViewDst = Mathf.RoundToInt(maxViewDst / chunkSize);
	}

	void UpdateVisibleChunks(){
		int currentChunkCoordX = Mathf.RoundToInt (viewerPosition.x / chunkSize);
		int currentChunkCoordY = Mathf.RoundToInt (viewerPosition.y / chunkSize);

		for (int yOffSet = -chunksVisibleInViewDst; yOffSet <= chunksVisibleInViewDst; yOffSet++) {
			for (int xOffSet = -chunksVisibleInViewDst; xOffSet <= chunksVisibleInViewDst; xOffSet++) {
				Vector2 viewedChunkCoord = new Vector2 (currentChunkCoordX + xOffSet, currentChunkCoordY + yOffSet);

				if (!terrainChunkDictionary.ContainsKey (viewedChunkCoord)) {
					terrainChunkDictionary.Add (viewedChunkCoord, new TerrainChunk ());
				}
			}
		}
	}

	public class TerrainChunk{
		
	}
}
