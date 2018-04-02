using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenuAdjusment : MonoBehaviour {

	public void setGraphicsQuality (int qualityIndex){
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	public void setFullScreen (bool isFullScreen){
		Screen.fullScreen = isFullScreen;
	}
}
