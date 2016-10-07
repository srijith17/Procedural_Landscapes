using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RegionMapDisplay))]
public class RegionMapDisplayEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RegionMapDisplay regionMapDisplay = (RegionMapDisplay)target;


        if (DrawDefaultInspector())
        {
            if (regionMapDisplay.autoUpdate)
            {
                regionMapDisplay.UpdateDisplayMap();
            }
        }


        if (GUILayout.Button("UpdateDisplay"))
        {
            regionMapDisplay.UpdateDisplayMap();
        }
    }
}


