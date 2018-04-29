using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class EnemyManager : MonoBehaviour {

	public GameObject enemy;                // The enemy prefab to be spawned.
	public float spawnTime = 5f;            // How long between each spawn.
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	public TextMeshProUGUI WaveDisplay;
	public TextMeshProUGUI KillsDisplay;
	private int currentWave = 1;
	private int numOfKills = 0;
	private int numOfZombiePerWave = 10;
	private int spawnedZombies = 0;
	private List<GameObject> enemies = new List<GameObject> ();


	void Start (){
		WaveDisplay.SetText ("Wave: " + currentWave);
		KillsDisplay.SetText ("Kills: " + numOfKills);
	}

	void Update(){
		if (spawnedZombies != numOfZombiePerWave) {
			NextWave ();
		}
		if(UnityStandardAssets.Characters.FirstPerson.FirstPersonController.isplayerDeath == false){
			StartCoroutine (Spawn ());	
		}
	}

	private void NextWave (){
		numOfZombiePerWave += 10;
		currentWave += 1;
		WaveDisplay.SetText ("Wave: " + currentWave);
	}


	IEnumerator Spawn ()
	{
		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		yield return new WaitForSeconds (spawnTime);
		GameObject zombie = Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

		// increasing number of spawned zombies counter
		spawnedZombies += 1;

		// append all enemies
		enemies.Add (zombie);
	}

	public void destroyZombie(){
		KillsDisplay.SetText ("Kills: " + numOfKills);
	}

}
