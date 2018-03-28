using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundPrecentaeg : MonoBehaviour {
	Text Precentage_text;
	
	void start(){
		Precentage_text = GetComponent<Text> ();
	}

	public void text_update(float value)
	    {
			Debug.LogError(value);
	       Precentage_text.text = Mathf.RoundToInt(value * 100) + "%";
	    }
}
