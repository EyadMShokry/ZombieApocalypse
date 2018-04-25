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
		if (InfectionBarController.initialInfectionValue > 25)
			turnBlurOff ();

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
