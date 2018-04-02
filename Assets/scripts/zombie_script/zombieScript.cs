using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Threading;

public class zombieScript : MonoBehaviour
{

	private Transform t_Player;
	// Finding player
	private FirstPersonController m_Player;
	static Animator anim;
	private bool health = true;

	public GameObject Zombie;
	//private GameObject []TheZombeies;
	private float PlaceX;
	private float PlaceZ;

	//sounds
	public string DeathSound;
	public string IdleSound;
	public string AttackSound;


	// Use this for initialization
	void Start ()
	{
		//TheZombeies = new GameObject ();
		anim = GetComponent<Animator> ();	
		t_Player = GameObject.Find ("Player").transform;
		m_Player = GameObject.FindObjectOfType<FirstPersonController>();
		//for (int i = 0; i < 3; i++) {
			PlaceX = Random.Range (-22, 22); // should change according to values of the room
			PlaceZ = Random.Range (-26, 12); // should change according to values of the room
			Zombie.transform.position = new Vector3 (PlaceX, 0, PlaceZ);

		//	TheZombeies[i]=Instantiate (Zombie,  new Vector3 (PlaceX, 0, PlaceZ), Quaternion.identity);
		//}
		

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!health) {
			anim.SetBool ("isDie", true);
			SoundManagerScript.PlaySound(DeathSound);
		}
			
		Vector3 direction = t_Player.position - Zombie.transform.position;
		float angle = Vector3.Angle (direction, this.transform.forward);

		direction.y = 0;
		if (direction.magnitude < DifficulityControlScript.DistanceMagnitute && DifficulityControlScript.SeeAngle<=30) {
			Zombie.transform.rotation = Quaternion.Slerp 
				(Zombie.transform.rotation, Quaternion.LookRotation (direction), 0.1f);

			anim.SetBool ("isWalking", true);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isBiting", false);
			Zombie.transform.Translate (0, 0, DifficulityControlScript.ZombieSpeed);
			if (anim.GetBool ("isWalking") == true) {
				if (direction.magnitude < 2) {	
					anim.SetBool ("isWalking", false);
					anim.SetBool ("isAttacking", true);
					SoundManagerScript.PlaySound(AttackSound);
					m_Player.ShakePlayer (/* Duration*/ DifficulityControlScript.CameraShakingDuration, /*Shaking Power*/ 
						DifficulityControlScript.CameraShakingPower, /*Slow down amount*/DifficulityControlScript.CameraShakingSlowDownAmount);
				}
			} 
			if (anim.GetBool ("isAttacking") == true || anim.GetBool ("isBiting") == true) {
				if (direction.magnitude > 2) {	
					anim.SetBool ("isAttacking", false);
					anim.SetBool ("isBiting", false);
					anim.SetBool ("isWalking", true);
				} 
			}
		} else {
			anim.SetBool ("isAttacking", false);
			anim.SetBool("isBiting", false);
			anim.SetBool ("isWalking", false);
			anim.SetBool("isIdle",true);
			SoundManagerScript.PlaySound(IdleSound);
		}	}

	void OnTriggerEnter(Collider other)
	{

		
		if (other.GetComponent<Collider> ().name == "Wall") {
			
			transform.Rotate(0, 180,0);

			transform.Translate(Vector3.forward * 1);

			//Destroy (gameObject);


		}


	}
}
