using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject playerPrefab; // The GameObject that will contain the PlayerController
    public GameObject spawnPoint; // Where the player will spawn in the game scene
    public Transform playerParent; // The parent of the 3D player model

    void Start()
    {
        // Ensure the PlayerManager instance is available (it should be persistent)
        if (PlayerManager.Instance == null)
        {
            Debug.LogError("PlayerManager instance is null. Ensure PlayerManager persists across scenes.");
            return;
        }

        // Ensure a player is selected in the PlayerManager
        if (PlayerManager.Instance.selectedPlayer == null)
        {
            Debug.LogError("No player selected in PlayerManager. Ensure a player is selected in the Shop Scene.");
            return;
        }

        Debug.Log("Selected Player: " + PlayerManager.Instance.selectedPlayer.name);

        // Instantiate player at the spawn point
        GameObject player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        Debug.Log("Player instantiated at: " + spawnPoint.transform.position);

        // Instantiate the selected player model
        GameObject selectedPlayerPrefab = PlayerManager.Instance.selectedPlayer.playerPrefab;
        if (selectedPlayerPrefab != null)
        {
            GameObject playerModel = Instantiate(selectedPlayerPrefab, player.transform);
            playerModel.transform.localPosition = Vector3.zero;
            Debug.Log("Player model instantiated: " + playerModel.name);
        }
        else
        {
            Debug.LogError("Selected player prefab is null. Check the assigned player prefab in PlayerManager.");
        }

        // Set up the player controller
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            Debug.Log("Setting player data on PlayerController.");
            playerController.SetPlayerData(PlayerManager.Instance.selectedPlayer);
        }
        else
        {
            Debug.LogError("PlayerController not found on the instantiated player.");
        }
    }
}
