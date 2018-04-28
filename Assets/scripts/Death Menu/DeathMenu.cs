using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

	public void retry()
    {
        SceneManager.LoadScene("cityEnvironment");
    }

    public void quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
