using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BallBounceOnPath : MonoBehaviour
{
    public float bounceForce = 10f;
    public float bounceFrequency = 0.5f;
    public float downwardSpeed = 5f;
    public GameObject gameOverText;
    public Text scoreText;
    public GameObject hitEffectPrefab;
    public GameObject deathEffectPrefab;
    public GameObject particleSystemPrefab;
    public GameObject winPanel; // Reference to the win panel
    public Button restartButton;
    public float hitEffectLifetime = 1f; // Lifetime of the hit effect in seconds

    private Rigidbody rb;
    private float lastBounceTime;
    private bool isMouseDown = false;
    private bool isGameOver = false;
    private int score = 0;
    private Vector3 originalScale;
    private bool isGameStarted = false; // Track if the game has started

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastBounceTime = Time.time;
        gameOverText.SetActive(false);
        restartButton.gameObject.SetActive(false);
        winPanel.SetActive(false);
        UpdateScoreText();
        originalScale = transform.localScale;

        // Initially disable ball movement
        
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isGameStarted)
            {
                isGameStarted = true;
                StartGame();
            }
            isMouseDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            Bounce();
        }

        if (isMouseDown)
        {
            BallDown();
        }
        else
        {
            transform.localScale = originalScale;
        }
    }

    void StartGame()
    {
        rb.useGravity = true; // Enable gravity when the game starts
        this.enabled = true; // Enable the script for ball movement
    }

    void BallDown()
    {
        rb.velocity = new Vector3(rb.velocity.x, -downwardSpeed, rb.velocity.z);
        transform.localScale = new Vector3(originalScale.x, originalScale.y * 0.8f, originalScale.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isGameOver)
        {
            return;
        }

        if (collision.gameObject.CompareTag("CylinderPath"))
        {
            if (isMouseDown)
            {
                DestroyPathAndParts(collision.gameObject);
            }
            else
            {
                Bounce();
            }
            InstantiateHitEffect(collision.contacts[0].point);
        }
        else if (collision.gameObject.CompareTag("Danger"))
        {
            if (isMouseDown)
            {
                StartCoroutine(HandleDeath(collision.contacts[0].point));
            }
            else
            {
                Bounce();
            }
            InstantiateHitEffect(collision.contacts[0].point);
        }
        else if (collision.gameObject.CompareTag("WinPlaform"))
        {
            HandleWin();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (isGameOver)
        {
            return;
        }

        if (collision.gameObject.CompareTag("CylinderPath"))
        {
            if (isMouseDown)
            {
                DestroyPathAndParts(collision.gameObject);
            }
            else
            {
                Bounce();
            }
            InstantiateHitEffect(collision.contacts[0].point);
        }
        else if (collision.gameObject.CompareTag("Danger"))
        {
            if (isMouseDown)
            {
                StartCoroutine(HandleDeath(collision.contacts[0].point));
            }
            else
            {
                Bounce();
            }
            InstantiateHitEffect(collision.contacts[0].point);
        }
    }

    void HandleWin()
    {
        if (particleSystemPrefab != null)
        {
            GameObject win = Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
            Destroy(win, hitEffectLifetime);
        }

        Debug.Log("Player has hit the winning platform!");
        StartCoroutine(ShowWinPanelAfterDelay(2f)); // Show win panel after 2 seconds
    }

    IEnumerator ShowWinPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        winPanel.SetActive(true);
    }

    void DestroyPathAndParts(GameObject path)
    {
        foreach (Transform child in path.transform)
        {
            if (child.CompareTag("CylinderParts") || child.CompareTag("Danger"))
            {
                Destroy(child.gameObject);
            }
        }
        Destroy(path);
        IncrementScore();
    }

    void Bounce()
    {
        if (Time.time - lastBounceTime >= bounceFrequency)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            lastBounceTime = Time.time;
        }
        transform.localScale = originalScale;
    }

    IEnumerator HandleDeath(Vector3 position)
    {
        isGameOver = true;

        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        if (deathEffectPrefab != null)
        {
            GameObject death = Instantiate(deathEffectPrefab, position, Quaternion.identity);
            Destroy(death, hitEffectLifetime);
        }

        yield return new WaitForSeconds(1f);
        gameOverText.SetActive(true);

        if (gameOverText.GetComponent<Text>() != null)
        {
            gameOverText.GetComponent<Text>().text = "Game Over\nScore: " + score;
        }

        restartButton.gameObject.SetActive(true);
    }

    void IncrementScore()
    {
        score++;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void InstantiateHitEffect(Vector3 position)
    {
        if (hitEffectPrefab != null)
        {
            GameObject hitEffect = Instantiate(hitEffectPrefab, position, Quaternion.identity);
            Destroy(hitEffect, hitEffectLifetime);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
