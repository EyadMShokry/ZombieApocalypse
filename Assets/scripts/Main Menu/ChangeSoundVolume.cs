using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSoundVolume : MonoBehaviour {

    public Slider MusicSlider;

    public AudioSource music;


	// Use this for initialization
	void Start () {
		
	}
	void Awake(){
		DontDestroyOnLoad (MusicSlider);
	}
	
	// Update is called once per frame
	void Update () {

        music.volume = MusicSlider.value;     
    }
}
