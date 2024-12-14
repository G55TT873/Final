using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : MonoBehaviour
{
    public Transform player;
    private Vector3 initialOffset;

    void Start()
    {
        if (player != null)
        {
            initialOffset = transform.position - player.position;
        }
        else
        {
            Debug.LogError("Player not assigned");
        }
    }

    void Update()
    {
          
        float playerSpeed = player.GetComponent<Rigidbody>()?.velocity.magnitude ?? 0;

        float ghostSpeed = playerSpeed * 1.0f;

        Vector3 direction = (player.position + initialOffset - transform.position).normalized;
        transform.position += direction * ghostSpeed * Time.deltaTime;

        //optional to rotate the ghost to face the player
        transform.LookAt(player);
    
    }
}

