using UnityEngine;

public class TerrainCollisionDetector : MonoBehaviour
{
    public TerrainPooler pooler;
    public GameObject previousTerrain;
    public GameObject currentTerrain;
    public GameObject nextTerrain;
    public float terrainLength = 21f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("test");

            // Deactivate previous terrain and return it to the pool
            if (previousTerrain != null)
            {
                pooler.ReturnTerrainToPool(previousTerrain);
            }

            // Reorganize terrain order
            previousTerrain = currentTerrain; // Update previous terrain
            currentTerrain = nextTerrain; // Update current terrain

            // Generate the next terrain
            GameObject pooledTerrain = pooler.GetNextTerrain(); // Get the new terrain

            // Check if nextTerrain is valid
            if (pooledTerrain != null)
            {
                pooledTerrain.SetActive(true); // Activate the new terrain

                // Position the new terrain correctly
                pooledTerrain.transform.position = nextTerrain.transform.position + new Vector3(terrainLength, 0, 0);

                // Update the terrain references in the new terrain
                TerrainCollisionDetector newTerrainDetector = pooledTerrain.GetComponent<TerrainCollisionDetector>();
                if (newTerrainDetector != null)
                {
                    newTerrainDetector.pooler = pooler;
                    newTerrainDetector.previousTerrain = previousTerrain;
                    newTerrainDetector.currentTerrain = currentTerrain;
                }
            }
            else
            {
                Debug.LogWarning("Next terrain is null, check TerrainPooler.");
            }
        }
    }
}
