using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
//External Components
    private GameManager _gameManager;

//UI Stats
    public Text _scoreUI;
    public Text _gameOverText;
    public Text _restartText;
    public Text _gamePlayMessages;
    public Text _ammoText;
//FontSizes
    public int fontSize = 24;

//Sprites
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Sprite[] _secondaryFireSprites;

    private Image _livesImg;
    private Image _secondaryFireImg;

//Shields
    [SerializeField]
    private Sprite[] _shieldSprites;
    [SerializeField]
    private Image _shieldsImg;

//Fuel System
    [SerializeField]
    private Slider _fuelLevelSlider;

    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        { 
            Debug.LogError("UIManager.cs- Game Manager is null"); 
        }

        _livesImg = GameObject.Find("Health_img").GetComponent<Image>();

        //_secondaryFireImg = GameObject.Find("SecondaryFire_img").GetComponent<Image>();
        
        _shieldsImg = GameObject.Find("Shields_img").GetComponent<Image>();
        if(_shieldsImg == null)
        {
            Debug.LogError("UIManager- Shields Unavailable");
        }

        _scoreUI = GameObject.Find("Score_text").GetComponent<Text>();
        _gameOverText = GameObject.Find("GameOver_text").GetComponent<Text>();
        _restartText = GameObject.Find("Restart_text").GetComponent<Text>();
        _gamePlayMessages = GameObject.Find("GamePlayMessages_text").GetComponent<Text>();
        _ammoText = GameObject.Find("Ammo_text").GetComponent<Text>();
        _fuelLevelSlider = GameObject.Find("FuelLevel_slider").GetComponent<Slider>();
        if(_fuelLevelSlider == null)
        { Debug.Log("UIManager- Fuel Level Indicator Missing");}


        //_fuelCellsSlider = GameObject.Find("FuelCell_Slider").GetComponent<Slider>();
        //if(_fuelCellsSlider == null)        { Debug.LogError("UIManager.cs- Slider not found"); }
    }
//Systems Check
    public void UpdateHealthUI(int _lives)
    {
        _livesImg.sprite = _liveSprites[_lives];
        if(_lives < 1)
        {
            GameOverSequence(1);
        }
    }
    public void UpdateShieldsUI(int _shields)
    {
        //Debug.Log("UIManager.cs- Shields");
        _shieldsImg.sprite = _shieldSprites[_shields];
        return;
    }

    public void UpdateScoreUI(int _score)
    {
        _scoreUI.text = _score.ToString();
    }
    public void AmmoCountUpdate(int _ammoCount, int _maxAmmoCount)
    {
        if(_ammoCount < 1)
        {
            GamePlayMessages(1);
        }
        else if (_ammoCount > 0)
        {
            GamePlayMessages(0);
        }
        string ammo = _ammoCount.ToString();
        string maxAmmo = _maxAmmoCount.ToString();
        _ammoText.text = ammo + "/" + maxAmmo;
        //Debug.Log("Ready for Ammo Count");
    }

    public void FuelManager(float _fuelLevel) //_fuelCells
    {
        Debug.Log("UIManager- " + _fuelLevel);
    }
    
//Message Center
    public void GamePlayMessages(int _gamePlayMessenger)
    {
        switch(_gamePlayMessenger)
        {
            case 0:
                _gamePlayMessages.text = "";
                break;
            case 1:
                _gamePlayMessages.text = "RELOAD";
                break;
            case 2:
                _gamePlayMessages.text = "Well Done. You Won!";
                break;
            default:
                break;
        }
    }    
    public void GameOverSequence(int gameOverInt)
    {
        _gamePlayMessages.text = "";
        _restartText.text = "";

        switch(gameOverInt)
        {
            case 1:
                _gameOverText.text = "Game Over!";
                _restartText.text = "Press R to Restart";
                StartCoroutine(GameOverFlickerRoutine());
                break;
            case 2:
                _gameOverText.fontSize = 58;
                _gameOverText.text = "You Won!";
                _restartText.text = "Press R to Restart";
                break;
            default:
                break;
        }
        
        _gameManager.GameOver();
    }
    public void UpdateWaveDisplay(int _currentWave)
    {
        _gamePlayMessages.text = " ";
        _gamePlayMessages.color = Color.magenta;
        _gamePlayMessages.text = "Wave: " + _currentWave;
        StartCoroutine(PlayMessageTimedErase());
    }
    public void UpdateEnemyInfo()
    {
        _gamePlayMessages.text = " ";
        //_gamePlayMessages.text = "All Enemies Have Been Eradicated";
        StartCoroutine(PlayMessageTimedErase());
    }

    public void UpdateSecondaryFire(int _secondaryWeaponID)
    {
        //_secondaryFireID = _secondaryWeaponID;
        //Debug.Log("UIManager Image" + _secondaryWeaponID);
        //_secondaryFireImg.sprite = _secondaryFireSprites[_secondaryWeaponID];
    }

// CoRoutines
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
        
        GameOverSequence(1);
    }
    IEnumerator PlayMessageTimedErase()
    {
        yield return new WaitForSeconds(2f);
        _gamePlayMessages.text = " ";
    }

}
