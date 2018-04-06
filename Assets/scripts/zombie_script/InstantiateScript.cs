using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateScript : MonoBehaviour {

	public GameObject ZombieGirl;
	public GameObject ZombiePolice;
	public GameObject ZombieCop;

	private float PlaceX;
	private float PlaceZ;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < DifficulityControlScript.ZombiesNumber; i++) {
			PlaceX = Random.Range (-22, 22); // should change according to values of the room
			PlaceZ = Random.Range (-26, 12); // should change according to values of the room
			ZombieGirl.transform.position = new Vector3 (PlaceX, 0, PlaceZ);
			PlaceX = Random.Range (-22, 22); // should change according to values of the room
			PlaceZ = Random.Range (-26, 12); // should change according to values of the room
			ZombiePolice.transform.position = new Vector3 (PlaceX, 0, PlaceZ);
			PlaceX = Random.Range (-22, 22); // should change according to values of the room
			PlaceZ = Random.Range (-26, 12); // should change according to values of the room
			ZombieCop.transform.position = new Vector3 (PlaceX, 0, PlaceZ);
			Instantiate (ZombieGirl,  new Vector3 (PlaceX, 0, PlaceZ), Quaternion.identity);
			Instantiate (ZombiePolice,  new Vector3 (PlaceX, 0, PlaceZ), Quaternion.identity);
			Instantiate (ZombieCop,  new Vector3 (PlaceX, 0, PlaceZ), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
