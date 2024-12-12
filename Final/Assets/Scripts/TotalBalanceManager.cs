using UnityEngine;
using TMPro;

public class TotalBalanceManager : MonoBehaviour
{
    private const string TotalBalanceKey = "TotalBalance";

    public TextMeshProUGUI totalBalanceText;

    public static TotalBalanceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateTotalBalanceUI();
    }


    public void AddToTotalBalance(int sessionCoins)
    {
        int totalBalance = LoadTotalBalance();
        totalBalance += sessionCoins;
        SaveTotalBalance(totalBalance);
        UpdateTotalBalanceUI();
    }


    private void UpdateTotalBalanceUI()
    {
        int totalBalance = LoadTotalBalance();
        if (totalBalanceText != null)
        {
            totalBalanceText.text = "Total Balance: " + totalBalance.ToString();
        }
    }


    private void SaveTotalBalance(int balance)
    {
        PlayerPrefs.SetInt(TotalBalanceKey, balance);
        PlayerPrefs.Save();
    }

    public int LoadTotalBalance()
    {
        return PlayerPrefs.GetInt(TotalBalanceKey, 0);
    }
}
