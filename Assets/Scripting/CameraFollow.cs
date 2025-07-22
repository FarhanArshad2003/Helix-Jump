using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ballTransform; 

    void LateUpdate()
    {
        if (ballTransform != null)
        {
            
            transform.position = new Vector3(ballTransform.position.x, transform.position.y, transform.position.z);
        }
    }
}
