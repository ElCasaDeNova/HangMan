using System.Collections.Generic;
using UnityEngine;

public class TerrainCollisionDetector : MonoBehaviour
{
    [SerializeField]
    private TerrainPooler pooler;

    private List<GameObject> activeTerrains; // List to track active terrains
    private float terrainLength = 25f;       // Length of a terrain (assumed constant)

    private void Start()
    {
        // Initialize the list with the current active terrains
        activeTerrains = GetListOfTerrain();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Update the list of active terrains
            activeTerrains = GetListOfTerrain();

            // Ensure there is a terrain to remove
            if (activeTerrains.Count > 0)
            {
                GameObject firstTerrain = activeTerrains[0]; // First terrain (the oldest)

                // Return the first terrain to the pool and remove it from the list
                pooler.ReturnTerrainToPool(firstTerrain);
                activeTerrains.RemoveAt(0);

                // Move the returned terrain to the end of the hierarchy
                firstTerrain.transform.SetAsLastSibling();
            }

            // Generate the next terrain and add it to the queue
            GameObject pooledTerrain = pooler.GetNextTerrain();
            if (pooledTerrain != null)
            {
                pooledTerrain.SetActive(true);

                // Position the new terrain after the last active terrain
                if (activeTerrains.Count > 0)
                {
                    GameObject lastTerrain = activeTerrains[activeTerrains.Count - 1]; // Last active terrain
                    pooledTerrain.transform.position = lastTerrain.transform.position + new Vector3(terrainLength, 0, 0);
                }
                else
                {
                    // If the list is empty, position the terrain at the origin
                    pooledTerrain.transform.position = Vector3.zero;
                }

                // Add the new terrain to the end of the list and in the hierarchy
                activeTerrains.Add(pooledTerrain);
                pooledTerrain.transform.SetAsLastSibling(); // Make it the last child

                // Assign the pooler to the new terrain's collision detector
                TerrainCollisionDetector newTerrainDetector = pooledTerrain.GetComponent<TerrainCollisionDetector>();
                if (newTerrainDetector != null)
                {
                    newTerrainDetector.pooler = pooler;
                }
            }
            else
            {
                Debug.LogWarning("Next terrain is null, check TerrainPooler.");
            }
        }
    }

    private List<GameObject> GetListOfTerrain()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        List<GameObject> listOfTerrains = new List<GameObject>();

        // Iterate through all the children of the parent (TerrainPooler) and get active terrains
        foreach (Transform terrain in parent.transform)
        {
            if (terrain.gameObject.activeInHierarchy)
            {
                listOfTerrains.Add(terrain.gameObject);
            }
        }

        return listOfTerrains;
    }
}
