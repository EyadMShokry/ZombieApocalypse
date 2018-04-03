using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Threading;

public class zombieScript : MonoBehaviour
{

	public Transform t_Player;
	// Finding player
	private FirstPersonController m_Player;
	static Animator anim;
	private bool health = true;
	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();	
		t_Player = GameObject.Find ("Player").transform;
		m_Player = GameObject.FindObjectOfType<FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!health) {
			anim.SetBool ("isDie", true);
		}
			
		Vector3 direction = t_Player.position - this.transform.position;
		//float angle = Vector3.Angle (direction, this.transform.forward);

		direction.y = 0;
		this.transform.rotation = Quaternion.Slerp 
			(this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);
		this.transform.Translate (0, 0, 0.005f);
		if (anim.GetBool ("isWalking") == true) {
			if (direction.magnitude < 2) {	
				anim.SetBool ("isWalking", false);
				anim.SetBool ("isAttacking", true);
				m_Player.ShakePlayer (/* Duration*/ DifficulityControlScript.CameraShakingDuration, /*Shaking Power*/ 
					DifficulityControlScript.CameraShakingPower, /*Slow down amount*/ DifficulityControlScript.CameraShakingSlowDownAmount);
			}
		} 
		if (anim.GetBool ("isAttacking") == true || anim.GetBool("isBiting") == true) {
			if (direction.magnitude > 2) {	
				anim.SetBool ("isAttacking", false);
				anim.SetBool("isBiting", false);
				anim.SetBool ("isWalking", true);
			} 
		}
	}
}
