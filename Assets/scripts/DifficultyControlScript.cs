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

	public static int number_of_instanse_health_Small=5;
	public static int number_of_instanse_health_Mid = 4;
	public static int number_of_instanse_health_Large = 3;
	public static int number_of_instanse_Ammo_9m = 5;
	public static int number_of_instanse_Ammo_4m = 5;
	public static int number_of_instanse_Zombie_Cop = 20;
	public static int number_of_instanse_Zombie_Girl=20;
	public static int number_of_instanse_health_Police=20;

	public static void DiffMode(string mode) // Function that change the mode of game in [Easy, Medium, Hard]
	{
		mode = mode.ToLower ();
		switch (mode) {
		case "easy":
			//Do Nothing
			break;
		case "medium":
			CameraShakingPower = 7.7f;
			CameraShakingDuration =  1.1f;
			CameraShakingSlowDownAmount =  1.1f;

			StartBatteryLevelAmount = 12.0f;
			DegradingBatteryLevelAmount = 0.8f;
			DegradingIntensityAmount = 0.8f;

			ZombieSpeed =  0.3f;
			ZombiesNumber = 6;
			DistanceMagnitute = 15.0f;
			break;
		case "hard":
			CameraShakingPower = 8.5f;
			CameraShakingDuration = 1.3f;
			CameraShakingSlowDownAmount = 1.3f;

			StartBatteryLevelAmount = 9.0f;
			DegradingBatteryLevelAmount = 0.6f;
			DegradingIntensityAmount = 0.6f;

			ZombieSpeed = 0.4f;
			ZombiesNumber = 8;
			DistanceMagnitute = 25.0f;
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
