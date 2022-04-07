using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;   

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
