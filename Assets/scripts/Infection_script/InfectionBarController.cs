using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfectionBarController : MonoBehaviour {
	public Image bar;
	public Text infection_text;
	public Text antizin_collection_message;
	public bool antizinTriggerEntered = false;
	public bool infectionRateincreasingFlag = false;
	public Collider Antizin;

	private float hitpoint = 25;
	private float maxHitpoint = 100;
	private float decreaseRate = 10;
	private float increaseRate = 10;
	// Use this for initialization
	void Start () {
		updateInfectionBar();
	}
	
	void updateInfectionBar(){
		float ratio = hitpoint / maxHitpoint;
		bar.rectTransform.localScale = new Vector3(ratio,1,1);
		infection_text.text = (ratio*100).ToString() + '%';
	}
	// Update is called once per frame150
	void Update () {
		updateInfectionBar();
		if (Input.GetKey(KeyCode.E) && antizinTriggerEntered == true){
			hitpoint += increaseRate;
			if (hitpoint > 100) {
				hitpoint = 100;
			}
			Antizin.gameObject.SetActive (false);
			antizin_collection_message.text = "";
			antizinTriggerEntered = false;
			//infectionRateincreasingFlag = false;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Antizin")) {
			Antizin = other;
			antizinTriggerEntered = true;
			antizin_collection_message.text = "press e to pick antizin up";
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.CompareTag ("Antizin")) {
			antizin_collection_message.text = "";
			antizinTriggerEntered = false;
		}
	}
}

