using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


public class PauseMenu : MonoBehaviour {
    Animator anim;
   
    public static bool GameIsPaused = false ;

    public GameObject pauseMenu;    
    public GameObject MainOptionsPanel;
    public GameObject GamePanel;
    public GameObject ControlsPanel;
    public GameObject GfxPanel;

    private FirstPersonController m_Player;

    private void Start()
    {
        MainOptionsPanel.SetActive(false);
        anim = GetComponent<Animator>();
		m_Player = GameObject.FindObjectOfType<FirstPersonController>(); // Initializing the player object in order to use some of its method.
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

        if (Time.timeScale == 0f)
        {
            m_Player.BlockReleaseInput(true);
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
        MainOptionsPanel.SetActive(true);

    }

    public void ExitPress()  //the button quit
    {
        Application.Quit();
        Debug.Log("Game is exiting"); //Just to make sure its working
    }
    public void openOptions_Game()
    {
        //enable respective panel
        GamePanel.SetActive(true);
        ControlsPanel.SetActive(false);
        GfxPanel.SetActive(false);
        MainOptionsPanel.SetActive(false);

    }
    public void openOptions_Controls()
    {
        //enable respective panel
        GamePanel.SetActive(false);
        ControlsPanel.SetActive(true);
        GfxPanel.SetActive(false);
        MainOptionsPanel.SetActive(false);

    }
    public void openOptions_Gfx()
    {
        //enable respective panel
        GamePanel.SetActive(false);
        ControlsPanel.SetActive(false);
        GfxPanel.SetActive(true);
        MainOptionsPanel.SetActive(false);

    }

    public void back_to_pausemenu()
    {
        MainOptionsPanel.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void back_options_panels_GameOptions()
    {
        GamePanel.SetActive(false);
        MainOptionsPanel.SetActive(true);
    }

    public void back_options_panels_controlOptions()
    {
        ControlsPanel.SetActive(false);
        MainOptionsPanel.SetActive(true);
    }

    public void back_options_panels_GFxOptions()
    {
        GfxPanel.SetActive(false);
        MainOptionsPanel.SetActive(true);
    }
}
