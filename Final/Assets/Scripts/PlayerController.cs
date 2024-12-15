using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    public float moveSpeed = 5f;
    public float laneDistance = 2f;
    private Player playerData;

    [Header("Power-Up")]
    public float boostedSpeed = 10f;
    public float powerUpDuration = 10f;
    private bool isBoosted = false;

    [Header("Camera")]
    public Transform cameraTransform;
    public float zOffset = -10f;

    [Header("Road Spawn")]
    public GameObject roadSegmentPrefab1;
    public GameObject roadSegmentPrefab2;
    public GameObject roadSegmentPrefab3;
    public Transform roadStartPosition;
    public float roadSegmentZOffset = 10f;

    private bool isSliding = false;

    private Animator animator;

    private int currentLane = 0;
    private float targetLanePosition;
    private float laneSwitchSpeed = 10f;
    private float rotationSpeed = 1f;
    private Quaternion targetRotation;

    private float roadLength = 100f;
    private GameObject currentRoadSegment;

    private bool canInstantiate = true;
    private float instantiateDelay = 0.5f;

    [Header("Jump")]
    public float jumpForce = 4f;
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

    [Header("Total Balance")]
    public TextMeshProUGUI totalBalanceText;
    [Header("Timer")]
    private Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        rb.drag = 0;
        rb.angularDrag = 0;

        animator.SetBool("Running", true);

        currentRoadSegment = Instantiate(roadSegmentPrefab1, roadStartPosition.position, Quaternion.identity);
        gameOverScreen.SetActive(false);

        UpdateCoinText();
        int totalBalance = TotalBalanceManager.Instance.LoadTotalBalance();
        totalBalanceText.text = "Total Balance: " + totalBalance.ToString();
    }

    public void SetPlayerData(Player data)
    {
        playerData = data;

        if (animator != null && data != null && data.animatorController != null)
        {
            animator.runtimeAnimatorController = data.animatorController;
        }
    }

    void Update()
    {
        if (playerData != null)
        {
            HandleLaneMovement();
            HandleJump();
            FollowPlayerCamera();

            // Handle speed boost activation with the B key
            if (Input.GetKeyDown(KeyCode.B))
            {
                StartCoroutine(ActivateSpeedBoost());
            }
        }
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
            animator.SetTrigger("SlideLeft");
        }

        if (Input.GetKeyDown(KeyCode.D) && currentLane < 1)
        {
            currentLane++;
            animator.SetTrigger("SlideRight");
        }

        targetLanePosition = currentLane * laneDistance;
        transform.position = new Vector3(
            Mathf.MoveTowards(transform.position.x, targetLanePosition, laneSwitchSpeed * Time.deltaTime),
            transform.position.y,
            transform.position.z
        );
    }

    private void HandleJump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("Jump");
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
            Debug.Log($"Collision detected with: {other.name}");

            animator.SetBool("Running", false);
            animator.SetTrigger("Fall");

            HandleGameOver();
        }
        else if (other.CompareTag("Coin"))
        {
            CollectCoin(other.gameObject);
        }
        else if (other.CompareTag("SpeedBoost")) // Handle Speed Boost Power-Up
        {
            StartCoroutine(ActivateSpeedBoost());
            Destroy(other.gameObject);
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
        Collectible collectible = coin.GetComponent<CollectibleManager>()?.collectible;

        if (collectible != null)
        {
            Debug.Log($"Collected coin: {coin.name} with value: {collectible.value}");
            coinCount += collectible.value;
            Debug.Log($"New total coin count: {coinCount}");
        }
        else
        {
            Debug.LogWarning($"Coin {coin.name} does not have a Collectible attached!");
        }

        UpdateCoinText();
        Destroy(coin);
    }

    private IEnumerator ActivateSpeedBoost()
    {
        if (isBoosted) yield break; // Prevent overlapping boosts

        isBoosted = true;
        float originalSpeed = moveSpeed;
        moveSpeed = boostedSpeed;

        yield return new WaitForSeconds(powerUpDuration);

        moveSpeed = originalSpeed;
        isBoosted = false;
    }

    private void UpdateCoinText()
    {
        Debug.Log($"Updating coin text: {coinCount}");
        coinText.text = "Coins: " + coinCount.ToString();
    }

    private void HandleGameOver()
    {
        rb.velocity = Vector3.zero;
        moveSpeed = 0f;

        TotalBalanceManager.Instance.AddToTotalBalance(coinCount);

        gameOverScreen.SetActive(true);
        if (timer != null)
        {
            timer.StopTimer();
        }
        StartCoroutine(DisableScriptAfterAnimation());
    }

    private IEnumerator DisableScriptAfterAnimation()
    {
        yield return new WaitForSeconds(1.0f);
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
