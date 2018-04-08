﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Threading;

public class zombieScript : MonoBehaviour
{

	private Transform t_Player;
	// Finding player
	//private FirstPersonController m_Player; disabled until fixing the unrealistic behavior of camera shaking effect.
	static Animator anim;
	private bool health = true;
	// Use this for initialization

	// cordinates that if the player die the zombie will wake randomly
	private float x;
	private float z;
	//sounds
	public string DeathSound;
	public string IdleSound;
	public string AttackSound;

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
			anim.SetBool ("isWalking", true);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isBiting", false);
			anim.SetBool ("isIdle", false);
		
		} else {	
			Vector3 direction = CalculateDiffVector();
			float angle = Vector3.Angle (direction, this.transform.forward);

			direction.y = 0;
			if (direction.magnitude <= DifficulityControlScript.DistanceMagnitute && angle <= DifficulityControlScript.SeeAngle) {
				anim.SetBool ("isWalking", true);
				anim.SetBool ("isAttacking", false);
				anim.SetBool ("isBiting", false);
				anim.SetBool ("isIdle", false);
				Translate (direction);
				if (anim.GetBool ("isWalking") == true) {
					if (CalculateDiffVector().magnitude <= 2) {	
						anim.SetBool ("isWalking", false);
						anim.SetBool ("isIdle", false);
						anim.SetBool ("isBiting", false);
						anim.SetBool ("isAttacking", true);
						SoundManagerScript.PlaySound (AttackSound);
						//m_Player.ShakePlayer (/* Duration*/ DifficulityControlScript.CameraShakingDuration, /*Shaking Power*/ 
						//DifficulityControlScript.CameraShakingPower, /*Slow down amount*/ DifficulityControlScript.CameraShakingSlowDownAmount);

						// TODO
						// Above function was disabled due to unrealistic behavior (disabled by andrewnagyeb)
					}
				} 
				if (anim.GetBool ("isAttacking") == true || anim.GetBool ("isBiting") == true) {
					if (direction.magnitude > 2) {	
						anim.SetBool ("isAttacking", false);
						anim.SetBool ("isBiting", false);
						anim.SetBool ("isIdle", false);
						anim.SetBool ("isWalking", true);
					} 
				}
			} else {
				anim.SetBool ("isAttacking", false);
				anim.SetBool ("isBiting", false);
				anim.SetBool ("isWalking", false);
				anim.SetBool ("isIdle", true);
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
}
