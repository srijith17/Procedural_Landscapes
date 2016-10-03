using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using Random = UnityEngine.Random;

//This class takes in HeightMap and creates an Intellignet terrain based the porperties of the terrain.

[Serializable]
public class WaterBodyOptions
{
    public int maximumRandomSearches = 10;
    public int gradientDescentStepSize = 2;
    public bool drawDebugLines = true;
}

[Serializable]
public class RegionMapOptions
{
    public WaterBodyOptions waterBodyOptions;
}

public class RegionMapGenerator
{

    enum Terrain
    {
        Unassigned = 0, 
        Water = 1,
        Urban = 2,

    }

    private int[,] terrainMap;


    private static void CreateTerrain(ref int[,] regionMap, float[,] heightmap, RegionMapOptions RegionMapOptions)
    {

        ///Find good locations for water boies and assign them in the map
        FindWaterBodies(ref regionMap, heightmap, RegionMapOptions.waterBodyOptions);

        ///Conect Water Bodies to create rivers.
        //ConnectWaterBodies();

        ///Connect Flat reagions to create urban areas  (farms, towns  and cities)
        //FindUrbanAreas();

        ///Connects Urban Areas with roads.
        //ConnectUrbanAreas();

        ///Assign Landscapes like Forest, Desert, Mountain, Flain, Farm
        //AssignLandscapeType();
    }

    private static void FindWaterBodies(ref int[,] regionMap, float[,] heightmap, WaterBodyOptions waterBodyFinderOptions)
    {
        int randomeSamples = waterBodyFinderOptions.maximumRandomSearches;
        for (int i = 0; i < randomeSamples; i++)
        {
            Vector2 searchStartPoint = new Vector2(0, 0);
            Vector2 localMinima = new Vector2(0, 0);
            SampleRandomPoint(ref searchStartPoint, heightmap.GetLength(0), heightmap.GetLength(1));
            GradientDescentToLocalMinima(ref localMinima, searchStartPoint, heightmap);
            Debug.Log("local minima no "+ i + ":\t" + localMinima);
        }
    }

    private static bool GradientDescentToLocalMinima(ref Vector2 localMinima, Vector2 searchPoint, float[,] heightmap)
    {
        Vector2[] steps = new Vector2[8] {new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(-1, 1), new Vector2( 0, -1), new Vector2( 0, 1), new Vector2( 1, -1), new Vector2( 1, 0), new Vector2( 1, 1)};
        float[] gradients = new float[8];
            
        for (int i = 0; i < steps.Length; i++)
        {
            Vector2 comparePoint = searchPoint + steps[i];
            if (comparePoint.x == 0 || comparePoint.y == 0 || comparePoint.x == heightmap.GetLength(0) ||
                comparePoint.y == heightmap.GetLength(1))
            {
                gradients[i] = 0;
            }
            else
            {
                gradients[i] = heightmap[(int)comparePoint.x, (int)comparePoint.y] -
                                 heightmap[(int)searchPoint.x, (int)searchPoint.y];
            }
        }

        int minIndex = Array.IndexOf(gradients, gradients.Min());
        if (gradients[minIndex] > 0)
        {
            localMinima = searchPoint;
            return true;
        }

        Vector2 newSearchPoint = searchPoint - steps[minIndex];
        return GradientDescentToLocalMinima(ref localMinima, newSearchPoint, heightmap);
    }


    private static void SampleRandomPoint(ref Vector2 point, int rows, int columns)
    {
        point.x = Random.Range(0, rows);
        point.y = Random.Range(0, columns);
    }

    //private void CreateTerrainMap(int rows, int columns)
    //{
    //    _terrainMap = new int[rows, columns];
    //    Array.Clear(_terrainMap, 0, _terrainMap.Length);         //probably not needed
    //}

    public static void Generate(ref int[,] regions, float[,] heightmap, RegionMapOptions regionMapOptions)
    {
        CreateTerrain(ref regions, heightmap, regionMapOptions);
    }
}
