using UnityEngine;
using TMPro;

public class TotalBalanceDisplay : MonoBehaviour
{
    private TextMeshProUGUI totalBalanceText;

    private void Start()
    {
        GameObject totalBalanceObject = GameObject.Find("Total Balance");
        if (totalBalanceObject != null)
        {
            totalBalanceText = totalBalanceObject.GetComponent<TextMeshProUGUI>();
        }

        if (totalBalanceText == null)
        {
            Debug.LogError("No TextMeshProUGUI component found with the name 'Total Balance' in the scene.");
        }
    }

    private void Update()
    {

        if (totalBalanceText != null)
        {
            UpdateTotalBalanceUI();
        }
    }


    private void UpdateTotalBalanceUI()
    {
        int totalBalance = TotalBalanceManager.Instance.LoadTotalBalance();
        totalBalanceText.text = "Total Balance: " + totalBalance.ToString();
    }
}
