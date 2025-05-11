using UnityEngine;
using System.Collections.Generic;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public Terrain terrain;
    public int treeCount = 1000;
    public BoxCollider lakeVolume;  // Assign your lake's BoxCollider here

    void Start()
    {
        PlaceTreesRandomly();
    }

    void PlaceTreesRandomly()
    {
        if (treePrefab == null || terrain == null || lakeVolume == null)
        {
            Debug.LogError("Assign treePrefab, terrain, and lakeVolume.");
            return;
        }

        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainPos = terrain.transform.position;
        float width = terrainData.size.x;
        float length = terrainData.size.z;

        List<Vector3> treePositions = new List<Vector3>();

        // Generate random positions for the trees
        for (int i = 0; i < treeCount; i++)
        {
            float x = Random.Range(0f, width);
            float z = Random.Range(0f, length);
            float worldX = terrainPos.x + x;
            float worldZ = terrainPos.z + z;
            float y = terrain.SampleHeight(new Vector3(worldX, 0, worldZ)) + terrainPos.y;
            Vector3 spawnPos = new Vector3(worldX, y, worldZ);

            // Add all trees to the list (without filtering)
            treePositions.Add(spawnPos);
        }

        // Now filter out the trees that are inside the lake volume
        List<Vector3> validTreePositions = new List<Vector3>();
        foreach (Vector3 position in treePositions)
        {
            if (!lakeVolume.bounds.Contains(position))
            {
                validTreePositions.Add(position);
            }
        }

        // Instantiate only the valid tree positions
        foreach (Vector3 validPosition in validTreePositions)
        {
            Instantiate(treePrefab, validPosition, Quaternion.identity, transform);
        }

        Debug.Log($"Placed {validTreePositions.Count} trees. Skipped {treePositions.Count - validTreePositions.Count} inside lake.");
    }
}
