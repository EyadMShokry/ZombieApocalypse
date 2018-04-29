using UnityEngine;

public class Enemy : MonoBehaviour {
	
	// health points
	public float health = 50f;

	private zombieScript zombieSc;

	public void Start(){
		zombieSc = gameObject.GetComponent<zombieScript> ();
	}
	
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
		Debug.Log ("Die");
		zombieSc.SetZombieState ("Die");
	}
}
