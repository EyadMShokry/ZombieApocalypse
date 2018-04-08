using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateScript : MonoBehaviour {

	public GameObject ZombieGirl;
	public GameObject ZombiePolice;
	public GameObject ZombieCop;

	// Positions that zombies can randomize in it
	private float[] X_start = { -18.0f, 6.0f, 2.0f, -27.0f, 4.0f };
	private float[] X_end = { 0.0f, 20.0f, 12.0f, -5.0f, 26.0f };
	private float[] Z_start = { 19.0f, 19.0f, 4.0f, -5.0f, -5.0f };
	private float[] Z_end = { 19.5f, 19.5f, 9.0f, -2.0f, -2.0f };

	private int index; // index of the array randomiz in range of the array size

	private float PlaceX;
	private float PlaceZ;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < DifficulityControlScript.ZombiesNumber; i++) {

			index = Random.Range (0, 4);
			PlaceX = Random.Range (X_start[index], X_end[index]); // should change according to values of the room
			PlaceZ = Random.Range (Z_start[index], Z_end[index]); // should change according to values of the room
			ZombieGirl.transform.position = new Vector3 (PlaceX, 0, PlaceZ);
			index = Random.Range (0, 4);
			PlaceX = Random.Range (X_start[index], X_end[index]); // should change according to values of the room
			PlaceZ = Random.Range (Z_start[index], Z_end[index]); // should change according to values of the room
			Instantiate (ZombieGirl,  new Vector3 (PlaceX, 0, PlaceZ), Quaternion.identity);

			index = Random.Range (0, 4);
			PlaceX = Random.Range (X_start[index], X_end[index]); // should change according to values of the room
			PlaceZ = Random.Range (Z_start[index], Z_end[index]); // should change according to values of the room
			ZombiePolice.transform.position = new Vector3 (PlaceX, 0, PlaceZ);
			index = Random.Range (0, 4);
			PlaceX = Random.Range (X_start[index], X_end[index]); // should change according to values of the room
			PlaceZ = Random.Range (Z_start[index], Z_end[index]); // should change according to values of the room
			Instantiate (ZombiePolice,  new Vector3 (PlaceX, 0, PlaceZ), Quaternion.identity);

			index = Random.Range (0, 4);
			PlaceX = Random.Range (X_start[index], X_end[index]); // should change according to values of the room
			PlaceZ = Random.Range (Z_start[index], Z_end[index]); // should change according to values of the room
			ZombieCop.transform.position = new Vector3 (PlaceX, 0, PlaceZ);
			index = Random.Range (0, 4);
			PlaceX = Random.Range (X_start[index], X_end[index]); // should change according to values of the room
			PlaceZ = Random.Range (Z_start[index], Z_end[index]); // should change according to values of the room
			Instantiate (ZombieCop,  new Vector3 (PlaceX, 0, PlaceZ), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
