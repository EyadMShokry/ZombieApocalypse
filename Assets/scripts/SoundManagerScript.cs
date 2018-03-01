﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
	public static AudioClip flashLightSwitchOnSound, flashLightSwitchOffSound;
	static AudioSource audioSrc;
	// Use this for initialization
	void Start ()
	{
		flashLightSwitchOnSound = Resources.Load<AudioClip> ("sounds/torch_sound/large_flashlight_switched_on");
		flashLightSwitchOffSound = Resources.Load<AudioClip> ("sounds/torch_sound/large_flashlight_switched_off");
		audioSrc = GetComponent<AudioSource> (); 
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public static void PlaySound (string clip)
	{
		switch (clip) {
		case "FlashlightSwitchOn":
			audioSrc.PlayOneShot (flashLightSwitchOnSound);
			break;
		case "FlashlightSwitchOff":
			audioSrc.PlayOneShot (flashLightSwitchOffSound);
			break;
		}
	}
}