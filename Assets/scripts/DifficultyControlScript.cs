﻿using System.Collections;
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
	// time in seconds.
	private static float degradeAfterTime = 5.00f;


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


}
