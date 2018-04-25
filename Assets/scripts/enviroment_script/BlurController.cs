using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;


public class BlurController : MonoBehaviour {
	public Blur blurEffect;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (InfectionBarController.initialInfectionValue > DifficulityControlScript.InfectionValue)
			turnBlurOff ();
		else if(InfectionBarController.initialInfectionValue <= DifficulityControlScript.InfectionValue)
			StartCoroutine (SetBlur());
	}

	IEnumerator SetBlur(){
		turnBlurOn();
		yield return new WaitForSeconds(5);
		turnBlurOff();
		yield return new WaitForSeconds(10);
	}

	void turnBlurOn()
	{
		blurEffect.enabled = true;
	}

	void turnBlurOff()
	{
		blurEffect.enabled = false;
	}

}
