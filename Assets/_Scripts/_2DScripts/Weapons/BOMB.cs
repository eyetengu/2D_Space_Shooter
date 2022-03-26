using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOMB : MonoBehaviour
{
    private SoundManager _soundManager;

    [SerializeField]
    private bool _isArmed;
    [SerializeField]
    private bool _isTimer;
    [SerializeField]
    private bool _isTrigger;

    public float _fuse = 2f;
    [SerializeField]
    private float _blastRadius;

    [SerializeField]
    private GameObject _explosionPrefab;
    private GameObject explosion;

    private Transform _explosionTransform;

    void Start()
    {
        _soundManager = GameObject.Find("Sound_Manager").GetComponent<SoundManager>();
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (_isArmed)
        {
            if (_isTrigger && Input.GetKeyDown(KeyCode.E))
            {
                if (_isTimer)
                {
                    Debug.Log("Testing");
                    StartCoroutine(BombExplosion2D());
                }
                else
                {
                    BombExplosion();
                }
            }
            else if (_isTrigger == false)
            {
                if (_isTimer)
                {
                    Debug.Log("Testing");
                    StartCoroutine(BombExplosion2D());
                }
                else
                {
                    BombExplosion();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            if(_isTimer == true && _isTrigger == true)
            {
                StartCoroutine(BombExplosion2D());
            }
            if(_isTrigger == true)
            {
                BombExplosion();
            }
        }
    }

    private void BombExplosion()
    {
        SpriteRenderer visual = this.GetComponent<SpriteRenderer>();
        visual.enabled = false;

        _soundManager.ExplosionSound();

        explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        _blastRadius = 3f;

        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(enemy.transform.position, this.transform.position);
            if (distance < _blastRadius)
            {
                enemy.GetComponent<Enemy2D>().EnemyTakeDamage();
            }
        }
        Destroy(explosion.gameObject, 2f);
        Destroy(this.gameObject, 3f);
    }

    IEnumerator BombExplosion2D()
    {
        yield return new WaitForSeconds(_fuse);
        BombExplosion();       
    }
}
