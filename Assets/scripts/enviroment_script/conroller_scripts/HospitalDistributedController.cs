using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalDistributedController : MonoBehaviour {

	private Vector3[] postion_health={
		new Vector3(6.81f,0.074f,19.9f),
		new Vector3(14.39f,0.074f,36.55f),
		new Vector3(31.34f,0.074f,19.95f),
		new Vector3(7.34f,0.074f,9.64f),
		new Vector3(-6.15f,0.074f,9.64f),
		new Vector3(-6.15f,0.074f,15.51f),
		new Vector3(19.71f,0.074f,19.9f),
		new Vector3(8.7f,0.074f,1.1f),
		new Vector3(8.7f,0.074f,7.37f),
		new Vector3(31.41f,0.074f,7.37f),
		new Vector3(27.25f,0.074f,7.37f),
		new Vector3(40f,0.074f,-8.62f),
		new Vector3(26.1f,0.074f,-8.62f),
		new Vector3(11.19f,0.074f,-5.51f),
		new Vector3(2.34f,0.074f,-8.72f),
		new Vector3(-10.53f,0.074f,-8.72f),
		new Vector3(-24.25f,0.074f,-8.72f),
		new Vector3(-21.36f,0.074f,7.28f),
		new Vector3(-7.93f,0.074f,7.28f),
		new Vector3(3.11f,0.074f,7.28f)
	};
	private Vector3[] postion_Ammo={
		new Vector3(-22.05f,0.032f,-8.72f),
		new Vector3(-8.4f,0.032f,-8.72f),
		new Vector3(4.3f,0.032f,-2.5f),
		new Vector3(21.9f,0.032f,-2.5f),
		new Vector3(32.1f,0.032f,-5.1f),
		new Vector3(32.1f,0.032f,6.4f),
		new Vector3(32.1f,0.032f,0.1f),
		new Vector3(2.6f,0.032f,0.1f),
		new Vector3(2.6f,0.032f,3.4f),
		new Vector3(19.9f,0.032f,16.7f),
		new Vector3(14.3f,0.032f,16.7f),
		new Vector3(26.4f,0.032f,24.5f),
		new Vector3(21f,0.032f,24.5f),
		new Vector3(5.9f,0.032f,24.5f),
		new Vector3(5.9f,0.032f,33.6f),
		new Vector3(23.9f,0.032f,33.6f)
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
				Instantiate (Ammo[type], postion_Ammo [MyNumber], Quaternion.identity);
			} else {
				i--;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
