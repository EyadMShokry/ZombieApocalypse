using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyAdjusment : MonoBehaviour {
	public Dropdown Game_difficulty;
	public int DifficultyInex;
	private string mode = "";
	// Use this for initialization
	void Start () {
		//difficultyScript = gameObject.GetComponent<ScriptName> ();
		Game_difficulty = GetComponent<Dropdown>();
	}
	
	// Update is called once per frame
	void Update () {
		DifficultyInex = Game_difficulty.value;
		if (DifficultyInex == 0) {
			mode = "easy";
		} else if (DifficultyInex == 1) {
			mode = "medium";
		} else if (DifficultyInex == 2){
			mode = "hard";
		}
		DifficulityControlScript.DiffMode (mode);
	}
}
