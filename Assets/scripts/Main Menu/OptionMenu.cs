using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour {
	Text Precentage_text;

	void start(){
		Precentage_text = GetComponent<Text>();
		Debug.LogError(Precentage_text);
	}

	void back_to_mainMenu()
	    {
	       SceneManager.LoadScene("MainMenu");
	    }

	public void text_update(float value)
	    {
	       Precentage_text.text = Mathf.RoundToInt(value * 100) + "%";
	    }
}
