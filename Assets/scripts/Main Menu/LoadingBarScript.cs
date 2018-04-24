using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBarScript : MonoBehaviour {

	private bool loadScene = false;
	public string LoadingSceneName;
	public Text loadingText;
	public Slider sliderBar;

	// Use this for initialization
	void Start () {

		//Hide Slider Progress Bar in start
		sliderBar.gameObject.SetActive(false);

	}

	// Update is called once per frame
	void Update () {

		// If the player has pressed the space bar and a new scene is not loading yet...
		if (loadScene == false)
		{

			// ...set the loadScene boolean to true to prevent loading a new scene more than once...
			loadScene = true;

			//Visible Slider Progress bar
			sliderBar.gameObject.SetActive(true);

			// ...and start a coroutine that will load the desired scene.
			StartCoroutine(LoadNewScene(LoadingSceneName));

		}

	}

	// The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
	IEnumerator LoadNewScene(string sceneName) {

		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

		// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		while (!async.isDone)
		{
			float progress = Mathf.Clamp01(async.progress / 0.9f);
			sliderBar.value = progress;
			loadingText.text = progress * 100f + "%";
			yield return null;

		}

	}

}