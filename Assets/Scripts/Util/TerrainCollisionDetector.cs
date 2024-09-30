using UnityEngine;
using System.Collections.Generic;

public class TerrainCollisionDetector : MonoBehaviour
{
    [SerializeField]
    private TerrainPooler pooler;

    private List<GameObject> activeTerrains;

    private float terrainLength = 50f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activeTerrains = GetListOfTerrain();

            // Check if the list contains at least one terrain to remove
            if (activeTerrains.Count > 0)
            {
                // Get and remove the first terrain in the list (the oldest one)
                GameObject firstTerrain = activeTerrains[0]; // Head of the list
                pooler.ReturnTerrainToPool(firstTerrain);
                activeTerrains.RemoveAt(0); // Remove it from the list
            }

            // Generate the next terrain and add it to the end of the list
            GameObject pooledTerrain = pooler.GetNextTerrain();
            if (pooledTerrain != null)
            {
                pooledTerrain.SetActive(true);

                // Position the new terrain at the end of the last terrain in the list
                if (activeTerrains.Count > 0)
                {
                    // Position the terrain
                    GameObject lastTerrain = activeTerrains[activeTerrains.Count - 1]; // Last terrain
                    pooledTerrain.transform.position = this.transform.position + new Vector3(terrainLength, 0, 0);
                }
                else
                {
                    // If the list is empty, position the terrain at the origin
                    pooledTerrain.transform.position = Vector3.zero;
                }

                activeTerrains.Add(pooledTerrain); // Add the new terrain to the end of the list

                // Assign the same pooler and list to the new terrain detector
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
