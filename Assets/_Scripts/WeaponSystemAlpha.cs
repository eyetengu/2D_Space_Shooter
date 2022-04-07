using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystemAlpha : MonoBehaviour
{
    private int _secondaryWeaponID = 0;
    [SerializeField]
    private GameObject[] weaponsArray;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager == null)
        { Debug.Log("Canvas is null"); }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            _secondaryWeaponID--;
        }
        else if(Input.GetKeyDown(KeyCode.L))
        {
            _secondaryWeaponID++;
        }

        if (_secondaryWeaponID > weaponsArray.Length - 1)
        {
            _secondaryWeaponID = 0;
        }

        if(_secondaryWeaponID < 0)
        { 
            _secondaryWeaponID = weaponsArray.Length - 1;
        }

        Debug.Log(_secondaryWeaponID + " Weapon " + weaponsArray[_secondaryWeaponID]);
        _uiManager.UpdateSecondaryFire(_secondaryWeaponID);




        if(Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(FireSecondary());
        }

    }

    void SecondaryFireRoutine()
    {
    }

    IEnumerator FireSecondary()
    {
        switch (_secondaryWeaponID)
        {
            case 0:
                Debug.Log("Weapon 0");
                break;
            case 1:
                Debug.Log("Weapon 1");
                break;
            case 2:
                Debug.Log("Weapon 2");
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(1f);
    }
}
