using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

    public Canvas quitMenu;
    public Canvas optionMenu;
    public Canvas startMenu;
    public Button options;
    public Button startText;
    public Button quitText;

    // Use this for initialization
    void Start()
    {
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        quitText = quitText.GetComponent<Button>();
        quitMenu.enabled = false;
        optionMenu.enabled = false;
        SoundManagerScript.PlaySound("startingMusic");
    }

    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        quitText.enabled = false;
    }

	public void Start_Survival_mode(){
		SceneManager.LoadScene("Arena");
	}

    public void NoPress()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        quitText.enabled = true;
    }

	public void Go_option_menu(){
        startMenu.enabled = false;
        optionMenu.enabled = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("enviroment");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting"); //Just to make sure its working
    }

}
