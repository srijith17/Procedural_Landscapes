using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (HeightMapGenerator))]
public class HeightMapGeneratorEditor : Editor {
	public override void OnInspectorGUI(){
		HeightMapGenerator heightMapGen = (HeightMapGenerator)target;

		if (DrawDefaultInspector ()) {
			if (heightMapGen.autoUpdate) {
				heightMapGen.GenerateMap ();
			}
		}

		if (GUILayout.Button ("Generate")) {
			heightMapGen.GenerateMap ();
		}
	}
}
