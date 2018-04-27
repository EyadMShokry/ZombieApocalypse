using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DifficulityControlScript : MonoBehaviour
{
	// Following variables are concerned with camera shaking effect. (defalt values)
	private static float cameraShakingPower = 0.7f;
	private static float cameraShakingDuration = 1.0f;
	private static float cameraShakingSlowDownAmount = 1.0f;
	// ------------------------------
	// Following variables are concerned with flash light battery level. (defalt values)
	private static float startBatLevelAmount = 15.00f;
	private static float degradingIntensityAmount = 1.00f;
	private static float degradingBatteryLevelAmount = 1.00f;
	// Time in seconds.
	private static float degradeAfterTime = 5.00f;

	// Following variables are concerned with zombies
	private static float zombieSpeed = 0.2f; // Zombie speed 
	private static float distanceMagnitute = 10.0f; // Vector Magnitute between Zombie and player determine the zombie state
	private static float seeAngle = 120.0f; // angle between zombie and player determine zombie state
	private static float rotationSpeed = 0.5f; // angle between zombie and player determine zombie state
	private static int zombiesNumber = 5;

	private static int infectionValue = 25;

	public static void DiffMode(string mode) // Function that change the mode of game in [Easy, Medium, Hard]
	{
		mode = mode.ToLower ();
		switch (mode) {
		case "easy":
			//Do Nothing
			break;
		case "medium":
			CameraShakingPower = CameraShakingPower * 1.1f;
			CameraShakingDuration = CameraShakingDuration * 1.1f;
			CameraShakingSlowDownAmount = CameraShakingSlowDownAmount * 1.1f;

			StartBatteryLevelAmount = StartBatteryLevelAmount * 0.8f;
			DegradingBatteryLevelAmount = DegradingBatteryLevelAmount * 0.8f;
			DegradingIntensityAmount = DegradingIntensityAmount * 0.8f;

			ZombieSpeed = ZombieSpeed * 1.5f;
			ZombiesNumber = ZombiesNumber * 2;
			DistanceMagnitute = DistanceMagnitute * 1.5f;
			break;
		case "hard":
			CameraShakingPower = CameraShakingPower * 1.3f;
			CameraShakingDuration = CameraShakingDuration * 1.3f;
			CameraShakingSlowDownAmount = CameraShakingSlowDownAmount * 1.3f;

			StartBatteryLevelAmount = StartBatteryLevelAmount * 0.6f;
			DegradingBatteryLevelAmount = DegradingBatteryLevelAmount * 0.6f;
			DegradingIntensityAmount = DegradingIntensityAmount * 0.6f;

			ZombieSpeed = ZombieSpeed * 2.0f;
			ZombiesNumber = ZombiesNumber * 4;
			DistanceMagnitute = DistanceMagnitute * 2.5f;
			break;

		}
	}

	public static int ZombiesNumber {
		get {
			return zombiesNumber;
		}

		set {
			zombiesNumber = value;
		}
	}
		

	public static float RotationSpeed {
		get {
			return rotationSpeed;
		}

		set {
			rotationSpeed = value;
		}
	}


	public static float ZombieSpeed {

		get {

			return zombieSpeed;

		}

		set {

			zombieSpeed = value;

		}

	}

	public static float DistanceMagnitute {
		get {
			return distanceMagnitute;
		}
		set {
			distanceMagnitute = value;
		}
	}

	public static float SeeAngle {
		get {
			return seeAngle;
		}
		set {
			seeAngle = value;
		}
	}


	// Camera shaking methods
	public static float CameraShakingPower {
		get {
			return cameraShakingPower;
		}
		set {
			cameraShakingPower = value;
		}
	}

	public static float CameraShakingDuration {
		get {
			return cameraShakingDuration;
		}
		set {
			cameraShakingDuration = value;
		}
	}

	public static float CameraShakingSlowDownAmount {
		get {
			return cameraShakingSlowDownAmount;
		}
		set {
			cameraShakingSlowDownAmount = value;
		}
	}
	// --------------------------------------------------------
	// Flashlight battery methods.
	public static float StartBatteryLevelAmount {
		get {
			return startBatLevelAmount;
		}
		set {
			startBatLevelAmount = value;
		}
	}
	public static float DegradingIntensityAmount {
		get {
			return degradingIntensityAmount;
		}
		set {
			degradingIntensityAmount = value;
		}
	}
	public static float DegradingBatteryLevelAmount {
		get {
			return degradingBatteryLevelAmount;
		}
		set {
			degradingBatteryLevelAmount = value;
		}
	}
	public static float DegradeAfterTime {
		get {
			return degradeAfterTime;
		}
		set {
			degradeAfterTime = value;
		}
	}
	// ---------------------------------

	public static int InfectionValue {
		get {
			return infectionValue;
		}
		set { 
			infectionValue = value;
		}
	}
}
