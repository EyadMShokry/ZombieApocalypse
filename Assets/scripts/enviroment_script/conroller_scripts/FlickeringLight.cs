using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour {

	public float minWaitTime;
	public float maxWaitTime;

	void Start () {
		StartCoroutine(Flashing());
	}

	IEnumerator Flashing ()
	{
		while (true)
		{
			/*yield return new WaitForSeconds(Random.Range(minWaitTime,maxWaitTime));
			gameObject.SetActive(!gameObject.activeInHierarchy);*/
			Debug.Log ("Hello");
		}
	}
}
