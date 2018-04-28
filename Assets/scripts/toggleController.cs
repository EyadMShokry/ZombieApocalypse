using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleController : MonoBehaviour {
	GameObject mission1;
	GameObject mission2;
	GameObject mission3;

	GameObject position;
		// Use this for initialization
	void Start () {

		position = GameObject.FindGameObjectWithTag ("missionsTag");

		mission1 = GameObject.Find("mission1");
		mission2 = GameObject.Find("mission2");
		mission3 = GameObject.Find("mission3");

		mission1.GetComponent<Toggle> ().isOn = false;
		mission2.GetComponent<Toggle> ().isOn = false;
		mission3.GetComponent<Toggle> ().isOn = false;

		mission1.GetComponent<Toggle> ().enabled = false;
		mission2.GetComponent<Toggle> ().enabled = false;
		mission3.GetComponent<Toggle> ().enabled = false;
		position.SetActive (false);
		mission1.GetComponent<Toggle> ().isOn = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab))
			position.SetActive (!position.activeSelf);
		}
}
