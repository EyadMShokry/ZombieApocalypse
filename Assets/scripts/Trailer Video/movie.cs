using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class movie : MonoBehaviour {

    public Canvas startmenu;
    public Canvas videoPlayer;

  // public VideoPlayer video;

    // Use this for initialization
    void Start () {
        startmenu.gameObject.SetActive(false);
        videoPlayer.gameObject.SetActive(true);
        //video.Play();
        //((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();

    }

    // Update is called once per frame
    void Update () {
      /*      
       if ( video.time== 25f) {
            videoPlayer.enabled = false;
            startmenu.enabled = true;
        }*/
        if (Input.GetButtonDown("Jump"))
        {
            videoPlayer.gameObject.SetActive(false);
            startmenu.gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(skip_when_fineshed());
        }



    }
    IEnumerator skip_when_fineshed()
    {
        print(Time.time);
        yield return new WaitForSeconds(25.0f);
        print(Time.time);
        videoPlayer.gameObject.SetActive(false);
        startmenu.gameObject.SetActive(true);
    }
}
