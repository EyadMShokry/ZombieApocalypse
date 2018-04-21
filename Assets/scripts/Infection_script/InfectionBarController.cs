/*
This script as a firt look is written in a non familiar way and is hard to uderstand why?
the aim is to check if the palyer is triggered with the antizin or not to make that we should 
check at each frame if the player is ontrigger with the antizin or not and that is impossible
so i followed a way that handle the probelm 

problem reference : https://answers.unity.com/questions/328391/how-to-call-a-ontriggerenter-function-in-a-update.html

Mina
*/

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
	public float timeLeft = 60.0f; // the value of time that the infection bar deacrease when passed
	private float initialInfectionValue = 25; // initial value
	private float maxInfectionValue = 100; // maximum value
	private float decreaseRate = 10; // the rate that the infection bar decreases with (decrease by time)
	private float increaseRate = 10; // the rate that the infection bar increases with (collectin antizin)
	private float barRatio = 0.0f;

	void Start () {
		updateInfectionBar();
	}
	
	void updateInfectionBar(){
		barRatio = initialInfectionValue / maxInfectionValue;
		bar.rectTransform.localScale = new Vector3(barRatio,1,1);
		infection_text.text = (barRatio*100).ToString() + '%';
	}
	// Update is called once per frame150
	void Update () {
		timeLeft -= Time.deltaTime;
		if(timeLeft < 0){
			timeLeft = 60.0f;
			initialInfectionValue -= decreaseRate;
		}
		updateInfectionBar();
		if (Input.GetKey(KeyCode.E) && antizinTriggerEntered == true){
			initialInfectionValue += increaseRate;
			if (initialInfectionValue > 100) {
				initialInfectionValue = 100;
			}
			Antizin.gameObject.SetActive (false);
			antizin_collection_message.text = "";
			antizinTriggerEntered = false;
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

