using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrputeOpjects : MonoBehaviour {

	// Use this for initialization
	public Transform[] objects;
	private float[] TransformY={
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0
	};
	private bool[] rotateX={
		false,
		false,
		false,
		false,
		true,
		false,
		false,
		false
	};
	public string Environment;
	private int[] number_of_instanse = {
		DifficulityControlScript.number_of_instanse_health_Small,
		DifficulityControlScript.number_of_instanse_health_Mid,
		DifficulityControlScript.number_of_instanse_health_Large,
		DifficulityControlScript.number_of_instanse_Ammo_9m,
		DifficulityControlScript.number_of_instanse_Ammo_4m,
		DifficulityControlScript.number_of_instanse_Zombie_Cop,
		DifficulityControlScript.number_of_instanse_Zombie_Girl,
		DifficulityControlScript.number_of_instanse_health_Police
	};


	void Start () {
		if (Environment == "City") {
			DstputeOpjects (57f,-23.1f, 36f,-1.3f);
		} else {
			DstputeOpjects (57f,-23.1f, 36f,-1.3f);
		}

	}




	public void DstputeOpjects(float maxRangeX , float beginX ,float maxRangeZ,float beginZ)
	{
		for (int i = 0; i < objects.Length; i++) {
			DstputeOpject (objects[i],number_of_instanse[i],TransformY[i],rotateX[i], maxRangeX, beginX, maxRangeZ, beginZ);
		}

	}
		

	private void DstputeOpject(Transform ObjectToCreate , int number_of_instanse, float positionY ,bool Rotate , float maxRangeX , float beginX ,float maxRangeZ,float beginZ)
	{
		int RangeX;
		List <float> RangeXbusy = new List<float>();
		float range = 100f;

		float beginY=1.17f;

		float RangeZ;
		List <float> RangeZbusy = new List<float>();

		int countCreated = 0;
		while (countCreated != number_of_instanse) {
			RangeX=Random.Range (0, (int)maxRangeX);
			RangeZ=Random.Range (0, (int)maxRangeZ);


			if (RangeXbusy.Contains (RangeX) && RangeZbusy.Contains (RangeZ)) {
				continue;
			}


			RaycastHit hit;
			transform.position = new Vector3 (beginX + RangeX, beginY, beginZ + RangeZ);
			Ray ray = new Ray (transform.position, transform.forward );
			if (Physics.Raycast (ray, out hit, range)) {
				if (hit.collider.CompareTag ("floor")) {

					RangeXbusy.Add (RangeX);
					RangeZbusy.Add (RangeZ);
					Vector3 temp = new Vector3 ();
					temp.x = beginX + RangeX;
					temp.y = positionY;
					temp.z = beginZ + RangeZ;
					if (Rotate) {
						Quaternion newQuaternion = new Quaternion();
						newQuaternion.Set (-1f, 0f, 0f, 1);
						Instantiate (ObjectToCreate, temp, newQuaternion);
					} else {
						Instantiate (ObjectToCreate, temp, Quaternion.identity);
					}
					countCreated++;
				}


			}
		}
	}





}
