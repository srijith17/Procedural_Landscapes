using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(NoiseMapDisplay))]
public class NoiseMapDisplayEditor : Editor
{
    public override void OnInspectorGUI()
    {
        NoiseMapDisplay noiseMapDisplay = (NoiseMapDisplay)target;


        if (DrawDefaultInspector())
        {
            if (noiseMapDisplay.autoUpdate)
            {
                noiseMapDisplay.UpdateDisplayMap();
            }
        }


        if (GUILayout.Button("UpdateDisplay"))
        {
            noiseMapDisplay.UpdateDisplayMap();
        }
    }
}


