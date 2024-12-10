using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject finishText;
    public GameObject player;
    public GameObject pauseButton; // Reference to your Pause Button
    public float timer = 10f;
    private bool isFinished = false;
    private bool isPaused = false; // Pause state

    private Vector3 playerInitialPosition;

    private void Start()
    {
        finishText.gameObject.SetActive(false);
        playerInitialPosition = player.transform.position;

        // Ensure the game starts unpaused
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (!isFinished && !isPaused)
        {
            timer -= Time.deltaTime;
            timerText.text = "Timer: " + Mathf.Ceil(timer).ToString() + "s";
            if (timer <= 0)
            {
                FinishGame();
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
            pauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Resume"; // Update button text
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            pauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Pause"; // Update button text
        }
    }

    private void FinishGame()
    {
        timer = 0;
        finishText.gameObject.SetActive(true);
        isFinished = true;

        // Optionally disable the player movement
        // player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        // player.GetComponent<PlayerController>().enabled = false;
    }
}
