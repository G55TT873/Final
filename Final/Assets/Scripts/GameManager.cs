using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadGameScene()
{
    // Load the game scene (replace "GameScene" with your actual scene name)
    UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterScene");

    // Ensure PlayerManager has the selected player
    if (PlayerManager.Instance.selectedPlayer != null)
    {
        // Instantiate the selected player in the scene
        Instantiate(PlayerManager.Instance.selectedPlayer.playerPrefab, Vector3.zero, Quaternion.identity);
    }
    else
    {
        Debug.LogError("Selected player is null!");
    }
}





    
}
