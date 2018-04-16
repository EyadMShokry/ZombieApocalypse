using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitMenu_in_pauseMenu : MonoBehaviour {

    public Canvas quitMenu;
    public Canvas pauseMenu;
    public Button resumeText;
    public Button quitText;

    // Use this for initialization
    void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
        pauseMenu = pauseMenu.GetComponent<Canvas>();
        quitText = quitText.GetComponent<Button>();
        resumeText = resumeText.GetComponent<Button>();
        quitMenu.enabled = false;
	}

    public void ExitPress()  //the button quit
    {
        quitMenu.enabled = true;
        resumeText.enabled = false;
        quitText.enabled = false;
    }

    public void NoPress()  //the button No
    {
        quitMenu.enabled = false;
        resumeText.enabled = true;
        quitText.enabled = true;
    }

    public void QuitGame()  //the button yes
    {
        Application.Quit();
        Debug.Log("Game is exiting"); //Just to make sure its working
    }
}
