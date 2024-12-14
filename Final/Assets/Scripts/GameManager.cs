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
    UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterScene");

    if (PlayerManager.Instance.selectedPlayer != null)
    {
        Instantiate(PlayerManager.Instance.selectedPlayer.playerPrefab, Vector3.zero, Quaternion.identity);
    }
    else
    {
        Debug.LogError("Selected player is null!");
    }
}





    
}
