using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; } // Singleton reference
    public Player selectedPlayer; // The selected player
    
    void Awake()
    {
        // Ensure only one instance of PlayerManager exists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make sure the object persists across scenes
            Debug.Log("PlayerManager instance set.");
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate if it exists
            Debug.Log("Duplicate PlayerManager destroyed.");
        }
    }
    public void SetSelectedPlayer(Player player)
    {
        selectedPlayer = player;
    }
}
