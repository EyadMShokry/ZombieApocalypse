using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.Characters.FirstPerson;

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
        if (other.gameObject.CompareTag("healthLarge"))
        {
			SoundManagerScript.PlaySound ("PickingUp");
            other.gameObject.SetActive(false);
            HealPlayer(70);
        }
        if (other.gameObject.CompareTag("healthMid"))
        {
			SoundManagerScript.PlaySound ("PickingUp");
            other.gameObject.SetActive(false);
            HealPlayer(50);
        }
        if (other.gameObject.CompareTag("healthSmall"))
        {
			SoundManagerScript.PlaySound ("PickingUp");
            other.gameObject.SetActive(false);
            HealPlayer(20);
        }

        if (other.gameObject.CompareTag("damageKits"))
        {
            other.gameObject.SetActive(false);
            TakeDamage(10);
        }
        if (other.gameObject.CompareTag("zombieGirl"))
        {
			TakeDamage((int)Math.Ceiling(Time.deltaTime/50));
        }
        if (other.gameObject.CompareTag("zombiePolice"))
        {
			TakeDamage((int)Math.Ceiling(Time.deltaTime/70));
        }
     

    }

	public IEnumerator Death()
    {
		if (!Isdie) {
			UnityStandardAssets.Characters.FirstPerson.FirstPersonController.isplayerDeath = true;
			anim.Play (die.name);
			Isdie = true;
			yield return new WaitForSeconds(3);
			int i = 0;
			int j = 0;
			foreach(Transform firstperson in transform){
				if (i == 0) {
					foreach(Transform weaponSwatch in firstperson){
						if (j == 0) {
							foreach(Transform weapon in weaponSwatch){
								weapon.gameObject.SetActive (false);
							}
						}
						j++;
					}

				}
				i++;

			}
		}

    }


}
