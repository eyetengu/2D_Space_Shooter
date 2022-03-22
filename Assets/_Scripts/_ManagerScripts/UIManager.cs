using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager _gameManager;

    public Text _scoreUI;
    public Text _gameOverText;
    public Text _restartText;

    [SerializeField]
    private Sprite[] _liveSprites;
    private Image _livesImg;

    [SerializeField]
    private Sprite[] _shieldSprites;
    private Image _shieldsImg;

    public int fontSize = 24;


    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        _livesImg = GameObject.Find("Health_img").GetComponent<Image>();

        _shieldsImg = GameObject.Find("Shields_img").GetComponent<Image>();

        _scoreUI = GameObject.Find("Score_text").GetComponent<Text>();

        _gameOverText = GameObject.Find("GameOver_text").GetComponent<Text>();
        _restartText = GameObject.Find("Restart_text").GetComponent<Text>();
    }

    public void UpdateHealthUI(int _lives)
    {
        _livesImg.sprite = _liveSprites[_lives];
        if(_lives < 1)
        {
            GameOverSequence();
        }
    }

    public void UpdateShieldsUI(int _shields)
    {
        Debug.Log("UIManager.cs- Shields");
        _shieldsImg.sprite = _shieldSprites[_shields];
        return;
    }










    public void UpdateScoreUI(int _score)
    {
        _scoreUI.text = _score.ToString();
    }

// CoRoutines

    public void GameOverSequence()
    {
        _gameOverText.text = "Game Over!";
        _restartText.text = "Press R to Restart";
        StartCoroutine(GameOverFlickerRoutine());
        _gameManager.GameOver();
    }

    IEnumerator GameOverFlickerRoutine()
    {
        _gameOverText.color = Color.red;
        _gameOverText.fontSize = 18;
        _gameOverText.text = "GAME OVER";
        yield return new WaitForSeconds(.5f);

        _gameOverText.color = Color.green;
                _gameOverText.fontSize = 36;
        _gameOverText.text = "GAME OVER";
        yield return new WaitForSeconds(.5f);
        
        _gameOverText.color = Color.blue;
                _gameOverText.fontSize = 72;
        _gameOverText.text = "GAME OVER";
        yield return new WaitForSeconds(.5f); //72 36 18
        
        GameOverSequence();

    }

}
