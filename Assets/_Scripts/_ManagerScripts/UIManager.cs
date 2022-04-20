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

    [SerializeField]
    private GameObject _weaponSlot1b;
    [SerializeField]
    private GameObject _weaponSlot2a;
    [SerializeField]
    private GameObject _weaponSlot3a;
    [SerializeField]
    private Slider _coolDownSlider;
    [SerializeField]
    private Slider _enemyHealthSlider;
    [SerializeField]
    private GameObject _enemyHealthPanel;

    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        { 
            Debug.LogError("UIManager.cs- Game Manager is null"); 
        }
        
        //_weaponSlot1b = GameObject.Find("WeapSlot1b");

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
        _gamePlayMessages.color = Color.red;
        _gamePlayMessages.text = "Resign Yourself To Your Fate, mortal!";
        
        _ammoText = GameObject.Find("Ammo_text").GetComponent<Text>();
        _fuelLevelSlider = GameObject.Find("FuelLevel_slider").GetComponent<Slider>();
        if(_fuelLevelSlider == null)
        { Debug.LogError("UIManager- Fuel Level Indicator Missing");}


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
    }

    public void FuelManager(float _fuelLevel) 
    {
        _fuelLevelSlider.value = _fuelLevel;
        //Debug.Log("UIManager- " + _fuelLevel);
    }
    
//Message Center
    public void GamePlayMessages(int _gamePlayMessenger)
    {
        switch(_gamePlayMessenger)
        {
            case 0:
                //  _gamePlayMessages.text = "";
                break;
            case 1:
                _gamePlayMessages.text = "RELOAD";
                break;
            case 2:
                _gamePlayMessages.text = "You Won!";
                StartCoroutine(YouWonFlickerRoutine());
                break;
            case 3:
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
                _restartText.fontSize = 20;
                _restartText.text = "Press R to Restart";
                StartCoroutine(GameOverFlickerRoutine());
                break;
            case 2:
                _gameOverText.fontSize = 58;
                _gameOverText.text = "You Won!";
                _restartText.text = "Press R to Restart";
                StartCoroutine(YouWonFlickerRoutine());
                break;
            default:
                break;
        }
        
        _gameManager.GameOver();
    }
    public void UpdateWaveDisplay(int _currentWave)
    {       
        _gamePlayMessages.color = Color.magenta;
        _gamePlayMessages.text = "Wave: " + (_currentWave + 1);
        StartCoroutine(PlayMessageTimedErase());
    }
    public void UpdateEnemyInfo()
    {
        //_gamePlayMessages.text = " ";
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

    IEnumerator YouWonFlickerRoutine()
    {
        _gameOverText.fontSize = 24;
            yield return new WaitForSeconds(.25f);
        _gameOverText.fontSize = 40;
            yield return new WaitForSeconds(.25f);
        _gameOverText.fontSize = 24;
            yield return new WaitForSeconds(.25f);
        _gameOverText.fontSize = 58;
            yield return new WaitForSeconds(.25f);
        GameOverSequence(2);
    }

    IEnumerator PlayMessageTimedErase()
    {
        yield return new WaitForSeconds(5f);
        _gamePlayMessages.text = " ";
    }
    
    IEnumerator WaveDisplay(int _currentWave)
    {
        //_gamePlayMessages.text = "Wave: " + _currentWave;
        //StartCoroutine(PlayMessageTimedErase());

        yield return new WaitForSeconds(.1f);
    }

//WeaponsBank
    public void ActivateTripleShot(bool _isTripleShotActive)
    {
        Debug.Log("UIMGR: " + _isTripleShotActive);
        _weaponSlot1b.SetActive(_isTripleShotActive);
    }

    public void ActivateSecondary(bool _isSecondaryEquipped)
    {
        _weaponSlot2a.SetActive(_isSecondaryEquipped);
        if(_isSecondaryEquipped == false)
        {
            StartCoroutine(LaserCooldown());
        }
    }

    public void ActivateMissile(bool _isHomingReady)
    {
        _weaponSlot3a.SetActive(_isHomingReady);
    }

    IEnumerator LaserCooldown()
    {
        _coolDownSlider.value = 10;
        for (int i = 0; i < 10; i++)
        {

            yield return new WaitForSeconds(.5f);
            _coolDownSlider.value -= 1;
        }
    }

//EnemyHealthRoutine
    public void EnemyBossHealth(int _ufoShield)
    {
        _enemyHealthPanel.SetActive(true);
        _enemyHealthSlider.value = _ufoShield;

        if(_enemyHealthSlider.value < 1)
        {
            _enemyHealthPanel.SetActive(false);
        }    
    }
}
