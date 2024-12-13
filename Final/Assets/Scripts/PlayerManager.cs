using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    
    public Player selectedPlayer; // The current selected player

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Optionally persist between scene loads
            Debug.Log("PlayerManager initialized.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSelectedPlayer(Player player)
    {
        selectedPlayer = player;
        Debug.Log("Player Selected: " + selectedPlayer.name);  // Log to confirm player selection
    }
}
