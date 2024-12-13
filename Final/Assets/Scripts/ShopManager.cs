using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopManager : MonoBehaviour
{
    public Player[] availabeCharacters;
    public Image characterImage;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI priceText;

    public int currentIndex;



    public void Start()
    {
        DisplayCharacter();
    }

    public void NextCharacter()
    {
        currentIndex = (currentIndex + 1 ) % availabeCharacters.Length;
        DisplayCharacter();
    }

    public void PreviousCharacter()
    {
        currentIndex = (currentIndex - 1 + availabeCharacters.Length) % availabeCharacters.Length;
        DisplayCharacter(); 
    }


    public void SelectCharacter()
    {
        // Ensure PlayerManager is properly initialized
        if (PlayerManager.Instance == null || availabeCharacters == null || availabeCharacters.Length == 0)
        {
            Debug.LogError("PlayerManager or available characters are not properly initialized.");
            return;
        }

        // Debug log to verify the selected character is valid
        Debug.Log("Selected character: " + availabeCharacters[currentIndex].name);

        // Set the selected player in PlayerManager
        PlayerManager.Instance.SetSelectedPlayer(availabeCharacters[currentIndex]);

        // Ensure GameManager is properly initialized
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager is not initialized.");
            return;
        }

        // Debug log to confirm game scene is being loaded
        Debug.Log("Loading game scene...");
        GameManager.Instance.LoadGameScene();
    }




    public void DisplayCharacter()
    {
        Player currentPlayer = availabeCharacters[currentIndex];
        characterImage.sprite = currentPlayer.skin;
        characterNameText.text = currentPlayer.playerName;
        priceText.text = $"Price: {currentPlayer.price}";
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterScene");
    }

    
}
