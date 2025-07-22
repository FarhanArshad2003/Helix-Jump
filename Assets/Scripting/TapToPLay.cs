using UnityEngine;
using UnityEngine.UI;

public class TapToPlay : MonoBehaviour
{
    public Text tapToPlayText;
    public float rotationSpeed = 30f; // Speed of rotation
    public float minRotationZ = -1f; // Minimum rotation angle
    public float maxRotationZ = 2f;  // Maximum rotation angle
    public GameObject ball;          // Reference to the ball
    public CylinderSpawner cylinderSpawner; // Reference to the cylinder spawner
    private bool isGameStarted = false; // Track if the game has started
    private Rigidbody ballRigidbody; // Reference to the ball's Rigidbody

    void Start()
    {
        if (ball != null)
        {
            ballRigidbody = ball.GetComponent<Rigidbody>();
            // Ensure gravity is initially disabled
            if (ballRigidbody != null)
            {
                ballRigidbody.useGravity = false;
            }
        }
    }

    void Update()
    {
        // Rotate the text around the Z-axis
        if (tapToPlayText != null && !isGameStarted)
        {
            float rotationZ = Mathf.PingPong(Time.time * rotationSpeed, maxRotationZ - minRotationZ) + minRotationZ;
            tapToPlayText.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        }

        // Check for mouse click to start the game
        if (Input.GetMouseButtonDown(0) && !isGameStarted)
        {
            isGameStarted = true;
            StartGame();
        }
    }

    void StartGame()
    {
        // Hide the tap to play text
        if (tapToPlayText != null)
        {
            tapToPlayText.gameObject.SetActive(false);
        }

        // Enable the ball movement and cylinder spawning
        if (ball != null)
        {
            BallBounceOnPath ballScript = ball.GetComponent<BallBounceOnPath>();
            if (ballScript != null)
            {
                ballScript.enabled = true;
            }

            if (ballRigidbody != null)
            {
                ballRigidbody.useGravity = true; // Enable gravity when the game starts
            }
        }

      
    }
}
