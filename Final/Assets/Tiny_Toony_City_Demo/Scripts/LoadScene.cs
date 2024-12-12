using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void load()
    {
        SceneManager.LoadScene(1);
    }
    public void SubLoad()
    {
        SceneManager.LoadScene(2);
    }
    public void Restart()
    {
        
        SceneManager.LoadScene(4);
    }

    public void BackMainMenu()
    {

        SceneManager.LoadScene(0);
    }
    public void Shop()
    {

        SceneManager.LoadScene(4);
    }

}
