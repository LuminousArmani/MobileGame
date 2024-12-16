using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGenerator : MonoBehaviour
{
    public GameObject[] brickPrefabs; // Array of different brick prefabs
    public Vector2 minSize;
    public Vector2 maxSize;
    public int rows = 5;
    public int columns = 10;
    public float spacing = 0.1f;

    void Start()
    {
        GenerateBricks();
    }

    public void GenerateBricks()
    {
        // Clear previous bricks
        ClearBricks();

        // Starting position
        float currentY = 0;
        float rowHeight = maxSize.y + spacing;

        for (int row = 0; row < rows; row++)
        {
            float currentX = 0;

            for (int col = 0; col < columns; col++)
            {
                // Randomly pick a brick prefab from the array
                GameObject brickPrefab = brickPrefabs[Random.Range(0, brickPrefabs.Length)];

                // Instantiate brick
                GameObject brick = Instantiate(brickPrefab, Vector3.zero, Quaternion.identity, transform);

                // Assign random size
                float randomWidth = Random.Range(minSize.x, maxSize.x);
                float randomHeight = Random.Range(minSize.y, maxSize.y);
                brick.transform.localScale = new Vector3(randomWidth, randomHeight, 1);

                // Calculate position
                float halfWidth = randomWidth / 2f;
                float halfHeight = randomHeight / 2f;

                Vector3 position = new Vector3(currentX + halfWidth, currentY - halfHeight, 0);
                brick.transform.localPosition = position;

                // Update X position for next brick
                currentX += randomWidth + spacing;
            }

            // Move to next row (downward)
            currentY -= rowHeight;
        }
    }

    // Helper method to clear previous bricks
    private void ClearBricks()
    {
        // Iterate over all children and destroy them
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

}
