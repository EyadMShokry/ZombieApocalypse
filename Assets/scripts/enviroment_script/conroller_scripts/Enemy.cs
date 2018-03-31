using UnityEngine;

public class Enemy : MonoBehaviour {
	
	// health points
	public float health = 50f;

	// destroyed version of box
	//public GameObject destroyedVersion;
	
	// Update is called once per frame
	public void TakeDamage (float amount) {
		health -= amount;
		Debug.Log (health);
		if (health <= 0f) {
			Die ();	
		}
	}

	// contain details for object to die
	void Die(){
		//Instantiate(destroyedVersion, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
