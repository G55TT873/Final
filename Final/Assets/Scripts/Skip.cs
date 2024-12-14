using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void SkipButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);

    }
}
