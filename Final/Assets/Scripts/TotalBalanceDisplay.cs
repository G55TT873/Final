using UnityEngine;
using TMPro;

public class TotalBalanceDisplay : MonoBehaviour
{
    private TextMeshProUGUI totalBalanceText; // Private UI Text to display the total balance

    private void Start()
    {
        // Try to find the TextMeshProUGUI component with the name "Total Balance" in the scene
        GameObject totalBalanceObject = GameObject.Find("Total Balance");
        if (totalBalanceObject != null)
        {
            totalBalanceText = totalBalanceObject.GetComponent<TextMeshProUGUI>();
        }

        // If the TextMeshProUGUI component is found, update the UI with the total balance
        if (totalBalanceText != null)
        {
            UpdateTotalBalanceUI();
        }
        else
        {
            Debug.LogError("No TextMeshProUGUI component found with the name 'Total Balance' in the scene.");
        }
    }

    /// <summary>
    /// Updates the total balance UI text.
    /// </summary>
    private void UpdateTotalBalanceUI()
    {
        int totalBalance = TotalBalanceManager.Instance.LoadTotalBalance();
        if (totalBalanceText != null)
        {
            totalBalanceText.text = "Total Balance: " + totalBalance.ToString();
        }
    }
}
