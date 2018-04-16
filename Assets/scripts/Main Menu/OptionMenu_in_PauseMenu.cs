using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionMenu_in_PauseMenu : MonoBehaviour
{
    Text Precentage_text;
    public GameObject pauseMenu;
    public Canvas optionMenu;

    void start()
    {
        Precentage_text = GetComponent<Text>();
        Debug.LogError(Precentage_text);
    }

    public void back_to_pausemenu()
    {
        optionMenu.enabled = false;
        pauseMenu.SetActive(true);
    }

    public void text_update(float value)
    {

        Precentage_text.text = Mathf.RoundToInt(value * 100) + "%";
    }


}
