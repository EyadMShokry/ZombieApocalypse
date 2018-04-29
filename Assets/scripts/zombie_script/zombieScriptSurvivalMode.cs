using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Threading;
using UnityEngine.AI;

public class zombieScriptSurvivalMode : MonoBehaviour
{
	#region PRIVATE VARIABLES

	// Use this for initialization
	private Transform t_Player;
	private Animator anim;
	private bool health = true;


	// cordinates that if the player die the zombie will wake randomly
	private float x;
	private float z;
	private int graspingKeysCount = 0;
	// A reference to zombie nav mesh agent
	private NavMeshAgent zombie;

	// Using this to trigger the zombie awarness state and chasing mode
	private bool isAware = false;

	// Using this to know if zombie is attacking or not
	private bool isAttacking = false;

	// Zombie rotating and moving speed -> they are fixed
	private float ZombieSpeed = 3.0f;
	private float RotationSpeed = 2.0f;

	// visual effects script
	private VisualEffectsScript vsc;

	// Enemy Manager
	private EnemyManager em;

	#endregion

	#region MonoBehaviour Functions and Events

	/*
	 * Following functions are used for the zombie AI
	 * @param Zombie AI
	 */

	void Start ()
	{
		zombie = GetComponent<NavMeshAgent> ();
		x = Random.Range (-28, 26);
		z = Random.Range (-15, 20);
		anim = gameObject.GetComponent <Animator> ();
		t_Player = GameObject.Find ("Player").transform;
		vsc = gameObject.GetComponent <VisualEffectsScript> ();
		em = GameObject.Find ("EnemyManager").GetComponent <EnemyManager> ();
	}

	// Update is called once per frame
	void Update ()
	{
		// If the zombie is out of health, play the animation and sound for zombie death
		if (!health) {
			anim.SetBool ("isDie", true);
			em.destroyZombie ();
		}

		// If the player is dead, make the zombie walk randomly
		if (UnityStandardAssets.Characters.FirstPerson.FirstPersonController.isplayerDeath == true) {
			Translate (new Vector3 (x, 0, z));
			SetZombieState ("Walk");
		} else {
			if (GetCurrentState () != "isDead") {
				ChasePlayer ();
			}
		}
	}

	#endregion

	#region Custome made Functions

	/*
	 * Following function is used to translate zombie's position.
	 * @param direction of the translated objcet.
	 */

	private void Translate (Vector3 direction)
	{
		// The last parameter of Quaternion.Slerp is multiplied by Time.deltaTime because,
		// someone could run the game at 100fps while someone else could run the game at 50fps so the slerp will be faster for the person that can run the game at 100fps
		// deltaTime: The time in seconds it took to complete the last frame
		this.transform.rotation = Quaternion.Slerp 
			(this.transform.rotation, Quaternion.LookRotation (direction), RotationSpeed * Time.deltaTime);
		this.transform.Translate (0, 0, ZombieSpeed * Time.deltaTime);
	}

	private Vector3 CalculateDiffVector ()
	{
		return t_Player.position - this.transform.position;
	}

	// Function contains zombie chasing logic
	private void ChasePlayer ()
	{
		Vector3 direction = CalculateDiffVector ();
		direction.y = 0;
		SetZombieState ("Running");
		if (CalculateDiffVector ().magnitude >= 1.7) {
			isAttacking = false;
			if (GetCurrentState () != "isAttacking" && GetCurrentState () != "isScreaming") {
				Translate (direction);
			}
		} else {
			// Zombie attacks
			isAttacking = true;
			SetZombieState ("Attack");
		}
	}

	// A fucntion to trigger aware state of zombie
	public void TriggerAwareState ()
	{
		isAware = true;
	}

	// A fucntion to untrigger aware state of zombie
	public void UnTriggerAwareState ()
	{
		isAware = false;
	}

	// A fucntion to reflect damage on player when zombie attacks -> this function is fired by an event in the animation
	public void ReflectDamage ()
	{
		PlayerHealthScript healthScript = GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerHealthScript> ();
		vsc.runBloodEffect ();
		StartCoroutine (vsc.disableBloodEffect ());
		healthScript.TakeDamage (10);
	}

	#endregion

	#region PRIVATE FUNCTIONS

	/*
	 * Following function is used to set state
	 * @param state to be set
	 */

	public void SetZombieState (string state)
	{
		switch (state) {
		case "Attack":
			anim.SetBool ("isAttacking", true);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isBitting", false);
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isGraspedOut", false);
			anim.SetBool ("isScreaming", false);
			break;
		case "Bite":
			anim.SetBool ("isBitting", true);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isGraspedOut", false);
			anim.SetBool ("isScreaming", false);
			break;
		case "Idle":
			anim.SetBool ("isIdle", true);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isBitting", false);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isGraspedOut", false);
			anim.SetBool ("isScreaming", false);
			break;
		case "Walk":
			anim.SetBool ("isWalking", true);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isBitting", false);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isGraspedOut", false);
			anim.SetBool ("isScreaming", false);
			break;
		case "Running":
			anim.SetBool ("isRunning", true);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isBitting", false);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isGraspedOut", false);
			anim.SetBool ("isScreaming", false);
			break;
		case "GraspOut":
			anim.SetBool ("isGraspedOut", true);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isBitting", false);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isScreaming", false);
			break;
		case "Die":
			anim.SetBool ("isDie", true);
			anim.SetBool ("isGraspedOut", false);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isBitting", false);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isScreaming", false);
			break;
		case "Scream":
			anim.SetBool ("isScreaming", true);
			anim.SetBool ("isGraspedOut", false);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isBitting", false);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isDie", false);
			break;
		}
	}

	private string GetCurrentState ()
	{
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle"))
			return "isIdle";
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Running"))
			return "isRunning";
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Walk"))
			return "isWalking";
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack"))
			return "isAttacking";
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Die"))
			return "isDead";
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("RunNeck Bitening"))
			return "isBitting";
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("RunnGrasped Outing"))
			return "isGraspedOut";
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Scream"))
			return "isScreaming";
		return "";
	}

	#endregion

}
