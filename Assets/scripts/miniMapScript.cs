using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapScript : MonoBehaviour {

	// Use this for initialization
	public Transform player;
	void LateUpdate()
	{
		Vector3 newPosition = player.position;
		newPosition.y = transform.position.y;
		transform.position = newPosition;
	}
}
