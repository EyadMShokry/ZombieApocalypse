using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {
    /*public float movespeed;

	// Use this for initialization
	void Start () {
        movespeed = 5f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-(movespeed * Time.deltaTime * (Input.GetAxis("Vertical"))), 0f, (movespeed * Time.deltaTime * (Input.GetAxis("Horizontal"))));
		
	}*/

	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start()
	{
		offset = transform.position - player.transform.position;
	}

	// Update is called once per frame
	void LateUpdate()
	{
		transform.position = player.transform.position + offset;
	}
}