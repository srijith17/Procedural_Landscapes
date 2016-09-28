using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

//This class takes in HeightMap and creates an Intellignet terrain based the porperties of the terrain.

[Serializable]
class WaterBodyFinderOptions
{
    public int maximumRandomSearches = 10;
    public int gradientDescentStepSize = 2;
    public bool drawDebugLines = true;
}


public class TerrainGenerator : MonoBehaviour
{

    enum Terrain
    {
        Unassigned = 0, 
        Water = 1,
        Urban = 2,

    }

    private int[,] _terrainMap;
    private WaterBodyFinderOptions waterBodyFinderOptions;


    void CreateTerrain(float[,] heightmap)
    {
        CreateTerrainMap(heightmap.GetLength(0), heightmap.GetLength(1));

        ///Find good locations for water boies and assign them in the map
        FindWaterBodies(_terrainMap, heightmap, waterBodyFinderOptions);

        ///Conect Water Bodies to create rivers.
        //ConnectWaterBodies();

        ///Connect Flat reagions to create urban areas  (farms, towns  and cities)
        //FindUrbanAreas();

        ///Connects Urban Areas with roads.
        //ConnectUrbanAreas();

        ///Assign Landscapes like Forest, Desert, Mountain, Flain, Farm
        //AssignLandscapeType();
    }

    private void FindWaterBodies(int[,] _terrainMap, float[,] heightmap, WaterBodyFinderOptions waterBodyFinderOptions)
    {
        int randomeSamples = waterBodyFinderOptions.maximumRandomSearches;
        for (int i = 0; i < randomeSamples; i++)
        {
            Vector2 searchStartPoint = new Vector2(0, 0);
            Vector2 localMinima = new Vector2(0, 0);
            SampleRandomPoint(ref searchStartPoint, heightmap.GetLength(0), heightmap.GetLength(1));
            GradientDescentToLocalMinima(ref localMinima, searchStartPoint, heightmap);

        }
    }

    private void GradientDescentToLocalMinima(ref Vector2 localMinima, Vector2 searchStartPoint, float[,] heightmap)
    {
        //calculate gradiesnt
        //check termination condition
        //find maximum gradient
        //move point to that location
    }


    private void SampleRandomPoint(ref Vector2 point, int rows, int columns)
    {
        point.x = Random.Range(0, rows);
        point.y = Random.Range(0, columns);
    }

    private void CreateTerrainMap(int rows, int columns)
    {
        _terrainMap = new int[rows, columns];
        Array.Clear(_terrainMap, 0, _terrainMap.Length);         //probably not needed
    }

}
