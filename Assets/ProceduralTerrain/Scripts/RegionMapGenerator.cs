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

    public enum RegionType
    {
        Unassigned = 0, 
        Water = 1,
        Urban = 2,

    }


    private static void CreateTerrain(ref RegionType[,] regionMap, float[,] heightmap, RegionMapOptions RegionMapOptions)
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

    private static void FindWaterBodies(ref RegionType[,] regionMap, float[,] heightmap, WaterBodyOptions waterBodyFinderOptions)
    {
        int randomeSamples = waterBodyFinderOptions.maximumRandomSearches;
        for (int i = 0; i < randomeSamples; i++)
        {
            Vector2 searchStartPoint = new Vector2(0, 0);
            Vector2 localMinima = new Vector2(0, 0);
            SampleRandomPoint(ref searchStartPoint, heightmap.GetLength(0), heightmap.GetLength(1));
            if (GradientDescentToLocalMinima(ref localMinima, searchStartPoint, heightmap))
            {
                Debug.Log("local minima no " + i + ":\t" + localMinima + "=" + heightmap[(int)localMinima.x, (int)localMinima.y]);
            }
        }
    }

    private const int maxIteration = 50;

    //todo: unit test this function
    private static bool GradientDescentToLocalMinima(ref Vector2 localMinima, Vector2 searchPoint, float[,] heightmap)
    {
        Vector2[] steps = new Vector2[8] {new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(-1, 1), new Vector2( 0, -1), new Vector2( 0, 1), new Vector2( 1, -1), new Vector2( 1, 0), new Vector2( 1, 1)};
        float[] gradients = new float[8];

        int iteration = 0;
        while (maxIteration > iteration++ )
        {
            for (int i = 0; i < steps.Length; i++)
            {
                Vector2 comparePoint = searchPoint + steps[i];
                if (comparePoint.x <= 0 || comparePoint.y <= 0 || comparePoint.x > heightmap.GetLength(0) - 1 || comparePoint.y > heightmap.GetLength(1) - 1)
                {
                    gradients[i] = float.MaxValue;
                }
                else
                {
                    gradients[i] = heightmap[(int)comparePoint.x, (int)comparePoint.y] -
                                     heightmap[(int)searchPoint.x, (int)searchPoint.y];
                    //Debug.Log("searchPoint: " + heightmap[(int)comparePoint.x, (int)comparePoint.y] + "comparePoint: " + heightmap[(int)searchPoint.x, (int)searchPoint.y] + "\tgradients[i] = " + gradients[i]);
                }
            }

            int minIndex = Array.IndexOf(gradients, gradients.Min());
            Vector2 minPoint = searchPoint + steps[minIndex];
            //        Debug.Log("\theightMap[minIndex]: " + heightmap[(int)searchPoint.x, (int)searchPoint.y]);
            //        Debug.Log("minIndex: " + minIndex + "\theightMap[minIndex]: " + heightmap[(int)minPoint.x, (int)minPoint.y]);
            if (heightmap[(int)searchPoint.x, (int)searchPoint.y] <= heightmap[(int)minPoint.x, (int)minPoint.y])
            {
                localMinima = searchPoint;
                return true;
            }
            //return false;
            searchPoint = searchPoint - steps[minIndex];

        }
        return false;
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

    public static void Generate(ref RegionType[,] regions, float[,] heightmap, RegionMapOptions regionMapOptions)
    {
        CreateTerrain(ref regions, heightmap, regionMapOptions);
    }
}
