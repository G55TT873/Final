using UnityEngine;

public class TotalBalanceManager : MonoBehaviour
{
    private const string TotalBalanceKey = "TotalBalance";

    public static TotalBalanceManager Instance { get; private set; }

    private void Awake()
    {
        // Singleton pattern
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


    public void AddToTotalBalance(int sessionCoins)
    {
        int totalBalance = LoadTotalBalance();
        totalBalance += sessionCoins;
        SaveTotalBalance(totalBalance);
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
