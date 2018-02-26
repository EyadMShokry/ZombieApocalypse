using UnityEngine;

public class GunScript : MonoBehaviour {

	// damage
	public float damage = 10f;
	// how far a bullet can go
	public float range = 100f;
	// fire rate -> how many bullets can be fired in a certaing time
	public float fireRate = 15f;
	// wooden crate force when hit
	public float impactForce = 30f;

	private float nextTimeToFire = 0f;

	// camera controller -> fps
	public Camera fpscam;
	// muzlle flash -> lightning on weapon fire
	//public ParticleSystem muzzleFlash;

	// audio source -> pistol audio
	private AudioSource pistolSound;

	// bullet impact effect
	public GameObject impactEffect;

	void Start(){
		pistolSound = GetComponent<AudioSource> ();
	}

	void Update (){
		if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire){
			nextTimeToFire = Time.time + 1f / fireRate;
			// a function to fire bullet with time
			Shoot();
		}
	}

	void Shoot(){
		// play muzzle flash
		//muzzleFlash.Play ();
		// like a collider object -> to check if something (target) is hit
		RaycastHit hit;
		// bullet coordinates
		Ray ray = new Ray (fpscam.transform.position, fpscam.transform.forward);
		if(Physics.Raycast(ray, out hit, range)){
			// play pistol sound clip
			pistolSound.Play ();
			if (hit.collider.CompareTag ("Enemy")) {
				Enemy enemy = hit.collider.GetComponent<Enemy> ();
				enemy.TakeDamage (damage);
				if(hit.rigidbody != null){
					hit.rigidbody.AddForce (-hit.normal * impactForce);
				}
			}
			GameObject impactGO =  Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
			Destroy (impactGO, 2f);
		}
	}
}
