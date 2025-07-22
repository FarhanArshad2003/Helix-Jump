using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    public float rotationSpeed = 100f; // Rotation speed in degrees per second

    void Update()
    {
        // Calculate the rotation amount for this frame
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Rotate the cylinder around the Y-axis
        transform.Rotate(Vector3.up, rotationAmount);
    }
}
