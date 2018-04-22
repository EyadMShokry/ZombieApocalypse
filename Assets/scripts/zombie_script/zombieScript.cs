using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Threading;
using UnityEngine.AI;

public class zombieScript : MonoBehaviour
{
	#region PRIVATE VARIABLES
	private Transform t_Player;

	// Finding player
	private FirstPersonController m_Player;
	static Animator anim;
	private bool health = true;
	// Use this for initialization

	// cordinates that if the player die the zombie will wake randomly
	private float x;
	private float z;

	// A reference to zombie nav mesh agent
	private NavMeshAgent zombie;

	// Using this to trigger the zombie awarness state and chasing mode
	private bool isAware = false;

	#endregion

	#region PUBLIC VARIABLES

	// sounds
	public string DeathSound;
	public string IdleSound;
	public string AttackSound;

	#endregion

	// Integer that is randomized to either choose to attack or bite.
	bool RANDOMIZED_STATE_INIT;


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
		anim = GetComponent<Animator> ();
		t_Player = GameObject.Find ("Player").transform;
		m_Player = GameObject.FindObjectOfType<FirstPersonController>(); // Initializing the player object in order to use some of its method.
	}
	
	// Update is called once per frame
	void Update ()
	{
		// If the zombie is out of health, play the animation and sound for zombie death
		if (!health) {
			anim.SetBool ("isDie", true);
			SoundManagerScript.PlaySound (DeathSound);
		}

		// Breaking The chase mode
		Vector3 direction = CalculateDiffVector();
		if(direction.magnitude >= 10){
			UnTriggerAwareState ();
		}

		// If the player is dead, make the zombie walk randomly
		if (UnityStandardAssets.Characters.FirstPerson.FirstPersonController.isplayerDeath == true) {
			Translate (new Vector3 (x, 0, z));
			SetZombieState ("Walk");
		} else {
			if (isAware) {
				// Our zombie chasing logic
				//zombie.SetDestination(t_Player.position);
				ChasePlayer ();
			} else {
				SearchForPlayer ();
			}
		}
		if (AmIChasing () == "isBitting") {
			if (Input.GetKeyDown (KeyCode.G))
				Debug.Log ("Grasping Out");
			//TODO
			// Add the bar which outputs the progress state the player want to achieve in order to grasp out from the biting animation.
		}
	}

	void OnTriggerEnter(MeshFilter other)
	{


		if (other.GetComponent<MeshFilter> ().name == "Wall") {

			transform.Rotate(0, 180,0);

			transform.Translate(Vector3.forward * 1*Time.deltaTime);

			//Destroy (gameObject);


		}


	}

	#endregion

	#region Custome made Functions
	/*
	 * Following function is used to set state
	 * @param state to be set
	 */

	private void Translate (Vector3 direction){
		// The last parameter of Quaternion.Slerp is multiplied by Time.deltaTime because,
		// someone could run the game at 100fps while someone else could run the game at 50fps so the slerp will be faster for the person that can run the game at 100fps
		this.transform.rotation = Quaternion.Slerp 
			(this.transform.rotation, Quaternion.LookRotation (direction),DifficulityControlScript.RotationSpeed * Time.deltaTime);
		this.transform.Translate (0, 0, DifficulityControlScript.ZombieSpeed*Time.deltaTime);
	}

	private Vector3 CalculateDiffVector ()
	{
		return t_Player.position - this.transform.position;
	}

	private void SearchForPlayer(){
		// Setting zombie idle animation and sound
		SetZombieState ("Idle");
		SoundManagerScript.PlaySound (IdleSound);

		/* The Following section is responsible for the line of sight concept for the zombie */

		// This lines is responsible for measuring the angle between the player and the zombie
		// InverseTransformPoint: This transform the player position from zero global scope (this means that the zero point of the player could be anywhere) to a local scope (this means that the zombie will be the zero point)
		// We also divide by two to check only 60 degree in the right or left direction
		if(Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(t_Player.position)) < DifficulityControlScript.SeeAngle/2f ){
			if(Vector3.Distance(t_Player.position, transform.position) < DifficulityControlScript.DistanceMagnitute){
				TriggerAwareState ();
			}
		}
	}

	// Funciton contains zombie chasing logic
	private void ChasePlayer(){
		Vector3 direction = CalculateDiffVector();
		direction.y = 0;
		SetZombieState ("Running");
		DifficulityControlScript.ZombieSpeed = 2.0f;
		DifficulityControlScript.RotationSpeed = 1.0f;
		Translate (direction);
		if (anim.GetBool ("isRunning") == true) {
			if (CalculateDiffVector().magnitude <= 5) {	
				// Randomizing State
				RANDOMIZED_STATE_INIT = true;/*randomBoolean();*/

				if (RANDOMIZED_STATE_INIT) {
					SetZombieState ("Attack");
				} else {
					SetZombieState("Bite");
					m_Player.BlockReleaseInput(true);
				}

				//SoundManagerScript.PlaySound (AttackSound);
				//m_Player.ShakePlayer (/* Duration*/ DifficulityControlScript.CameraShakingDuration, /*Shaking Power*/ 
				//DifficulityControlScript.CameraShakingPower, /*Slow down amount*/ DifficulityControlScript.CameraShakingSlowDownAmount);

				// TODO
				// Above function was disabled due to unrealistic behavior (disabled by andrewnagyeb)
			}
		} 
		if ((anim.GetBool ("isAttacking") == true || anim.GetBool ("isBitting") == true) && isAware == true) {
			if (direction.magnitude > 2) {	
				SetZombieState ("Running");
			} 
		}
	}

	// A fucntion to trigger aware state of zombie
	public void TriggerAwareState(){
		isAware = true;
	}

	// A fucntion to trigger aware state of zombie
	public void UnTriggerAwareState(){
		isAware = false;
	}

	#endregion

	#region PRIVATE FUNCTIONS
	/*
	 * Following function is used to set state
	 * @param state to be set
	 */

	private void SetZombieState(string state){
		switch (state) {
		case "Attack":
			anim.SetBool ("isAttacking", true);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isBitting", false);
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isGraspedOut", false);

			break;
		case "Bite":
			anim.SetBool ("isBitting", true);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isGraspedOut", false);

			break;
		case "Idle":
			anim.SetBool ("isIdle", true);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isBitting", false);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isGraspedOut", false);

			break;
		case "Walk":
			anim.SetBool ("isWalking", true);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isBitting", false);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isGraspedOut", false);

			break;
		case "Running":
			anim.SetBool ("isRunning", true);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isBitting", false);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isGraspedOut", false);

			break;
		case "GraspOut":
			anim.SetBool ("isGraspedOut", true);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isBitting", false);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("isRunning", true);
			break;
		}
	}

	private bool randomBoolean ()
	{
		return (Random.Range(0, 2) == 1); 
	}
	private string AmIChasing(){
		if (anim.GetBool ("isBitting"))
			return "isBitting";
		if (anim.GetBool ("isWalking"))
			return "isWalking";
		if (anim.GetBool ("isIdle"))
			return "isIdle";
		if (anim.GetBool ("isAttacking"))
			return "isAttacking";
		if (anim.GetBool ("isRunning"))
			return "isRunning";
		return "";
	}
	#endregion

}
