using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LandscapeDisplay))]
public class LanscapeDisplayEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LandscapeDisplay landscapDisplay = (LandscapeDisplay)target;

        if (DrawDefaultInspector())
        {
            if (landscapDisplay.autoUpdate)
            {
                landscapDisplay.UpdateDisplayMesh();
            }
        }

        if (GUILayout.Button("UpdateDisplay"))
        {
            landscapDisplay.UpdateDisplayMesh();
        }
    }
}
