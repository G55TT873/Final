using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float laneDistance = 2f;
    public Transform cameraTransform;
    public float zOffset = -10f;
    public GameObject roadSegmentPrefab;
    public Transform roadStartPosition;
    public float roadSegmentZOffset = 10f;

    private int currentLane = 0;
    private float targetLanePosition;
    private float laneSwitchSpeed = 10f;
    private float roadLength = 100f;
    private GameObject currentRoadSegment;

    private bool canInstantiate = true;
    private float instantiateDelay = 0.5f;

    void Start()
    {
        currentRoadSegment = Instantiate(roadSegmentPrefab, roadStartPosition.position, Quaternion.identity);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        HandleLaneMovement();

        FollowPlayerCamera();
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

    private void FollowPlayerCamera()
    {
        cameraTransform.position = new Vector3(transform.position.x, cameraTransform.position.y, transform.position.z + zOffset);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger") && canInstantiate)
        {
            canInstantiate = false;

            Vector3 newRoadPosition = new Vector3(currentRoadSegment.transform.position.x, currentRoadSegment.transform.position.y, currentRoadSegment.transform.position.z + roadLength + roadSegmentZOffset);
            currentRoadSegment = Instantiate(roadSegmentPrefab, newRoadPosition, Quaternion.identity);

            Destroy(other.gameObject);

            StartCoroutine(ResetInstantiationFlag());
        }
    }

    private IEnumerator ResetInstantiationFlag()
    {
        yield return new WaitForSeconds(instantiateDelay);
        canInstantiate = true;
    }
}
