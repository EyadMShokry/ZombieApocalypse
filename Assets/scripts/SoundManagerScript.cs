using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
		flashLightSwitchOnSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "torch_sound" + Path.DirectorySeparatorChar + "large_flashlight_switched_on");
		flashLightSwitchOffSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "torch_sound" + Path.DirectorySeparatorChar + "large_flashlight_switched_off");

		//box breaking sounds
		boxBreakingSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "box_breaking" + Path.DirectorySeparatorChar + "box_breaking");

		//character sounds
		characterDeathSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "character" + Path.DirectorySeparatorChar + "death");
		characterHurtSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "character" + Path.DirectorySeparatorChar + "hurt");
		characterHeavyBreathingSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "character" + Path.DirectorySeparatorChar + "heavy_breathing");
		characterScaredBreathingSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "character" + Path.DirectorySeparatorChar + "scared_breathing");

	//Weapons sounds:
		//m4 sounds
		m4MultiShotsSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "weapons" + Path.DirectorySeparatorChar + "m4" + Path.DirectorySeparatorChar + "multi_shots");
		m4OneShotSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "weapons" + Path.DirectorySeparatorChar + "m4" + Path.DirectorySeparatorChar + "one_shot");
		m4ReloadSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "weapons" + Path.DirectorySeparatorChar + "m4" + Path.DirectorySeparatorChar + "reload");
		//m9 pistol sounds
		m9ShotSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "weapons" + Path.DirectorySeparatorChar + "m9_pistol" + Path.DirectorySeparatorChar + "shot");
		m9ReloadChangeMagazineSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "weapons" + Path.DirectorySeparatorChar + "m9_pistol" + Path.DirectorySeparatorChar + "reload_change_magazine");
		//machine gun sounds
		machineGunReloadSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "weapons" + Path.DirectorySeparatorChar + "machine_gun" + Path.DirectorySeparatorChar + "reload");
		machineGunShotSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "weapons" + Path.DirectorySeparatorChar + "machine_gun" + Path.DirectorySeparatorChar + "shot");
		weaponsSwitchingSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "weapons" + Path.DirectorySeparatorChar + "weapons_switching");

		//background sounds
		startingMusicSound = Resources.Load<AudioClip> ("");
		gameMusic1Sound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "background" + Path.DirectorySeparatorChar + "game_music_1");

	//Zombies sounds:
		//big zombie sounds
		bigZombieAttackSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "zombies" + Path.DirectorySeparatorChar + "big_zombie" + Path.DirectorySeparatorChar + "attack");
		bigZombieBreathingSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "zombies" + Path.DirectorySeparatorChar + "big_zombie" + Path.DirectorySeparatorChar + "breathing");
		bigZombieDeathSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "zombies" + Path.DirectorySeparatorChar + "big_zombie" + Path.DirectorySeparatorChar + "death");
		//zombie girl sounds
		zombieGirlAttackSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "zombies" + Path.DirectorySeparatorChar + "zombie_girl" + Path.DirectorySeparatorChar + "attack");
		zombieGirlBreathingSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "zombies" + Path.DirectorySeparatorChar + "zombie_girl" + Path.DirectorySeparatorChar + "breathing");
		zombieGirlDeathSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "zombies" + Path.DirectorySeparatorChar + "zombie_girl" + Path.DirectorySeparatorChar + "death");
		//zombie police sounds
		zombiePoliceAttackSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "zombies" + Path.DirectorySeparatorChar + "zombie_police" + Path.DirectorySeparatorChar + "attack");
		zombiePoliceBreathingSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "zombies" + Path.DirectorySeparatorChar + "zombie_police" + Path.DirectorySeparatorChar + "breathing");
		zombiePoliceDeathSound = Resources.Load<AudioClip> ("sounds" + Path.DirectorySeparatorChar + "zombies" + Path.DirectorySeparatorChar + "zombie_police" + Path.DirectorySeparatorChar + "death");
		audioSrc = GetComponent<AudioSource> (); 
	}

	//Update is called once per frame
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