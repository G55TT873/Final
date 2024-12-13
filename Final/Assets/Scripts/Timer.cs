using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Assign this in the Inspector
    private float elapsedTime = 0f;
    private bool isRunning = true; // Control whether the timer is running

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime; // Increment elapsed time
            UpdateTimerUI(); // Update the timer display
        }
    }

    /// <summary>
    /// Updates the timer text to display elapsed time in MM:SS format.
    /// </summary>
    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60); // Get the minutes
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // Get the remaining seconds
        timerText.text = $"{minutes:D2}:{seconds:D2}"; // Format as MM:SS
    }

    /// <summary>
    /// Stops the timer.
    /// </summary>
    public void StopTimer()
    {
        isRunning = false;
    }

    /// <summary>
    /// Resets the timer to 0.
    /// </summary>
    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerUI();
    }
}
