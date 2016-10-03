using UnityEngine;

public abstract class TextureMapDisplay : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    protected Texture2D textureMap;
    public bool autoUpdate;

    public void Start()
    {
        UpdateDisplayMap();
    }

    public void DrawTexture(Texture2D texture)
    {
        meshRenderer.sharedMaterial.mainTexture = texture;
        meshRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void UpdateDisplayMap()
    {
        if (meshFilter == null)
        {
            meshFilter = this.GetComponent<MeshFilter>();
        }

        if (meshRenderer == null)
        {
            meshRenderer = this.GetComponent<MeshRenderer>();
        }
        DisplayTexture();
    }

    abstract public void DisplayTexture();
}