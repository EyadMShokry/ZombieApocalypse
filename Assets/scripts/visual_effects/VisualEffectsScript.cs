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
		fadeSpeed = 0.5f;
		drawDepth = -1000;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			enterCollider = true; 
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.CompareTag ("Player")) {
			enterCollider = false;
			alpha = 1.0f;
		}
	}
	
	void OnGUI() {

		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		if (enterCollider == true) {
			alpha += fadeDir * fadeSpeed * Time.deltaTime;
			alpha = Mathf.Clamp01(alpha); // 3lshan alpha matb2ash negative
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), myGUITexture);
		}
		GUI.depth = drawDepth;

	}
}
