using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using System.Collections;

namespace UnityStandardAssets.Characters.FirstPerson
{
	[RequireComponent (typeof(CharacterController))]
	[RequireComponent (typeof(AudioSource))]
	public class FirstPersonController : MonoBehaviour
	{
		#region PRIVATE VARIABLES | MADE BY ASSET DEVELOPER
		[SerializeField] private bool m_IsWalking;
		[SerializeField] private float m_WalkSpeed;
		[SerializeField] private float m_RunSpeed;
		[SerializeField] [Range (0f, 1f)] private float m_RunstepLenghten;
		[SerializeField] private float m_JumpSpeed;
		[SerializeField] private float m_StickToGroundForce;
		[SerializeField] private float m_GravityMultiplier;
		[SerializeField] private MouseLook m_MouseLook;
		[SerializeField] private bool m_UseFovKick;
		[SerializeField] private FOVKick m_FovKick = new FOVKick ();
		[SerializeField] private bool m_UseHeadBob;
		[SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob ();
		[SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob ();
		[SerializeField] private float m_StepInterval;
		[SerializeField] private AudioClip[] m_FootstepSounds;
		// an array of footstep sounds that will be randomly selected from.
		[SerializeField] private AudioClip m_JumpSound;
		// the sound played when character leaves the ground.
		[SerializeField] private AudioClip m_LandSound;
		// the sound played when character touches back on ground.
		private bool m_Jump;
		private float m_YRotation;
		private Vector2 m_Input;
		private Vector3 m_MoveDir = Vector3.zero;
		private CharacterController m_CharacterController;
		private CollisionFlags m_CollisionFlags;
		private bool m_PreviouslyGrounded;
		private Vector3 m_OriginalCameraPosition;
		private float m_StepCycle;
		private float m_NextStep;
		private bool m_Jumping;
		private AudioSource m_AudioSource;
		#endregion
		#region PRIVATE VARIABLES | MADE BY GAME DEVELOPERS
		private Camera m_Camera;
		// added by andrewnagyeb, camera transformer to shake it.
		private Transform t_Camera;
		// added by andrewnagyeb, camera shaking bool value that describes if the camera is already being shaked.
		private bool shaking_Camera;
		// added by andrewnagyeb, value that illustrates whether user's input is currently blocked.
		private bool blocked_input;
		#endregion
		// Use this for initialization
		#region PUBLIC VARIABLES | MADE BY GAME DEVELOPERS
		public static bool isplayerDeath=false;
		public Animation anim;
		#endregion
		#region DEVELOPED FUNCTIONS
		//added by developer @andrewnagyeb
		public void ShakePlayer (float duration, float shaking_power, float shaking_slow_down_amount)
		{
			if (duration > 0.00f) {
				this.shaking_Camera = !this.shaking_Camera;		
				if (this.shaking_Camera) {
					blocked_input = true;
					this.t_Camera.localPosition = this.m_OriginalCameraPosition + Random.insideUnitSphere * shaking_power;
					duration -= Time.deltaTime * shaking_slow_down_amount;
					this.shaking_Camera = false;
				}
			} 
		}
		
		/*
		* Function to either block player's keyboard input or release it.
		* @paranm bool state.
		*/
		
		public void BlockReleaseInput(bool state){
			this.blocked_input = state;
		}

		public void PlayAnimation(string AnimationName, float delay){
			StartCoroutine (PlayAnimationCouroutine(AnimationName, delay));
		}

		public IEnumerator PlayAnimationCouroutine(string AnimationName, float delay){
			yield return new WaitForSeconds(delay);
			anim.Play (AnimationName);
		}
		#endregion
		private void Start ()
		{
			m_CharacterController = GetComponent<CharacterController> ();
			m_Camera = Camera.main;
			m_OriginalCameraPosition = m_Camera.transform.localPosition;
			m_FovKick.Setup (m_Camera);
			m_HeadBob.Setup (m_Camera, m_StepInterval);
			m_StepCycle = 0f;
			m_NextStep = m_StepCycle / 2f;
			m_Jumping = false;
			m_AudioSource = GetComponent<AudioSource> ();
			m_MouseLook.Init (transform, m_Camera.transform);
			t_Camera = Camera.main.transform;
			shaking_Camera = false;
			blocked_input = false;
		}
		// Update is called once per frame
		private void Update ()
		{
			if(!isplayerDeath)
			{
			RotateView ();
			// the jump state needs to read here to make sure it is not missed
			if (!m_Jump && !blocked_input) {
				m_Jump = CrossPlatformInputManager.GetButtonDown ("Jump");
			}

			if (!m_PreviouslyGrounded && m_CharacterController.isGrounded && !blocked_input) {
				StartCoroutine (m_JumpBob.DoBobCycle ());
				PlayLandingSound ();
				m_MoveDir.y = 0f;
				m_Jumping = false;
			}
			if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded && !blocked_input) {
				m_MoveDir.y = 0f;
			}

			m_PreviouslyGrounded = m_CharacterController.isGrounded;
			}
		}


		private void PlayLandingSound ()
		{
			m_AudioSource.clip = m_LandSound;
			m_AudioSource.Play ();
			m_NextStep = m_StepCycle + .5f;
		}


		private void FixedUpdate ()
		{
			if (!isplayerDeath && !blocked_input) {
				float speed;
				GetInput (out speed);
				// always move along the camera forward as it is the direction that it being aimed at
				Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

				// get a normal for the surface that is being touched to move along it
				RaycastHit hitInfo;
				Physics.SphereCast (transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
					m_CharacterController.height / 2f);
				desiredMove = Vector3.ProjectOnPlane (desiredMove, hitInfo.normal).normalized;

				m_MoveDir.x = desiredMove.x * speed;
				m_MoveDir.z = desiredMove.z * speed;


				if (m_CharacterController.isGrounded) {
					m_MoveDir.y = -m_StickToGroundForce;

					if (m_Jump) {
						m_MoveDir.y = m_JumpSpeed;
						PlayJumpSound ();
						m_Jump = false;
						m_Jumping = true;
					}
				} else {
					m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
				}
				m_CollisionFlags = m_CharacterController.Move (m_MoveDir * Time.fixedDeltaTime);

				ProgressStepCycle (speed);
				UpdateCameraPosition (speed);
			}
		}


		private void PlayJumpSound ()
		{
			m_AudioSource.clip = m_JumpSound;
			m_AudioSource.Play ();
		}


		private void ProgressStepCycle (float speed)
		{
			if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0)) {
				m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) *
				Time.fixedDeltaTime;
			}

