using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitMenu_in_pauseMenu : MonoBehaviour {

    public void ExitPress()  //the button quit
    {
        Application.Quit();
        Debug.Log("Game is exiting"); //Just to make sure its working
    }
}
