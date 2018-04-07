using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;

public class PlayerHealthScript : MonoBehaviour
{
	static PlayerHealthScript instance;
	public static PlayerHealthScript Instance { get { return instance; } }
	public int maxHealth = 100;
	float currentHealth = 100;
	public SimpleHealthBarScript healthBar;
	public Animation anim;
	public AnimationClip die;
	private bool Isdie = false;
	public Transform[] health;
	private int number_of_exists_health=0;
	private int[] values_instanse = {
		1,
		2,
		3,
		4,
		5,
		6
	};
	private float[] values_distance = {
		0.1f,
		0.2f,
		0.3f,
		0.4f,
		0.5f,
		0.6f
	};
	 
	private Vector3[,] postions = { 
		{new Vector3(-23.15f,0f,-2.29f),new Vector3(-23.15f,0f,-8.57f),new Vector3(-16.03f,0f,-5.62f),new Vector3(-12.98f,0f,-7.89f)},
		{new Vector3(-9.84f,0f,-7.89f),new Vector3(-9.84f,0f,-2.19f),new Vector3(-2.08f,0f,-2.19f),new Vector3(-2.08f,0f,-15.26f)},
		{new Vector3(1.9f,0f,-2.74f),new Vector3(5.21f,0f,-2.74f),new Vector3(2.94f,0f,-5.93f),new Vector3(1.31f,0f,-8.79f)},
		{new Vector3(7.91f,0f,-4.99f),new Vector3(36.3f,0f,-8.8f),new Vector3(-3.58f,0f,-4.99f),new Vector3(16.39f,0f,-4.99f)},
		{new Vector3(19.74f,0f,-1.96f),new Vector3(26.28f,0f,-1.96f),new Vector3(26.28f,0f,-7.97f),new Vector3(22.98f,0f,-6.46f)},
		{new Vector3(30.6f,0f,-8.8f),new Vector3(40f,0f,-8.8f),new Vector3(40f,0f,-3.9f),new Vector3(34.2f,0f,-2.5f)},
		{new Vector3(34.2f,0f,6.8f),new Vector3(21.9f,0f,6.8f),new Vector3(8.9f,0f,1.4f),new Vector3(8.9f,0f,7.9f)},
		{new Vector3(4.8f,0f,7.5f),new Vector3(3.1f,0f,1.9f),new Vector3(-16.5f,0f,1.9f),new Vector3(-18.6f,0f,6.8f)},
		{new Vector3(-11.54f,0f,31.39f),new Vector3(-15.26f,0f,31.39f),new Vector3(-14.83f,0f,28.66f),new Vector3(-11.53f,0f,28.85f)},
		{new Vector3(-3.58f,0f,28.85f),new Vector3(-3.58f,0f,32.35f),new Vector3(-7.24f,0f,32.35f),new Vector3(-6.36f,0f,30.06f)},
		{new Vector3(4.98f,0f,29.41f),new Vector3(7.74f,0f,35.78f),new Vector3(14.26f,0f,32.78f),new Vector3(12.86f,0f,28.7f)},
		{new Vector3(22.5f,0f,29.9f),new Vector3(28.7f,0f,28.8f),new Vector3(28.7f,0f,36.3f),new Vector3(22.8f,0f,36.3f)},
		{new Vector3(23.4f,0f,19.8f),new Vector3(29.8f,0f,26.2f),new Vector3(10.4f,0f,24.9f),new Vector3(10.4f,0f,19f)},
		{new Vector3(6.8f,0f,19.5f),new Vector3(2.9f,0f,24.7f),new Vector3(-4.6f,0f,25.5f),new Vector3(-23.3f,0f,26f)},
		{new Vector3(-7.87f,0f,11.31f),new Vector3(19.67f,0f,10.06f),new Vector3(5.94f,0f,9.96f),new Vector3(11.92f,0f,9.9f)}
	};
	private List<int> room_busy=new List<int>(); 
	void Awake()
	{
		// If the instance variable is already assigned, then there are multiple player health scripts in the scene. Inform the user.
		if (instance != null)
			Debug.LogError("There are multiple instances of the Player Health script. Assigning the most recent one to Instance.");

		// Assign the instance variable as the Player Health script on this object.
		instance = GetComponent<PlayerHealthScript>();
	}

