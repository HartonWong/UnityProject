using UnityEngine;
using System.Collections;

public class LiftDoorsTracking : MonoBehaviour {
		
	public float doorSpeed=7f;

	private Transform leftOuterDoor;
	private Transform rightOuterDoor;
	private Transform leftInnerDoor;
	private Transform rightInnerDoor;
	private float leftClosePosX;
	private float rightClosePosX;

	void Awake()
	{
		leftOuterDoor = GameObject.Find ("door_exit_outer_left_001").transform;
		rightOuterDoor = GameObject.Find ("door_exit_outer_right_001").transform; 
		leftInnerDoor = GameObject.Find ("door_exit_inner_left_001").transform;
		rightInnerDoor = GameObject.Find ("door_exit_inner_right_001").transform; 

		leftClosePosX = leftInnerDoor.position.x;
		rightClosePosX = rightInnerDoor.position.x;
	}

	void MoveInnerDoors(float newLeftXTarget,float newRightXTarget)		//this function is used to move inner doors according to outer doors position
	{
		float newX = Mathf.Lerp (leftInnerDoor.position.x, newLeftXTarget, doorSpeed * Time.deltaTime);
		leftInnerDoor.position = new Vector3 (newX, leftInnerDoor.position.y, leftInnerDoor.position.z);

		newX=Mathf.Lerp (rightInnerDoor.position.x, newRightXTarget, doorSpeed * Time.deltaTime);
		rightInnerDoor.position = new Vector3 (newX, rightInnerDoor.position.y, rightInnerDoor.position.z);
	}

	public void DoorFollowing()				//outer doors is moving automatically by animation, by tracking the position of outer doors, we move the inner doors 
	{
		MoveInnerDoors (leftOuterDoor.position.x, rightOuterDoor.position.x);
	}
	public void CloseDoors()
	{
		MoveInnerDoors (leftClosePosX, rightClosePosX);
	}
}
