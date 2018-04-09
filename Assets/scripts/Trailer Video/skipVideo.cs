using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skipVideo : MonoBehaviour {

    public Canvas startmenu;
    public Canvas videoPlayer;



    public void onClick() {
    
        videoPlayer.enabled = false;
        startmenu.enabled = true;
    

    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            videoPlayer.enabled = false;
            startmenu.enabled = true;
        }

    }
}
