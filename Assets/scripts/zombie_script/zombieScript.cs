using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Threading;

public class zombieScript : MonoBehaviour
{
	#region PRIVATE VARIABLES
	private Transform t_Player;
	// Finding player
	//private FirstPersonController m_Player; disabled until fixing the unrealistic behavior of camera shaking effect.
	static Animator anim;
	private bool health = true;
	// Use this for initialization

	// cordinates that if the player die the zombie will wake randomly
	private float x;
	private float z;
	#endregion

	//sounds
	public string DeathSound;
	public string IdleSound;
	public string AttackSound;

	// Integer that is randomized to either choose to attack or bite.
	bool RANDOMIZED_STATE_INIT;
	private void Translate (Vector3 direction){
		this.transform.rotation = Quaternion.Slerp 
			(this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);
		this.transform.Translate (0, 0, DifficulityControlScript.ZombieSpeed*Time.deltaTime);
	}

	private Vector3 CalculateDiffVector ()
	{
		return t_Player.position - this.transform.position;
	}

	void Start ()
	{
		x = Random.Range (-28, 26);
		z = Random.Range (-15, 20);
		anim = GetComponent<Animator> ();	
		t_Player = GameObject.Find ("Player").transform;
		//m_Player = GameObject.FindObjectOfType<FirstPersonController>(); disabled until fixing the unrealistic behavior of camera shaking effect.
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!health) {
			anim.SetBool ("isDie", true);
			SoundManagerScript.PlaySound (DeathSound);
		}

		if (UnityStandardAssets.Characters.FirstPerson.FirstPersonController.isplayerDeath == true) {
			
			Translate (new Vector3 (x, 0, z));
			SetZombieState ("Walk");
		
		} else {	
			Vector3 direction = CalculateDiffVector();
			float angle = Vector3.Angle (direction, this.transform.forward);

			direction.y = 0;
			if (direction.magnitude <= DifficulityControlScript.DistanceMagnitute && angle <= DifficulityControlScript.SeeAngle) {
				SetZombieState ("Walk");
				Translate (direction);
				if (anim.GetBool ("isWalking") == true) {
					if (CalculateDiffVector().magnitude <= 2) {	

						// Randomizing State
						RANDOMIZED_STATE_INIT = randomBoolean();
						if (RANDOMIZED_STATE_INIT)
							SetZombieState ("Attack");
						else 
							SetZombieState("Bite");

						SoundManagerScript.PlaySound (AttackSound);
						//m_Player.ShakePlayer (/* Duration*/ DifficulityControlScript.CameraShakingDuration, /*Shaking Power*/ 
						//DifficulityControlScript.CameraShakingPower, /*Slow down amount*/ DifficulityControlScript.CameraShakingSlowDownAmount);

						// TODO
						// Above function was disabled due to unrealistic behavior (disabled by andrewnagyeb)
					}
				} 
				if (anim.GetBool ("isAttacking") == true || anim.GetBool ("isBitting") == true) {
					if (direction.magnitude > 2) {	
						SetZombieState ("Walk");
					} 
				}
			} else {
				SetZombieState ("Idle");
				SoundManagerScript.PlaySound (IdleSound);
			}
		}
	}
	void OnTriggerEnter(MeshFilter other)
	{


		if (other.GetComponent<MeshFilter> ().name == "Wall") {

			transform.Rotate(0, 180,0);

			transform.Translate(Vector3.forward * 1*Time.deltaTime);

			//Destroy (gameObject);


		}


	}
	#region PRIVATE FUNCTIONS
	/*
	 * Following function is used to set state
	 * @param state to be set
	 */

	private void SetZombieState(string state){
		switch (state) {
		case "Attack":
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isBitting", false);
			anim.SetBool ("isAttacking", true);
			break;
		case "Bite":
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isBitting", true);
			anim.SetBool ("isAttacking", false);
			break;
		case "Idle":
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", true);
			anim.SetBool ("isBitting", false);
			anim.SetBool ("isAttacking", false);
			break;
		case "Walk":
			anim.SetBool ("isWalking", true);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isBitting", false);
			anim.SetBool ("isAttacking", false);
			break;
		}
	}

	private bool randomBoolean ()
	{
		return (Random.Range(0, 2) == 1); 
	}
	#endregion

}
