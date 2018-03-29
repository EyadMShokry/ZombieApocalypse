using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
	public static AudioClip flashLightSwitchOnSound, flashLightSwitchOffSound, boxBreakingSound, characterDeathSound, characterHurtSound,
	characterHeavyBreathingSound, characterScaredBreathingSound, m4MultiShotsSound, m4OneShotSound, m4ReloadSound, m9ShotSound, m9ReloadChangeMagazineSound,
	machineGunReloadSound, machineGunShotSound, weaponsSwitchingSound, startingMusicSound, gameMusic1Sound, bigZombieAttackSound, bigZombieBreathingSound,
	bigZombieDeathSound, zombieGirlAttackSound, zombieGirlBreathingSound, zombieGirlDeathSound, zombiePoliceAttackSound, zombiePoliceBreathingSound,
	zombiePoliceDeathSound;
	static AudioSource audioSrc;
	// Use this for initialization
	void Start ()
	{
		//flash light sounds
		flashLightSwitchOnSound = Resources.Load<AudioClip> ("sounds/torch_sound/large_flashlight_switched_on");
		flashLightSwitchOffSound = Resources.Load<AudioClip> ("sounds/torch_sound/large_flashlight_switched_off");

		//box breaking sounds
		boxBreakingSound = Resources.Load<AudioClip> ("sounds/box_breaking/box_breaking");

		//character sounds
		characterDeathSound = Resources.Load<AudioClip> ("sounds/character/death");
		characterHurtSound = Resources.Load<AudioClip> ("sounds/character/hurt");
		characterHeavyBreathingSound = Resources.Load<AudioClip> ("sounds/character/heavy_breathing");
		characterScaredBreathingSound = Resources.Load<AudioClip> ("sounds/character/scared_breathing");

	//Weapons sounds:
		//m4 sounds
		m4MultiShotsSound = Resources.Load<AudioClip> ("sounds/weapons/m4/multi_shots");
		m4OneShotSound = Resources.Load<AudioClip> ("sounds/weapons/m4/one_shot");
		m4ReloadSound = Resources.Load<AudioClip> ("sounds/weapons/m4/reload");
		//m9 pistol sounds
		m9ShotSound = Resources.Load<AudioClip> ("sounds/weapons/m9_pistol/shot");
		m9ReloadChangeMagazineSound = Resources.Load<AudioClip> ("sounds/weapons/m9_pistol/reload_change_magazine");
		//machine gun sounds
		machineGunReloadSound = Resources.Load<AudioClip> ("sounds/weapons/machine_gun/reload");
		machineGunShotSound = Resources.Load<AudioClip> ("sounds/weapons/machine_gun/shot");
		weaponsSwitchingSound = Resources.Load<AudioClip> ("sounds/weapons/weapons_switching");

		//background sounds
		startingMusicSound = Resources.Load<AudioClip> ("sounds/background/starting_music");
		gameMusic1Sound = Resources.Load<AudioClip> ("sounds/background/game_music_1");

	//Zombies sounds:
		//big zombie sounds
		bigZombieAttackSound = Resources.Load<AudioClip> ("sounds/zombies/big_zombie/attack");
		bigZombieBreathingSound = Resources.Load<AudioClip> ("sounds/zombies/big_zombie/breathing");
		bigZombieDeathSound = Resources.Load<AudioClip> ("sounds/zombies/big_zombie/death");
		//zombie girl sounds
		zombieGirlAttackSound = Resources.Load<AudioClip> ("sounds/zombies/zombie_girl/attack");
		zombieGirlBreathingSound = Resources.Load<AudioClip> ("sounds/zombies/zombie_girl/breathing");
		zombieGirlDeathSound = Resources.Load<AudioClip> ("sounds/zombies/zombie_girl/death");
		//zombie police sounds
		zombiePoliceAttackSound = Resources.Load<AudioClip> ("sounds/zombies/zombie_police/attack");
		zombiePoliceBreathingSound = Resources.Load<AudioClip> ("sounds/zombies/zombie_police/breathing");
		zombiePoliceDeathSound = Resources.Load<AudioClip> ("sounds/zombies/zombie_police/death");
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
		case "boxBreaking":
			audioSrc.PlayOneShot (boxBreakingSound);
			break;
		case "characterDeath":
			audioSrc.PlayOneShot (characterDeathSound);
			break;
		case "characterHurt":
			audioSrc.PlayOneShot (characterHurtSound);
			break;
		case "characterHeavyBreathing":
			audioSrc.PlayOneShot (characterHeavyBreathingSound);
			break;
		case "characterScaredBreathing":
			audioSrc.PlayOneShot (characterScaredBreathingSound);
			break;
		case "m4MultiShots":
			audioSrc.PlayOneShot (m4MultiShotsSound);
			break;
		case "m4OneShot":
			audioSrc.PlayOneShot (m4OneShotSound);
			break;
		case "m4Reload":
			audioSrc.PlayOneShot (m4ReloadSound);
			break;
		case "m9Shot":
			audioSrc.PlayOneShot (m9ShotSound);
			break;
		case "m9ReloadChangeMagazine":
			audioSrc.PlayOneShot (m9ReloadChangeMagazineSound);
			break;
		case "machineGunReload":
			audioSrc.PlayOneShot (machineGunReloadSound);
			break;
		case "machineGunShot":
			audioSrc.PlayOneShot (machineGunShotSound);
			break;
		case "weaponsSwitching":
			audioSrc.PlayOneShot (weaponsSwitchingSound);
			break;
		case "startingMusic":
			audioSrc.PlayOneShot (startingMusicSound);
			break;
		case "gameMusic1":
			audioSrc.PlayOneShot (gameMusic1Sound);
			break;
		case "bigZombieAttack":
			audioSrc.PlayOneShot (bigZombieAttackSound);
			break;
		case "bigZombieBreathing":
			audioSrc.PlayOneShot (bigZombieBreathingSound);
			break;
		case "bigZombieDeath":
			audioSrc.PlayOneShot (bigZombieDeathSound);
			break;
		case "zombieGirlAttack":
			audioSrc.PlayOneShot (zombieGirlAttackSound);
			break;
		case "zombieGirlBreathing":
			audioSrc.PlayOneShot (zombieGirlBreathingSound);
			break;
		case "zombieGirlDeath":
			audioSrc.PlayOneShot (zombieGirlDeathSound);
			break;
		case "zombiePoliceAttack":
			audioSrc.PlayOneShot (zombiePoliceAttackSound);
			break;
		case "zombiePoliceBreathing":
			audioSrc.PlayOneShot (zombiePoliceBreathingSound);
			break;
		case "zombiePoliceDeath":
			audioSrc.PlayOneShot (zombiePoliceDeathSound);
			break;
		
		}
	}
}