			if (!(m_StepCycle > m_NextStep)) {
				return;
			}

			m_NextStep = m_StepCycle + m_StepInterval;

			PlayFootStepAudio ();
		}


		private void PlayFootStepAudio ()
		{
			if (!m_CharacterController.isGrounded) {
				return;
			}
			// pick & play a random footstep sound from the array,
			// excluding sound at index 0
			int n = Random.Range (1, m_FootstepSounds.Length);
			m_AudioSource.clip = m_FootstepSounds [n];
			m_AudioSource.PlayOneShot (m_AudioSource.clip);
			// move picked sound to index 0 so it's not picked next time
			m_FootstepSounds [n] = m_FootstepSounds [0];
			m_FootstepSounds [0] = m_AudioSource.clip;
		}


		private void UpdateCameraPosition (float speed)
		{
			if (!isplayerDeath) {
				Vector3 newCameraPosition;
				if (!m_UseHeadBob) {
					return;
				}
				if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded) {
					m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob (m_CharacterController.velocity.magnitude +
					(speed * (m_IsWalking ? 1f : m_RunstepLenghten)));
					newCameraPosition = m_Camera.transform.localPosition;
					newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset ();
				} else {
					newCameraPosition = m_Camera.transform.localPosition;
					newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset ();
				}
				m_Camera.transform.localPosition = newCameraPosition;
			}
		}


		private void GetInput (out float speed)
		{
			// Read input
			float horizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
			float vertical = CrossPlatformInputManager.GetAxis ("Vertical");

			bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
			// On standalone builds, walk/run speed is modified by a key press.
			// keep track of whether or not the character is walking or running
			m_IsWalking = !Input.GetKey (KeyCode.LeftShift);
#endif
			// set the desired speed to be walking or running
			speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
			m_Input = new Vector2 (horizontal, vertical);

			// normalize input if it exceeds 1 in combined length:
			if (m_Input.sqrMagnitude > 1) {
				m_Input.Normalize ();
			}

			// handle speed change to give an fov kick
			// only if the player is going to a run, is running and the fovkick is to be used
			if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0) {
				StopAllCoroutines ();
				StartCoroutine (!m_IsWalking ? m_FovKick.FOVKickUp () : m_FovKick.FOVKickDown ());
			}
		}


		private void RotateView ()
		{
			m_MouseLook.LookRotation (transform, m_Camera.transform);
		}


		private void OnControllerColliderHit (ControllerColliderHit hit)
		{
			if (!isplayerDeath) {
				Rigidbody body = hit.collider.attachedRigidbody;
				//dont move the rigidbody if the character is on top of it
				if (m_CollisionFlags == CollisionFlags.Below) {
					return;
				}

				if (body == null || body.isKinematic) {
					return;
				}
				body.AddForceAtPosition (m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
			}
		}
	}

}

