using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchToHospital : MonoBehaviour {
	public bool TriggerEntered = false;
	public Text message;
	public Transform door;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.E) && TriggerEntered ){
			message.text = "";
			TriggerEntered = false;
			SceneManager.LoadScene("enviroment");
		}
		if (CalculateDiffVector ().magnitude < 10) {
			TriggerEntered = true;
			message.text = "press e to enter the hospital";
		} else {
			TriggerEntered = false;
			message.text = "";
		}

	}
	private Vector3 CalculateDiffVector ()
	{
		return door.position - this.transform.position;
	}


}
