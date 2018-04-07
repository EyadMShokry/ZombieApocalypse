using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class m9_pistol : MonoBehaviour {
	
	// damage
	public float damage = 20f;

	// how far a bullet can go
	public float range = 100f;

	// fire rate -> how many bullets can be fired in a certain time
	public float fireRate = 15f;

	// crates force when hit
	public float impactForce = 30f;

	// Controlls the fire rate of the weapon
	private float nextTimeToFire = 0f;

	// camera controller -> fps
	public Camera fpscam;

	// muzlle flash -> lightning on weapon fire
	public ParticleSystem muzzleFlash;

	// bullet impact effect
	public GameObject impactEffect;

	// blood impact effect
	public GameObject bloodEffect;

	/* Following variables are concerned with Weapons ammunition */

	// The max clip size for the weapon
	public int maxMagazineSize = 15;

	// The max owned ammo by the player
	public int maxOwnedAmmo = 30;

	// The current count of ammo in the magazine
	private int currentMagazineAmmo;

	// The reload time -> (it's the same as the length of the reload animation clip)
	private float reloadTime = 2.3f;

	// A variable to check if the player is reloading
	private bool isReloading = false;

	// Weapon animation Script
	private WeaponAnimation WeaponAnimationScript;

	// UI for displaying weapon name, ammo count and image
	public TextMeshProUGUI WeaponNameDisplay;
	public TextMeshProUGUI ammoDisplay;
	public RawImage WeaponImage;

	// The weapon real name that will be displayed in the UI
	private string WeaponName = "M9 Pistol";

	// run once
	void Start (){
		// Set the magazine ammo to full size
		currentMagazineAmmo = maxMagazineSize;

		// Displaying current ammo count
		ammoDisplay.text = currentMagazineAmmo.ToString () + "/" + maxOwnedAmmo.ToString ();

		// Displaying Weapon Image
		WeaponImage.texture = Resources.Load("Weapon_images/M9") as Texture;

		WeaponAnimationScript = gameObject.GetComponent<WeaponAnimation> ();
	}

	// run once each frame
	void OnEnable(){
		// Displaying current ammo count
		ammoDisplay.text = currentMagazineAmmo.ToString () + "/" + maxOwnedAmmo.ToString ();

		// Displaying current weapon name
		WeaponNameDisplay.text = WeaponName;

		// Displaying Weapon Image
		WeaponImage.texture = Resources.Load("Weapon_images/M9") as Texture;

		isReloading = false;
	}

	void Update (){

		// Check if reloading do nothing
		if(isReloading){
			return;
		}

		// handling reload
		if (Input.GetKeyDown(WeaponAnimationScript.reloadKey) && !WeaponAnimationScript.anim.IsPlaying(WeaponAnimationScript.fire.name) && !WeaponAnimationScript.anim.IsPlaying(WeaponAnimationScript.draw.name) && (maxOwnedAmmo > 0 && currentMagazineAmmo != maxMagazineSize))
		{
			if (WeaponAnimationScript.reload != null) {
				StartCoroutine (Reload());
			}
		}

		if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && !WeaponAnimationScript.anim.IsPlaying(WeaponAnimationScript.fire.name)){
			nextTimeToFire = Time.time + 1f / fireRate;
			// a function to fire bullet with time
			Shoot();
		}
	}

	void Shoot(){
		if (currentMagazineAmmo > 0) {
			WeaponAnimationScript.anim.Play (WeaponAnimationScript.fire.name);
			SoundManagerScript.PlaySound ("m9Shot");
			// Decrease Ammo count
			currentMagazineAmmo--;
			// Displaying current ammo count
			ammoDisplay.text = currentMagazineAmmo.ToString () + "/" + maxOwnedAmmo.ToString ();
			// play muzzle flash
			muzzleFlash.Play ();
			// like a collider object -> to check if something (target) is hit
			RaycastHit hit;
			// bullet coordinates
			Ray ray = new Ray (fpscam.transform.position, fpscam.transform.forward);
			if (Physics.Raycast (ray, out hit, range)) {
				if (hit.collider.CompareTag ("zombieGirl") || hit.collider.CompareTag ("zombieCop") || hit.collider.CompareTag ("zombiePolice")) {
					/*Enemy enemy = hit.collider.GetComponent<Enemy> ();
					enemy.TakeDamage (damage);*/
					if (hit.rigidbody != null) {
						hit.rigidbody.AddForce (-hit.normal * impactForce);
					}
					GameObject bloodGO = Instantiate (bloodEffect, hit.point, Quaternion.LookRotation (hit.normal));
					Destroy (bloodGO, 2f);
				}
				GameObject impactGO = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
				Destroy (impactGO, 2f);
			}
		}
	}

	IEnumerator Reload(){
		isReloading = true;
		Debug.Log ("Reloading...");
		WeaponAnimationScript.anim.Play(WeaponAnimationScript.reload.name);
		yield return new WaitForSeconds(reloadTime);
		SoundManagerScript.PlaySound ("m9ReloadChangeMagazine");
		int AmmoToReload = maxMagazineSize - currentMagazineAmmo;
		if (AmmoToReload > maxOwnedAmmo) {
			currentMagazineAmmo += maxOwnedAmmo;
			maxOwnedAmmo = 0;
		} else {
			currentMagazineAmmo += AmmoToReload;
			maxOwnedAmmo -= AmmoToReload;
		}
		// Displaying current ammo count
		ammoDisplay.text = currentMagazineAmmo.ToString () + "/" + maxOwnedAmmo.ToString ();
		isReloading = false;
	}
}
