using UnityEngine;

public class TotalBalanceManager : MonoBehaviour
{
    private const string TotalBalanceKey = "TotalBalance"; // Key for PlayerPrefs

    public static TotalBalanceManager Instance { get; private set; }

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Adds the session's coin count to the total balance and saves it.
    /// </summary>
    /// <param name="sessionCoins">The coins collected during the current session.</param>
    public void AddToTotalBalance(int sessionCoins)
    {
        int totalBalance = LoadTotalBalance();
        totalBalance += sessionCoins;
        SaveTotalBalance(totalBalance);
    }

    /// <summary>
    /// Saves the total balance to PlayerPrefs.
    /// </summary>
    private void SaveTotalBalance(int balance)
    {
        PlayerPrefs.SetInt(TotalBalanceKey, balance);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Loads the total balance from PlayerPrefs.
    /// </summary>
    /// <returns>The saved total balance.</returns>
    public int LoadTotalBalance()
    {
        return PlayerPrefs.GetInt(TotalBalanceKey, 0); // Default to 0 if no balance is saved
    }
}
