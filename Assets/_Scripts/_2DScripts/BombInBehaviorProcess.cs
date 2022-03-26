using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombInBehaviorProcess : MonoBehaviour
{
    //private GameObject bomb;
    //private CircleCollider2D _collider2D;
    //public Vector3 _explosionTransform;

    public SoundManager _soundManager;
    private GameObject explosion;
    private GameObject _enemy2d;

    public bool _is2ndaryFireActive;
    public bool _isSafetyOff;
    //
    public bool _detonate;
    public bool _timedDetonate;
    public bool _withinDetonateRange;
    //
    public GameObject _bombPrefab;
    public GameObject _explosionPrefab;

    [SerializeField]
    private int bombID = 0;
    [SerializeField]
    private float _fuse = 2f;
    [SerializeField]
    private float _blastRadius = 3f;

    public void Start()
    {
        _soundManager = GameObject.Find("Sound_Manager").GetComponent<SoundManager>();
        //_collider2D = _bombPrefab.GetComponent<CircleCollider2D>();
        //_enemy2d = GameObject.Find("Enemy").GetComponent<Enemy2D>();

        switch (bombID)
        {
            case 0:
                //Debug.Log("The Bomber");
                TheBomber();
                break;
            case 1:
                //Debug.Log("The Athlete");
                StartCoroutine(TheAthlete());
                break;
            case 2:
                //Debug.Log("The Sniper");
                _detonate = true;
                _withinDetonateRange = true;
                break;
            case 3:
                //Debug.Log("The Strategist");
                _timedDetonate = true;
                _withinDetonateRange = true;
                break;
            default:
                break;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //theSniper
            if (_detonate && _withinDetonateRange)
            {
                TheBomber();
            }

            //theStrategist
            if (_timedDetonate && _withinDetonateRange)
            {
                TheStrategist();
            }
        }
        
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("Enemy Exploded");
            //_enemy2d.EnemyTakeDamage();
        }
    }

    private void TheBomber()
    {
        //_collider2D.radius = 10f;
        explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject, 1f);
        Destroy(explosion.gameObject, 4f);
        _soundManager.ExplosionSound();
        StartCoroutine(CheckCollateralDamage());
    }

    IEnumerator TheAthlete()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(.2f);
            float time = 0f;
            time += .2f;

            Debug.Log("Timer " + time);
        }
        explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject, 1f);
        Destroy(explosion.gameObject, 4f);
        _soundManager.ExplosionSound();
        CheckCollateralDamage();
    }

    private void TheStrategist()
    {
        StartCoroutine(TimedExplosion());
    }

    IEnumerator TimedExplosion()
    {
        yield return new WaitForSeconds(_fuse);
        TheBomber();
    }

    IEnumerator CheckCollateralDamage()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(enemy.transform.position, this.transform.position);
            if(distance < _blastRadius)
            {
                enemy.GetComponent<Enemy2D>().EnemyTakeDamage();
            }
        }

        yield return new WaitForSeconds(.25f);
        
    }   
        
}




    

