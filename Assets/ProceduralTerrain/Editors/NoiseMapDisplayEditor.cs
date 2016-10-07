using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(NoiseMapDisplay))]
public class NoiseMapDisplayEditor : Editor
{
    public override void OnInspectorGUI()
    {
        NoiseMapDisplay noiseMapDisplay = (NoiseMapDisplay)target;
        //EditorGUI.DrawTexture(new Rect(10, 10, 60, 60), noiseMapDisplay.textureMap, ScaleMode.ScaleToFit, true, 10.0F);
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


