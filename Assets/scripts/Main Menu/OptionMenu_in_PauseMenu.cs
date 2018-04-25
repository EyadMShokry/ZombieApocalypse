using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionMenu_in_PauseMenu : MonoBehaviour
{
    Text Precentage_text;
    public GameObject pauseMenu;
    public GameObject optionMenu;

    public void back_to_pausemenu()
    {
        optionMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }


}
