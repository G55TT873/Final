using UnityEngine;

public class GhostChase : MonoBehaviour
{
    public Transform player;
    public float distanceOffset = 4f;

    void Start()
    {
        player = GameObject.Find("Player(Clone)")?.transform;

        if (player == null)
        {
            Debug.LogError("Player(Clone) not found in the scene!");
            return;
        }

        UpdateGhostPosition();
    }

    void Update()
    {
        player = GameObject.Find("Player(Clone)")?.transform;
        if (player == null) return;

        UpdateGhostPosition();

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    private void UpdateGhostPosition()
    {
        transform.position = new Vector3(
            player.position.x,
            transform.position.y,
            player.position.z - distanceOffset
        );
    }
}