	void Start()
	{
		// Set the current health and shield to max values.
		currentHealth = maxHealth;

		// Update the Simple Health Bar with the updated values of Health and Shield.
		healthBar.UpdateBar(currentHealth, maxHealth);
	}

	public void HealPlayer(int health)
	{
		// Increase the current health by 25%.
		currentHealth += health;
		// If the current health is greater than max, then set it to max.
		if (currentHealth > maxHealth)
			currentHealth = maxHealth;
		// Update the Simple Health Bar with the new Health values.
		healthBar.UpdateBar(currentHealth, maxHealth);
	}

	public void TakeDamage(int damage)
	{

		if (currentHealth > 0)
		{
			currentHealth -= damage;
			if (currentHealth > 10)
			{
				if (!SoundManagerScript.audioSrc.isPlaying)
				{
					SoundManagerScript.PlaySound ("characterHurt");
				}
			}

			if (currentHealth <= 10 && currentHealth != 0)
			{
				if (!SoundManagerScript.audioSrc.isPlaying) 
				{
					SoundManagerScript.PlaySound ("characterHurt");
					SoundManagerScript.PlaySound ("characterHeavyBreathing");
				}
			}
			if (currentHealth == 0)
			{
				SoundManagerScript.PlaySound("characterDeath");
			}

			//add health component

			if (currentHealth < 10) 
			{
				number_of_exists_health = increase_instance_health (values_instanse[5],values_distance[5],number_of_exists_health);
			} 
			else if (currentHealth < 20) 
			{
				number_of_exists_health = increase_instance_health (values_instanse[4],values_distance[4],number_of_exists_health);
			}
			else if (currentHealth < 30) 
			{
				number_of_exists_health = increase_instance_health (values_instanse[3],values_distance[3],number_of_exists_health);
			}
			else if (currentHealth < 40) 
			{
				number_of_exists_health = increase_instance_health (values_instanse[2],values_distance[2],number_of_exists_health);
			}
			else if (currentHealth < 50) 
			{
				number_of_exists_health = increase_instance_health (values_instanse[1],values_distance[1],number_of_exists_health);
			}
			else if (currentHealth < 70) 
			{
				number_of_exists_health = increase_instance_health (values_instanse[0],values_distance[0],number_of_exists_health);
			}
		}

		else
		{
			// Set the current health to zero.
			currentHealth = 0;

			// Run the Death function since the player has died.
			StartCoroutine(Death());
		}

		// Update the Health and Shield status bars.
		healthBar.UpdateBar(currentHealth, maxHealth);

	}

	void OnTriggerStay(Collider other)
	{
		int index = getRoom(other);
		if (other.gameObject.CompareTag("healthLarge"))
		{
			SoundManagerScript.PlaySound ("PickingUp");
			other.gameObject.SetActive(false);
			HealPlayer(70);
			room_busy.Remove (index);
			number_of_exists_health--;
		}
		if (other.gameObject.CompareTag("healthMid"))
		{
			SoundManagerScript.PlaySound ("PickingUp");
			other.gameObject.SetActive(false);
			HealPlayer(50);
			room_busy.Remove (index);
			number_of_exists_health--;
		}
		if (other.gameObject.CompareTag("healthSmall"))
		{
			SoundManagerScript.PlaySound ("PickingUp");
			other.gameObject.SetActive(false);
			HealPlayer(20);
			room_busy.Remove (index);
			number_of_exists_health--;
		}

		if (other.gameObject.CompareTag("damageKits"))
		{
			other.gameObject.SetActive(false);
			TakeDamage(10);
		}
		if (other.gameObject.CompareTag("zombieGirl"))
		{
			TakeDamage((int)Math.Ceiling(Time.deltaTime/1500));
		}
		if (other.gameObject.CompareTag("zombiePolice"))
		{
			TakeDamage((int)Math.Ceiling(Time.deltaTime/2700));
		}
			
	}





