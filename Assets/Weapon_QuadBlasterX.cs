using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_QuadBlasterX : MonoBehaviour
{
    private float _speed = 2;
    public bool quadReady;

    public GameObject shell1;
    public GameObject shell2;
    public GameObject shell3;
    public GameObject shell4;

    void Update()
    {
        if (quadReady)
        {
            DecisionTree();
        }
    }

    void DecisionTree()
    {
        shell1.transform.Translate(Vector3.up * _speed * Time.deltaTime);
        shell2.transform.Translate(Vector3.left * _speed * Time.deltaTime);
        shell3.transform.Translate(Vector3.right * _speed * Time.deltaTime);
        shell4.transform.Translate(Vector3.down * _speed * Time.deltaTime);

        StartCoroutine(ShellCleanup());
    }

    IEnumerator ShellCleanup()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
