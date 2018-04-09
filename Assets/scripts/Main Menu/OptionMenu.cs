using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour {
	Text Precentage_text;
    public Canvas startMenu;
    public Canvas optionMenu;

	void start(){
		Precentage_text = GetComponent<Text>();
		Debug.LogError(Precentage_text);
	}

	void back_to_mainMenu()
	    {
        optionMenu.enabled = false;
        startMenu.enabled = true;
	    }

	public void text_update(float value)
	    {
		
	       Precentage_text.text = Mathf.RoundToInt(value * 100) + "%";
	    }
}
