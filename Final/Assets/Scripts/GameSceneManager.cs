using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject spawnPoint;
    public Transform playerParent;

   void Start()
{
    if (PlayerManager.Instance != null && PlayerManager.Instance.selectedPlayer != null)
    {
        GameObject player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);

        GameObject selectedPlayerPrefab = PlayerManager.Instance.selectedPlayer.playerPrefab;
        if (selectedPlayerPrefab != null)
        {
            GameObject playerModel = Instantiate(selectedPlayerPrefab, player.transform);
            playerModel.transform.localPosition = Vector3.zero;
        }

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
