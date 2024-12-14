using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public Player[] availabeCharacters;
    public Image characterImage;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI priceText;
    public Button buyButton;
    public Button selectCharacterButton;
    public Button selectCharacter2Button;

    public int currentIndex;

    private void Start()
    {
        DisplayCharacter();
        UpdateButtons();
    }

    public void NextCharacter()
    {
        currentIndex = (currentIndex + 1) % availabeCharacters.Length;
        DisplayCharacter();
        UpdateButtons();
    }

    public void PreviousCharacter()
    {
        currentIndex = (currentIndex - 1 + availabeCharacters.Length) % availabeCharacters.Length;
        DisplayCharacter();
        UpdateButtons();
    }

    public void SelectCharacter()
    {
        if (!IsCharacterOwned(availabeCharacters[currentIndex]))
        {
            Debug.LogError("You must own this character to play!");
            return;
        }

        PlayerManager.Instance?.SetSelectedPlayer(availabeCharacters[currentIndex]);
        SceneManager.LoadScene(5);
    }

    public void SelectCharacter2()
    {
        if (!IsCharacterOwned(availabeCharacters[currentIndex]))
        {
            Debug.LogError("You must own this character to play!");
            return;
        }

        PlayerManager.Instance?.SetSelectedPlayer(availabeCharacters[currentIndex]);
        SceneManager.LoadScene(6);
    }

    public void BuyCharacter()
    {
        Player currentPlayer = availabeCharacters[currentIndex];

        if (IsCharacterOwned(currentPlayer))
        {
            Debug.Log("Character is already owned.");
            return;
        }

        int totalBalance = TotalBalanceManager.Instance.LoadTotalBalance();
        if (totalBalance >= currentPlayer.price)
        {
            TotalBalanceManager.Instance.AddToTotalBalance(-currentPlayer.price);

            SaveCharacterAsOwned(currentPlayer);

            priceText.text = "Owned";
            buyButton.interactable = false;

            UpdateButtons();
        }
        else
        {
            Debug.Log("Not enough coins to purchase this character.");
        }
    }

    private void DisplayCharacter()
    {
        Player currentPlayer = availabeCharacters[currentIndex];
        characterImage.sprite = currentPlayer.skin;
        characterNameText.text = currentPlayer.playerName;

        if (IsCharacterOwned(currentPlayer))
        {
            priceText.text = "Owned";
            buyButton.interactable = false;
        }
        else
        {
            priceText.text = $"Price: {currentPlayer.price}";
            buyButton.interactable = true;
        }
    }

    private bool IsCharacterOwned(Player character)
    {
        return PlayerPrefs.GetInt($"Character_{character.playerName}_Owned", 0) == 1;
    }

    private void SaveCharacterAsOwned(Player character)
    {
        PlayerPrefs.SetInt($"Character_{character.playerName}_Owned", 1);
        PlayerPrefs.Save();
    }

    private void UpdateButtons()
    {
        bool isCurrentCharacterOwned = IsCharacterOwned(availabeCharacters[currentIndex]);

        selectCharacterButton.interactable = isCurrentCharacterOwned;
        selectCharacter2Button.interactable = isCurrentCharacterOwned;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("CharacterScene");
    }
}
