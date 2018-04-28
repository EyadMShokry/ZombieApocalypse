using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchToHospital : MonoBehaviour {
	public bool TriggerEntered = false;
	public Text message;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.E) && TriggerEntered == true){
			message.text = "";
			TriggerEntered = false;
			SceneManager.LoadScene("enviroment");
		}

	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("hospitalDoor")) {
			TriggerEntered = true;
			message.text = "press e to enter the hospital";
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.CompareTag ("hospitalDoor")) {
			message.text = "";
			TriggerEntered = false;
		}
	}
}
