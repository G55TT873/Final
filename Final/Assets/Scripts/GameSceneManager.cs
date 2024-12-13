using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject playerPrefab; // The existing GameObject that contains PlayerController (empty GameObject)
    public GameObject spawnPoint;   // The spawn point to position the player
    public Transform playerParent;  // Parent for the 3D model (the existing Player GameObject)

   void Start()
{
    if (PlayerManager.Instance != null && PlayerManager.Instance.selectedPlayer != null)
    {
        // Instantiate the player (this is the empty GameObject with PlayerController attached)
        GameObject player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);

        // Now instantiate the selected player prefab (this is the 3D model with components) inside the player GameObject
        GameObject selectedPlayerPrefab = PlayerManager.Instance.selectedPlayer.playerPrefab;
        if (selectedPlayerPrefab != null)
        {
            GameObject playerModel = Instantiate(selectedPlayerPrefab, player.transform);
            playerModel.transform.localPosition = Vector3.zero; // Set model to the root of the player GameObject
        }

        // Set the PlayerController with the selected player data
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.SetPlayerData(PlayerManager.Instance.selectedPlayer);
        }
        else
        {
            Debug.LogError("PlayerController not found on the instantiated player.");
        }
    }
    else
    {
        Debug.LogError("No player selected.");
    }
}

}
