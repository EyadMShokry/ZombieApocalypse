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
	public float startBatLevel;
	// Battery current state level
	public float currentBatLevel;
	// Spotlight Object.
	public Light FLight;
	// Boolean variable to describe the flashlight state.
	public bool isOn;
	// timer that automatically decreases the battery level.
	public float timer;
	[Header("Flashlight Energy Bar")]
	public Image energyBar;
	void Start()
	{
		// Catching the component which is in the children of this.
		FLight = GetComponentInChildren<Light> ();
		// Defining the light as spot.
		FLight.type = LightType.Spot;
		// Battery level initialized with the value set in difficulty control script.
		startBatLevel = DifficulityControlScript.StartBatteryLevelAmount;
		energyBar = GameObject.Find ("ScreenCanvas/HealthAndEnergyBars/EnergyBar").GetComponent<Image>();
		currentBatLevel = startBatLevel;
		energyBar.fillAmount = currentBatLevel / startBatLevel;
		minusBat();
		isOn = true;
	}

	void minusBat()
	{
		if (isOn)
		{
			if (currentBatLevel > 0.00f) {
				currentBatLevel -= DifficulityControlScript.DegradingBatteryLevelAmount;
				FLight.intensity -= DifficulityControlScript.DegradingIntensityAmount;
				// Adjusting energy bar fill amount.
				energyBar.fillAmount = currentBatLevel / startBatLevel;
			}
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
			timer = DifficulityControlScript.DegradeAfterTime;
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

		if(currentBatLevel == 0)
		{
			FLight.enabled = false;
			isOn = false;
		}

	}
		
}
