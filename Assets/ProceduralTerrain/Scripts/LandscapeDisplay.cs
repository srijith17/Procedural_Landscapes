using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class LandscapeDisplay : MonoBehaviour
{
    protected Renderer textureRenderer;
    protected MeshFilter meshFilter;
    protected MeshRenderer meshRenderer;

    public bool autoUpdate;

    public void Start()
    {
        UpdateDisplayMesh();
    }

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }

    public void UpdateDisplayMesh()
    {
        if (meshFilter == null)
        {
            meshFilter = this.GetComponent<MeshFilter>();
        }

        if (meshRenderer == null)
        {
            meshRenderer = this.GetComponent<MeshRenderer>();
        }

        DrawMesh(LandscapeGenerator.Instance.terrainMesh, Texture2D.whiteTexture);
    }

}
