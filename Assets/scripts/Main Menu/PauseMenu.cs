using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


public class PauseMenu : MonoBehaviour {

    Animator anim;
   
    public static bool GameIsPaused = false ;

    public GameObject pauseMenu;
    public Canvas optionMenu;

    private void Start()
    {
        optionMenu.enabled = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }	
	}

   public void Resume(){

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

   }

    void Pause()
    {
        pauseMenu.SetActive(true);   //enable pause menu
        Time.timeScale = 0f;    //freeze the game
        GameIsPaused = true;
    }

    public void option()
    {
        pauseMenu.SetActive(false);
        optionMenu.enabled=true;

        anim.Play("buttonTweenAnims_on");
    }
}
