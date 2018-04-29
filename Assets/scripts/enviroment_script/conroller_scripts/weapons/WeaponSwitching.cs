using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;
public class WeaponSwitching : MonoBehaviour {

	public int selectedWeapon = 0;
	
	// Use this for initialization
	void Start () {
		SelectWeapon ();
	}

	// Update is called once per frame
	void Update () {
		if (UnityStandardAssets.Characters.FirstPerson.FirstPersonController.isplayerDeath==false) {
			int previousSelectedWeapon = selectedWeapon;
			if(Input.GetAxis("Mouse ScrollWheel") > 0f){
				if (selectedWeapon >= transform.childCount - 1) {
					selectedWeapon = 0;
					if (!SoundManagerScript.audioSrc.isPlaying) 
					{
						SoundManagerScript.PlaySound ("weaponsSwitching");
					}
				} else {
					selectedWeapon++;
					if (!SoundManagerScript.audioSrc.isPlaying) 
					{
						SoundManagerScript.PlaySound ("weaponsSwitching");
					}
				}
			}
			if(Input.GetAxis("Mouse ScrollWheel") < 0f){
				if (selectedWeapon <= 0) {
					selectedWeapon = transform.childCount - 1;
					if (!SoundManagerScript.audioSrc.isPlaying) 
					{
						SoundManagerScript.PlaySound ("weaponsSwitching");
					}

				} else {
					selectedWeapon--;
					if (!SoundManagerScript.audioSrc.isPlaying) 
					{
						SoundManagerScript.PlaySound ("weaponsSwitching");
					}

				}
			}
			if(Input.GetKeyDown(KeyCode.Alpha1)){
				selectedWeapon = 0;
				if (!SoundManagerScript.audioSrc.isPlaying) 
				{
					SoundManagerScript.PlaySound ("weaponsSwitching");
				}
			}
			if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2){
				selectedWeapon = 1;
				if (!SoundManagerScript.audioSrc.isPlaying) 
				{
					SoundManagerScript.PlaySound ("weaponsSwitching");
				}
			}
			if(Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3){
				selectedWeapon = 2;
				if (!SoundManagerScript.audioSrc.isPlaying) 
				{
					SoundManagerScript.PlaySound ("weaponsSwitching");
				}

			}
			if(previousSelectedWeapon != selectedWeapon){
				SelectWeapon ();
			}
		}

	}

	void SelectWeapon(){
		int i = 0;
		foreach(Transform weapon in transform){
			if (i == selectedWeapon) {
				weapon.gameObject.SetActive (true);
			} else {
				weapon.gameObject.SetActive (false);
			}
			i++;
		}
	}
}

