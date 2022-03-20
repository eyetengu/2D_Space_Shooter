using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);      //Load the Game Scene
    }


    void Update()
    {
        
    }
}
