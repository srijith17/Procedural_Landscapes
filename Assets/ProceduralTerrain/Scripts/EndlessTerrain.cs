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
	List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();

	void Start() {
		chunkSize = HeightMapGenerator.mapChunkSize - 1;
		chunksVisibleInViewDst = Mathf.RoundToInt(maxViewDst / chunkSize);
	}

	void Update(){
		viewerPosition = new Vector2 (viewer.position.x, viewer.position.z);
		UpdateVisibleChunks ();
	}

	void UpdateVisibleChunks(){

		foreach (TerrainChunk chunk in terrainChunksVisibleLastUpdate){
			chunk.SetVisible(false);
		}
		terrainChunksVisibleLastUpdate.Clear ();  

		int currentChunkCoordX = Mathf.RoundToInt (viewerPosition.x / chunkSize);
		int currentChunkCoordY = Mathf.RoundToInt (viewerPosition.y / chunkSize);

		for (int yOffSet = -chunksVisibleInViewDst; yOffSet <= chunksVisibleInViewDst; yOffSet++) {
			for (int xOffSet = -chunksVisibleInViewDst; xOffSet <= chunksVisibleInViewDst; xOffSet++) {
				Vector2 viewedChunkCoord = new Vector2 (currentChunkCoordX + xOffSet, currentChunkCoordY + yOffSet);

				if (!terrainChunkDictionary.ContainsKey (viewedChunkCoord)) {
					terrainChunkDictionary.Add (viewedChunkCoord, new TerrainChunk (viewedChunkCoord, chunkSize, transform));
				} else {
					terrainChunkDictionary [viewedChunkCoord].UpdateTerrainChunk ();
					if (terrainChunkDictionary [viewedChunkCoord].isVisible ()) {
						terrainChunksVisibleLastUpdate.Add (terrainChunkDictionary [viewedChunkCoord]);
					}
				}
			}
		}
	}

	public class TerrainChunk{

		GameObject meshObject;
		Vector2 position;
		Bounds bounds;

		public TerrainChunk(Vector2 coord, int size, Transform parent){
			position = coord *size;
			bounds = new Bounds(position, Vector2.one * size);
			Vector3 positionV3 = new Vector3(position.x, 0, position.y);

			meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
			meshObject.transform.position = positionV3;
			meshObject.transform.localScale = Vector3.one * size /10f;
			meshObject.transform.parent = parent;

			SetVisible(false);
		}

		public void UpdateTerrainChunk(){
			float viewerDstFromEdge = bounds.SqrDistance (viewerPosition);
			bool visible = viewerDstFromEdge <= maxViewDst;
			SetVisible (visible);
		}

		public void SetVisible(bool vis){
			meshObject.SetActive (vis);
		}

		public bool isVisible(){
			return meshObject.activeSelf;
		}
	}
}
