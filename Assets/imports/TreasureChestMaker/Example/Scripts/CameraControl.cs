using UnityEngine;
using System.Collections;

namespace TCM{

	public class CameraControl : MonoBehaviour {
	
		private float camSpeed= 0.50f;
		
		void Start()
		{
			SetPosition(); // Set the initial position of the camera, this needed just for this demonstation
		}
		
		void  LateUpdate ()
		{
			if (Input.mousePosition.y <= 2) // If the mouse is at the bottom of the visible screen or lower then continue
			{
				if(transform.position.z >= -25f) // If the camera is within desired Z axis then continue
				{
					transform.Translate(0, 0, -camSpeed, Space.World); // this moves the camera down based on the worlds vectors
				}
			}
			if (Input.mousePosition.y >= Screen.height -2) // If the mouse is at the top of the visible screen or higher then continue
			{
				if(transform.position.z <= 15) // If the camera is within desired Z axis then continue
				{
					transform.Translate(0, 0, camSpeed, Space.World); // this moves the camera up based on the worlds vectors
				}
			}
			if (Input.mousePosition.x <= 2) // If the mouse is at the left of the visible screen or lower then continue
			{
				if(transform.position.x >= -25f) // If the camera is within desired X axis then continue
				{
					transform.Translate(-camSpeed, 0, 0, Space.World); // this moves the camera left based on the worlds vectors
				}
			}
			if (Input.mousePosition.x >= Screen.width -2) // If the mouse is at the right of the visible screen or higher then continue
			{
				if(transform.position.x <= 25)	// If the camera is within desired X axis then continue
				{
					transform.Translate(camSpeed, 0, 0, Space.World); // this moves the camera right based on the worlds vectors
				}
			}
			if(Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.transform.position.y <= 15) // If the mouse scroll wheel is scrolled down and not scrolled further than a number then continue
			{
				transform.Translate(0, 0, -camSpeed); // move the camera back on the z axis
			}
			if(Input.GetAxis("Mouse ScrollWheel") > 0  && Camera.main.transform.position.y >= 5) // If the mouse scroll wheel is scrolled up and not scrolled further than a number then continue
			{
				transform.Translate(0, 0, camSpeed);// move the camera forward on the z axis
			}
		}
		
		private void SetPosition()
		{
			transform.rotation = Quaternion.Euler(40f, 0, 0); // Set the camera's rotation
		}
	}

}