using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneend : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene(3);
    }
}