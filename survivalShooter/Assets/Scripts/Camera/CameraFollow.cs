using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothing=5f;

	Vector3 offset;

	void Start()
	{
		offset = transform.position - target.position;//offset is the standard distance 
													// between the player and the camera
	}

	void FixedUpdate()
	{
		Vector3 targetCamPos = target.position + offset; //camera position is always a standard distance
														//away from player
		transform.position = Vector3.Lerp (transform.position, targetCamPos,smoothing*Time.deltaTime);
		//smoothly move the camera from original position to the new position 
	}
}

