using UnityEngine;

public class CylinderSpawner : MonoBehaviour
{
    public GameObject cylinderPrefab;
    public GameObject winningPlatformPrefab; // Reference to the winning platform prefab
    public int numberOfCylinders = 100;
    public float distanceBetweenCylinders = 2f;
    public float initialRotationAngle = 5f;
    public float subsequentRotationAngle = 10f;

    void Start()
    {
        SpawnCylinders();
    }

    void SpawnCylinders()
    {
        Vector3 spawnPosition = transform.position;

        for (int i = 0; i < numberOfCylinders; i++)
        {
            float rotationAngle = (i / 20) % 2 == 0 ? initialRotationAngle : subsequentRotationAngle;
            Quaternion spawnRotation = Quaternion.Euler(0f, rotationAngle * i, 0f);
            Instantiate(cylinderPrefab, spawnPosition, spawnRotation);
            spawnPosition.y -= distanceBetweenCylinders;

            Debug.Log("Cylinder " + i + " spawned at position: " + spawnPosition + " with rotation: " + spawnRotation.eulerAngles.y);
        }

        // Adjust the spawn position for the winning platform to be below the last cylinder path
        spawnPosition.y -= distanceBetweenCylinders;

        // Log the position for debugging
        Debug.Log("Final position for winning platform: " + spawnPosition);

        // Spawn the winning platform after all cylinders
        if (winningPlatformPrefab != null)
        {
            Instantiate(winningPlatformPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Spawning winning platform at position: " + spawnPosition);
        }
        else
        {
            Debug.LogError("Winning platform prefab is not assigned!");
        }
    }
}
