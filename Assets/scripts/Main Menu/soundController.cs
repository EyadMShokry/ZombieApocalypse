using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class soundController : MonoBehaviour
{
    public AudioSource audio;
    public Canvas startmenu;
    public Canvas videoPlayer;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "MainMenu" || videoPlayer.gameObject.activeSelf)
        {
            audio.clip.UnloadAudioData();
        }
        else if (scene.name == "MainMenu" && startmenu.gameObject.activeSelf)
        {

            SoundManagerScript.PlaySound("startingMusic");
        }
    }
}
