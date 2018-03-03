using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightScript : MonoBehaviour {
	// Public variables to modify the place of the battery rect.
	public float a;
	public float b;
	public float c;
	public float d;
	// Battery Integer Level.
	public int batLevel;
	// Spotlight Object.
	public Light FLight;
	// Boolean variable to describe the flashlight state.
	public bool isOn;
	// timer that automatically decreases the battery level.
	public float timer;
	void Start()
	{
		// Catching the component which is in the children of this.
		FLight = GetComponentInChildren<Light> ();
		// Defining the light as spot.
		FLight.type = LightType.Spot;
		// Battery level initialized with 15.
		batLevel = 15;
		minusBat();
		isOn = true;
	}

	void minusBat()
	{
		if (isOn)
		{
			batLevel -= 1;
			FLight.intensity -= 1;
		}
	}

	void Update()
	{

		if(timer >= 0)
		{
			if (isOn)
			{
				timer -= Time.deltaTime;
			}
		}

		if (timer <= 0)
		{
			timer = 5;
			minusBat();
		}

		if (Input.GetKeyUp(KeyCode.F))
		{
			FLight.enabled = !FLight.enabled;

			if (!isOn)
			{
				isOn = true;
				SoundManagerScript.PlaySound ("FlashlightSwitchOn");

			}
			else
			{
				isOn = false;
				SoundManagerScript.PlaySound ("FlashlightSwitchOff");
			}

		}

		if(batLevel == 0)
		{
			batLevel = 0;
			FLight.enabled = false;
			isOn = false;
		}

	}
		
}
