using UnityEngine;

public class CharacterPreviewer : MonoBehaviour
{
    [Header("Character Settings")]
    public GameObject[] characterPrefabs; // Array to store character prefabs
    public Transform previewSpawnPoint; // Where the characters will be displayed

    private int currentIndex = 0; // Tracks the current character index
    private GameObject currentCharacterInstance; // Holds the currently spawned character

    void Start()
    {
        if (characterPrefabs.Length > 0)
        {
            ShowCharacter(currentIndex); // Display the first character at the start
        }
        else
        {
            Debug.LogWarning("No character prefabs assigned!");
        }
    }

    /// <summary>
    /// Shows the next character in the array.
    /// </summary>
    public void ShowNextCharacter()
    {
        if (characterPrefabs.Length == 0) return;

        currentIndex = (currentIndex + 1) % characterPrefabs.Length; // Cycle to the next index
        ShowCharacter(currentIndex);
    }

    /// <summary>
    /// Shows the previous character in the array.
    /// </summary>
    public void ShowPreviousCharacter()
    {
        if (characterPrefabs.Length == 0) return;

        currentIndex = (currentIndex - 1 + characterPrefabs.Length) % characterPrefabs.Length; // Cycle to the previous index
        ShowCharacter(currentIndex);
    }

    /// <summary>
    /// Instantiates and displays the character at the given index.
    /// </summary>
    /// <param name="index">The index of the character to display.</param>
    private void ShowCharacter(int index)
    {
        // Destroy the current character instance if it exists
        if (currentCharacterInstance != null)
        {
            Destroy(currentCharacterInstance);
        }

        // Instantiate the new character at the preview spawn point
        currentCharacterInstance = Instantiate(characterPrefabs[index], previewSpawnPoint.position, Quaternion.identity);
        currentCharacterInstance.transform.SetParent(previewSpawnPoint); // Optional: parent the character for organization
    }
}
