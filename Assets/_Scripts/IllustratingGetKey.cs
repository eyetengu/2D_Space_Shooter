using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustratingGetKey : MonoBehaviour
{


	void Start()
	{
		//(Input.GetKeyDown(KeyCode.Space));   //true during frame key is started to press
		//(Input.GetKey(KeyCode.Space));   //true while user holds down the key
		//(Input.GetKeyUp(KeyCode.Space));   //true during frame that key is released



		Debug.Log("Don't You Push that 'Space' Key");
		Debug.Log("Don't Do It!");
		Debug.Log("Back Off, Jack!");
	}
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Debug.Log("You just had to do it. didn't you?");
			}
			if (Input.GetKey(KeyCode.Space))
			{
				Debug.Log("You can let up now.");
				Debug.Log("No. Seriously. Time To Go");
			}
			else if (Input.GetKeyUp(KeyCode.Space))
			{
				Debug.Log("OOFA! No Need to get Pushy");
			}
			else if (Input.GetKey(KeyCode.Space) == false)
			{
			Debug.Log("What? You Chicken?");
			Debug.Log("Bok-bok-bok-bok-Ke-BOK");
			Debug.Log("Go tell your mommy you're too afraid to press the 'Space' key");
			}
		}
}