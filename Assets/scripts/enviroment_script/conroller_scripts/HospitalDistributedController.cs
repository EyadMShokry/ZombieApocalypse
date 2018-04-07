using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalDistributedController : MonoBehaviour {

	private Vector3[] postion_health={
		new Vector3(-23.15f,0f,-2.29f),
		new Vector3(-9.84f,0f,-7.89f),
		new Vector3(1.9f,0f,-2.74f),
		new Vector3(7.91f,0f,-4.99f),
		new Vector3(19.74f,0f,-1.96f),
		new Vector3(30.6f,0f,-8.8f),
		new Vector3(34.2f,0f,6.8f),
		new Vector3(4.8f,0f,7.5f),
		new Vector3(-11.54f,0f,31.39f),
		new Vector3(-3.58f,0f,28.85f),
		new Vector3(4.98f,0f,29.41f),
		new Vector3(22.5f,0f,29.9f),
		new Vector3(23.4f,0f,19.8f),
		new Vector3(6.8f,0f,19.5f),
		new Vector3(-7.87f,0f,11.31f)
	};
	private Vector3[] postion_Ammo={
		new Vector3(-16.03f,0f,-5.62f),
		new Vector3(-2.08f,0f,-2.19f),
		new Vector3(2.94f,0f,-5.93f),
		new Vector3(-3.58f,0f,-4.99f),
		new Vector3(26.28f,0f,-7.97f),
		new Vector3(40f,0f,-3.9f),
		new Vector3(8.9f,0f,1.4f),
		new Vector3(-16.5f,0f,1.9f),
		new Vector3(-14.83f,0f,28.66f),
		new Vector3(-7.24f,0f,32.35f),
		new Vector3(14.26f,0f,32.78f),
		new Vector3(28.7f,0f,36.3f),
		new Vector3(10.4f,0f,24.9f),
		new Vector3(-4.6f,0f,25.5f),
		new Vector3(5.94f,0f,9.96f)

	};
	public Transform[] health;
	public Transform[] Ammo;
	public int number_of_health_comp=10;
	public int number_of_Ammo_comp=10;
	// Use this for initialization
	void Start () {
		List<int> randomHealthList = new List<int> ();
		int MyNumber = 0;
		int type = 0;
		for (int i = 0; i < number_of_health_comp; i++) {
			MyNumber = Random.Range (0, postion_health.Length);
			if (!randomHealthList.Contains (MyNumber)) {
				randomHealthList.Add (MyNumber);
				type = Random.Range (0, health.Length);
				Instantiate (health[type], postion_health [MyNumber], Quaternion.identity);
			} else {
				i--;
			}
		}
		List<int> randomAmmoList = new List<int> ();
		for (int i = 0; i < number_of_Ammo_comp; i++) {
			MyNumber = Random.Range (0, postion_Ammo.Length);
			if (!randomAmmoList.Contains (MyNumber)) {
				randomAmmoList.Add (MyNumber);
				type = Random.Range (0, Ammo.Length);
				if (type == 1) {
					Vector3 temp = new Vector3 ();
					temp.x = postion_Ammo [MyNumber].x;
					temp.y = 0.1f;
					temp.z = postion_Ammo [MyNumber].x;
					Quaternion newQuaternion = new Quaternion();
					newQuaternion.Set (-1f, 0f, 0f, 1);
					Instantiate (Ammo[type], temp, newQuaternion);
				} else {
					Instantiate (Ammo[type], postion_Ammo [MyNumber], Quaternion.identity);
				}


			} else {
				i--;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
