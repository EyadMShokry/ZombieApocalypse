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
		if (InfectionBarController.initialInfectionValue > 25) {
			blurEffect.enabled = false;
		} else
			blurEffect.enabled = true;
		/*
		else {
			while (InfectionBarController.initialInfectionValue <= 25) {
				InvokeRepeating("turnBlurOn", 1.0f, 5.0f);

			}
		}
		*/
	}
	/*
	void turnBlurOn()
	{
		blurEffect.enabled = true;
	}
	*/
}
