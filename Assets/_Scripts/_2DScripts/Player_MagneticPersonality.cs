using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MagneticPersonality : MonoBehaviour
{
    private GameObject[] _powerUps;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            _powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
            foreach (GameObject powerUp in _powerUps)
            {
                powerUp.transform.position = Vector3.MoveTowards(powerUp.transform.position, this.transform.position, .01f);
            }
        }

    }
}
