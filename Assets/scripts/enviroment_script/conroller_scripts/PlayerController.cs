using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class PlayerController : MonoBehaviour {
	private Rigidbody rb;
	public float speed ;
	void Start (){
		rb = GetComponent<Rigidbody>();
	}
	void FixedUpdate () {
		float morehorizontal = Input.GetAxis("Horizontal");
		float morevertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3 (morevertical, 0.0f, morehorizontal);

		rb.AddForce(movement*speed);
	}


}
