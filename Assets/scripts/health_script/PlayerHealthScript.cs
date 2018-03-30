using UnityEngine;
using System.Collections;


public class PlayerHealthScript : MonoBehaviour
    {
        static PlayerHealthScript instance;
        public static PlayerHealthScript Instance { get { return instance; } }
        //bool canTakeDamage = true;

        public int maxHealth = 100;
        float currentHealth = 100;
        //public float invulnerabilityTime = 0.5f;

        //float currentShield = 0;
        //public int maxShield = 25;
        //float regenShieldTimer = 0.0f;
        //public float regenShieldTimerMax = 1.0f;

        //public GameObject explosionParticles;

        public SimpleHealthBarScript healthBar;
        //public SimpleHealthBar shieldBar;


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
            //currentShield = maxShield;

            // Update the Simple Health Bar with the updated values of Health and Shield.
            healthBar.UpdateBar(currentHealth, maxHealth);
            //shieldBar.UpdateBar( currentShield, maxShield );
        }

        /*void Update ()
		{
			// If the shield is less than max, and the regen cooldown is not in effect...
			if( currentShield < maxShield && regenShieldTimer <= 0 )
			{
				// Increase the shield.
				currentShield += Time.deltaTime * 5;

				// Update the Simple Health Bar with the new Shield values.
				shieldBar.UpdateBar( currentShield, maxShield );
			}

			// If the shield regen timer is greater than zero, then decrease the timer.
			if( regenShieldTimer > 0 )
				regenShieldTimer -= Time.deltaTime;
		}*/

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
            }

            else
            {
                // Set the current health to zero.
                currentHealth = 0;

                // Run the Death function since the player has died.
                //Death();
            }

            // Update the Health and Shield status bars.
            healthBar.UpdateBar(currentHealth, maxHealth);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("healthLarge"))
            {
                other.gameObject.SetActive(false);
                HealPlayer(70);
            }
        else if (other.gameObject.CompareTag("healthKits"))
        {
            other.gameObject.SetActive(false);
            HealPlayer(20);
        }
        else if(other.gameObject.CompareTag("damageKits"))
            {
                other.gameObject.SetActive(false);
                TakeDamage(90);
            }
    }

}
