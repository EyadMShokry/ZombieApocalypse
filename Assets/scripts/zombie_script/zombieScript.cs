using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieScript : MonoBehaviour {

	public Transform player;
	static Animator anim;
	private bool health = true;
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!health) 
		{
			anim.SetBool ("isDie", true);
		}
			
		Vector3 direction = player.position - this.transform.position;
		//float angle = Vector3.Angle (direction, this.transform.forward);

		direction.y = 0;
		this.transform.rotation = Quaternion.Slerp 
			(this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);
		this.transform.Translate (0, 0, 0.005f);
		if (anim.GetBool ("isWalking") == true) {

			if (direction.magnitude < 2) {	
				anim.SetBool ("isAttacking", true);
				anim.SetBool ("isWalking", false);

			}
		} 
		if (anim.GetBool ("isAttacking") == true) {
			if (direction.magnitude > 2) {	
				anim.SetBool ("isAttacking", false);
				anim.SetBool ("isWalking", true);
			} 
		}
	}
}
