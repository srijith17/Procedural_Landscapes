using UnityEngine;
using System.Collections;

public class LandscapeGenerator : Singleton<LandscapeGenerator>
{
    //[SerializeField]
    //private HeightMapOptions heightMapOptions = new HeightMapOptions();

    [SerializeField]
    private NoiseMapOptions noiseMapOptions = new NoiseMapOptions();

    [SerializeField]
    private HeightMapOptions heightMapOptions = new HeightMapOptions();

    [SerializeField]
    private RegionMapOptions regionMapOptions = new RegionMapOptions();

    [SerializeField]
    private MeshGeneratorOptions meshGeneratorOptions = new MeshGeneratorOptions();


    public float[,] noiseMap;
    public float[,] heightMap;
    public RegionMapGenerator.RegionType[,] regionMap;
    public MeshData terrainMesh;

    public bool autoUpdate;

    public int mapRows = 256;
    public int mapColumns = 256;

    public void Awake()
    {
        GenerateMap();
    }

    // Use this for initialization
    public void GenerateMap()
    {
        if (noiseMap == null)
        {
            noiseMap = new float[mapRows, mapColumns];
        }
        NoiseMapGenerator.Generate(ref noiseMap, noiseMapOptions);
        if (heightMap == null)
        {
            heightMap = new float[mapRows, mapColumns];
        }
        HeightMapGenerator.Generate(ref heightMap, noiseMap, heightMapOptions);


        if (regionMap == null)
        {
            regionMap = new RegionMapGenerator.RegionType[mapRows, mapColumns];
        }
        RegionMapGenerator.Generate(ref regionMap, heightMap, regionMapOptions);

        MeshGenerator.GenerateTerrainMesh(ref terrainMesh, heightMap, meshGeneratorOptions);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
