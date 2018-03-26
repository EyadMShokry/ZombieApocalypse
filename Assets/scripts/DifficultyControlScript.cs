using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DifficulityControlScript : MonoBehaviour
{
	//Following variables are concerned with camera shaking effect.
	private static float cameraShakingPower = 0.7f;
	private static float cameraShakingDuration = 1.0f;
	private static float cameraShakingSlowDownAmount = 1.0f;
	// ------------------------------


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


}
