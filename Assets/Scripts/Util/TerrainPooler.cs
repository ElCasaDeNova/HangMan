using System.Collections.Generic;
using UnityEngine;

public class TerrainPooler : MonoBehaviour
{
    public List<GameObject> terrainPrefabs;  // List of different terrain prefabs
    public int poolSize = 4;  // Number of active terrains at the same time
    private Queue<GameObject> terrainPool;

    void Start()
    {
        terrainPool = new Queue<GameObject>();

        // Initialize the pool with the terrain prefabs
        for (int i = 0; i < poolSize; i++)
        {
            GameObject terrain = Instantiate(terrainPrefabs[i % terrainPrefabs.Count], transform);  // Set TerrainPooler as the parent
            terrain.SetActive(false);  // Deactivate all terrains initially
            terrainPool.Enqueue(terrain);  // Add to the pool
        }
    }

    // Function to get the next terrain from the pool and activate it
    public GameObject GetNextTerrain()
    {
        if (terrainPool.Count > 0)
        {
            return terrainPool.Dequeue();  // Return the terrain without activating it here
        }
        return null;  // Return null if the pool is empty
    }

    // Function to return the terrain to the pool
    public void ReturnTerrainToPool(GameObject terrain)
    {
        terrain.SetActive(false);  // Deactivate the terrain
        terrainPool.Enqueue(terrain);  // Add it back to the pool
    }
}
