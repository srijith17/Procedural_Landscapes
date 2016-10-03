using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(HeightMapTextureDisplay))]
public class HeightMapTextureDisplayEditor : Editor
{
    public override void OnInspectorGUI()
    {
        HeightMapTextureDisplay heightMapTextureDisplay = (HeightMapTextureDisplay)target;


        if (DrawDefaultInspector())
        {
            if (heightMapTextureDisplay.autoUpdate)
            {
                heightMapTextureDisplay.UpdateDisplayMap();
            }
        }


        if (GUILayout.Button("UpdateDisplay"))
        {
            heightMapTextureDisplay.UpdateDisplayMap();
        }
    }
}


