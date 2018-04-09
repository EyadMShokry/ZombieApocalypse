using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movie : MonoBehaviour {

    public Canvas startmenu;
    public Canvas videoPlayer;

   //private MovieTexture video;

    // Use this for initialization
    void Start () {
        startmenu.enabled =false;
        //((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
		
	}

    // Update is called once per frame
    void Update () {
            
       /* if ( video.isPlaying== false) {
            videoPlayer.enabled = false;
            startmenu.enabled = true;
        }*/
           
		
	}
}