	public IEnumerator Death()
	{
		if (!Isdie) 
		{
			UnityStandardAssets.Characters.FirstPerson.FirstPersonController.isplayerDeath = true;
			anim.Play (die.name);
			Isdie = true;
			yield return new WaitForSeconds(3);
			foreach(Transform firstperson in transform)
			{
				foreach(Transform weaponSwatch in firstperson)
				{
					foreach(Transform weapon in weaponSwatch)
					{
						weapon.gameObject.SetActive (false);
					}
					break;
				}
				break;
			}

		}
	}








	public int increase_instance_health(int number_of_instanse, float distance , int number_of_exists_instanse)
	{
		int number = number_of_instanse - number_of_exists_instanse;
		if(number>0)
		{
			int type = 0;
			Vector3[] postions_with_minmum_distance=get_postions(number_of_instanse,distance);
			for (int i = 0; i < number; i++) 
			{

				type = UnityEngine.Random.Range (0, health.Length);
				if (type == 0) {
					Vector3 temp = new Vector3 ();
					temp.x = postions_with_minmum_distance[i] .x;
					temp.y = 0.1f;
					temp.z = postions_with_minmum_distance[i] .z;
					Instantiate (health[type], temp, Quaternion.identity);
				} else {
					Instantiate (health[type], postions_with_minmum_distance[i] , Quaternion.identity);
				}

			}
		}
		if (number_of_instanse > number_of_exists_instanse) {
			return number_of_instanse;
		} else {
			return number_of_exists_instanse;
		}
	}








	private Vector3[] get_postions(int number_of_instanse,float distance)
	{
		Vector3 self_transform = transform.position;
		float[,] minmum_distance = new float[15, 2];
		Vector3[] postions_with_minmum_distance = new Vector3[number_of_instanse];
		for (int i = 0; i < 15 ; i++) 
		{
			float minmum = 500f;
			for (int j = 0; j < 4; j++) 
			{

				float dist = Vector3.Distance(postions[i,j], self_transform);
				float Value_Distanse =Mathf.Abs(dist - distance) ;
				if (minmum > Value_Distanse) 
				{
					minmum = Value_Distanse;
				}
			}
			minmum_distance [i, 0] = i;
			minmum_distance [i, 1] = minmum;
		}

		for (int i = 0; i < 14; i++) 
		{
			for (int j = 0; j < 14 -i; j++) 
			{
				if(minmum_distance[j,1]>minmum_distance[j+1,1])
				{
					float[] temp = new float[2];
					temp[0]=minmum_distance [j,0];
					temp[1]=minmum_distance [j,1];
					minmum_distance [j,0] = minmum_distance [j + 1,0];
					minmum_distance [j,1] = minmum_distance [j + 1,1];
					minmum_distance [j + 1,0]=temp[0];
					minmum_distance [j + 1,1]=temp[1];
				}
			}
		}
		int m = 0;
		for (int i = 0; i < 15; i++) 
		{				
			if (!room_busy.Contains (Mathf.RoundToInt (minmum_distance [i, 0]))) {
				postions_with_minmum_distance [m] = postions [Mathf.RoundToInt( minmum_distance [i,0]), UnityEngine.Random.Range (0, 4)];
				room_busy.Add (Mathf.RoundToInt (minmum_distance [i, 0]));
				m++;
				if(m==number_of_instanse)
				{
					break;
				}
			}	

		}
			return postions_with_minmum_distance;
	}





	private int getRoom(Collider other)
	{
		Vector3 self_position = other.gameObject.transform.position;
		for (int i = 0; i < 15; i++) {
			for (int j = 0; j < 4; j++) {
				if (postions [i, j] == self_position) {
					return i;
				}
			}
		}
		return 0;

	}


}



