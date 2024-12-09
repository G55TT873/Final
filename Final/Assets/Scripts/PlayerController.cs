using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    public float moveSpeed = 5f;
    public float laneDistance = 2f;
    [Header("Camera")]
    public Transform cameraTransform;
    public float zOffset = -10f;
    [Header("Road Spawn")]
    public GameObject roadSegmentPrefab1;
    public GameObject roadSegmentPrefab2;
    public GameObject roadSegmentPrefab3;
    public Transform roadStartPosition;
    public float roadSegmentZOffset = 10f;

    

    private int currentLane = 0;
    private float targetLanePosition;
    private float laneSwitchSpeed = 10f;
    
    private float roadLength = 100f;
    private GameObject currentRoadSegment;

    private bool canInstantiate = true;
    private float instantiateDelay = 0.5f;

    [Header("Jump")]
    public float jumpForce = 5f;
    private bool isGrounded = true;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody rb;

    [Header("Game Over")]
    public GameObject gameOverScreen;

    [Header("Coin")]
    public int coinCount = 0;
    public TextMeshProUGUI coinText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 0;
        rb.angularDrag = 0;

        currentRoadSegment = Instantiate(roadSegmentPrefab1, roadStartPosition.position, Quaternion.identity);
        gameOverScreen.SetActive(false);

        UpdateCoinText();
    }

    void Update()
    {
        HandleLaneMovement();
        HandleJump();
        FollowPlayerCamera();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveSpeed);
    }

    private void HandleLaneMovement()
    {
        if (Input.GetKeyDown(KeyCode.A) && currentLane > -1)
        {
            currentLane--; 
        }

        if (Input.GetKeyDown(KeyCode.D) && currentLane < 1)
        {
            currentLane++;
        }

        targetLanePosition = currentLane * laneDistance;

        transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, targetLanePosition, laneSwitchSpeed * Time.deltaTime), transform.position.y, transform.position.z);
    }

    private void HandleJump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FollowPlayerCamera()
    {
        cameraTransform.position = new Vector3(transform.position.x, cameraTransform.position.y, transform.position.z + zOffset);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger") && canInstantiate)
        {
            canInstantiate = false;

            GameObject selectedRoadSegment = SelectRandomRoadSegment();

            Vector3 newRoadPosition = new Vector3(currentRoadSegment.transform.position.x, currentRoadSegment.transform.position.y, currentRoadSegment.transform.position.z + roadLength + roadSegmentZOffset);
            currentRoadSegment = Instantiate(selectedRoadSegment, newRoadPosition, Quaternion.identity);

            Destroy(currentRoadSegment, 20f);

            Destroy(other.gameObject);

            StartCoroutine(ResetInstantiationFlag());
        }
        else if (other.CompareTag("Obstacle"))
        {
            HandleGameOver();
        }
        else if (other.CompareTag("Coin"))
        {
            CollectCoin(other.gameObject);
        }
    }

    private GameObject SelectRandomRoadSegment()
    {
        int randomIndex = Random.Range(0, 3);
        if (randomIndex == 0) return roadSegmentPrefab1;
        if (randomIndex == 1) return roadSegmentPrefab2;
        return roadSegmentPrefab3;
    }

    private void CollectCoin(GameObject coin)
    {
        coinCount++;

        UpdateCoinText();

        Destroy(coin);
    }

    private void UpdateCoinText()
    {
        coinText.text = "Coins: " + coinCount.ToString();
    }

    private void HandleGameOver()
    {
        rb.velocity = Vector3.zero;
        moveSpeed = 0f;

        gameOverScreen.SetActive(true);

        this.enabled = false;
    }

    private IEnumerator ResetInstantiationFlag()
    {
        yield return new WaitForSeconds(instantiateDelay);
        canInstantiate = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
