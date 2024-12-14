using UnityEngine;

public class CharacterPreviewer : MonoBehaviour
{
    [Header("Character Settings")]
    public GameObject[] characterPrefabs;
    public Transform previewSpawnPoint;

    private int currentIndex = 0;
    private GameObject currentCharacterInstance;

    void Start()
    {
        if (characterPrefabs.Length > 0)
        {
            ShowCharacter(currentIndex);
        }
        else
        {
            Debug.LogWarning("No character prefabs assigned!");
        }
    }


    public void ShowNextCharacter()
    {
        if (characterPrefabs.Length == 0) return;

        currentIndex = (currentIndex + 1) % characterPrefabs.Length;
        ShowCharacter(currentIndex);
    }


    public void ShowPreviousCharacter()
    {
        if (characterPrefabs.Length == 0) return;

        currentIndex = (currentIndex - 1 + characterPrefabs.Length) % characterPrefabs.Length;
        ShowCharacter(currentIndex);
    }


    private void ShowCharacter(int index)
    {
        if (currentCharacterInstance != null)
        {
            Destroy(currentCharacterInstance);
        }

        currentCharacterInstance = Instantiate(characterPrefabs[index], previewSpawnPoint.position, Quaternion.identity);
        currentCharacterInstance.transform.SetParent(previewSpawnPoint);
    }
}
