using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LandscapeGenerator))]
public class LandscapeGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LandscapeGenerator LandscapGen = (LandscapeGenerator)target;

        if (DrawDefaultInspector())
        {
            if (LandscapGen.autoUpdate)
            {
                LandscapGen.GenerateMap();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            LandscapGen.GenerateMap();
        }
    }
}
