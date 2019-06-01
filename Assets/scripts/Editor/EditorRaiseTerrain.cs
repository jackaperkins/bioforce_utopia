using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorRaiseTerrain : Editor
{
    [MenuItem("AAA/Raise Terrain")]
    public static void LowerTerrain () {
        foreach(GameObject o in Selection.gameObjects) {
            Debug.Log(o.name);

            Terrain terrain = o.GetComponent<Terrain>();
            if(terrain == null){
                continue;
            }

            int nx = terrain.terrainData.heightmapWidth;
            int ny = terrain.terrainData.heightmapHeight;

            float[,] htmap = terrain.terrainData.GetHeights(0, 0, nx, ny);

            
            float offset = 0.1f;

            for (int i = 0; i < nx; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    htmap[i, j] += offset;
                }
            }

            terrain.terrainData.SetHeights(0, 0, htmap);
        }
    }
}
