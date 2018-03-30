using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

    public Canvas quitMenu;
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
    }

    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        quitText.enabled = false;
    }

    public void NoPress()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        quitText.enabled = true;
    }

	public void Go_option_menu(){
		SceneManager.LoadScene("OptionMenu");
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
