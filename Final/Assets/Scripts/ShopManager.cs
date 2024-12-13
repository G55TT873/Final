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
    // Ensure that available characters are properly set
    if (availabeCharacters == null || availabeCharacters.Length == 0)
    {
        Debug.LogError("No characters available.");
        return;
    }

    // Ensure PlayerManager is properly initialized before accessing it
    if (PlayerManager.Instance == null)
    {
        Debug.LogError("PlayerManager is not initialized.");
        return;
    }

    // Set the selected player in PlayerManager
    PlayerManager.Instance.SetSelectedPlayer(availabeCharacters[currentIndex]);

    // Ensure GameManager is properly initialized
    if (GameManager.Instance == null)
    {
        Debug.LogError("GameManager is not initialized.");
        return;
    }

    // Load the game scene
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
