using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectsScript : MonoBehaviour {
	
	public bool enterCollider = false;
	private float alpha = 0.0f;
	private float fadeDir = 0.0f;
	public Texture2D myGUITexture;
	float fadeSpeed = 0.0f;
	int drawDepth = 0;

	// Use this for initialization
	void Start () {
		alpha = 1.0f;
		fadeDir = -1.0f;
		fadeSpeed = 0.8f;
		drawDepth = -1000;
	}
	
	void OnGUI() {

		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		if (enterCollider == true) {
			alpha += fadeDir * fadeSpeed * Time.deltaTime;
			alpha = Mathf.Clamp01(alpha); // So ALpha is not negaitve
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), myGUITexture);
		}
		GUI.depth = drawDepth;
	}

	public void runBloodEffect(){
		enterCollider = true; 
	}

	public IEnumerator disableBloodEffect(){
		yield return new WaitForSeconds (1);
		enterCollider = false;
		alpha = 1.0f;
	}
}
