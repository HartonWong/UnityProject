using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour {

	public int dyingState;
	public int deadBool;

	public int sneakState;
	public int sneakBool;

	public int locomotionState;
	public int speedFloat;

	public int idleState;

	public int shoutState;
	public int shoutBool;

	public int playerInsightBool;
	public int shotFloat;
	public int aimWeightFloat;
	public int angularSpeedFloat;
	public int openBool;


	void Awake()
	{
		dyingState = Animator.StringToHash ("Base Layer.Dying");
		deadBool = Animator.StringToHash ("Dead");

		sneakState = Animator.StringToHash ("Base Layer.Sneak");
		sneakBool = Animator.StringToHash ("Sneaking");

		locomotionState = Animator.StringToHash ("Base Layer.Locomotion");
		speedFloat = Animator.StringToHash ("Speed");

		idleState = Animator.StringToHash ("Base Layer.Idle");

		shoutState = Animator.StringToHash ("Shouting.Shout");
		shoutBool = Animator.StringToHash ("Shouting");

		playerInsightBool = Animator.StringToHash ("PlayerInSight");

		shotFloat = Animator.StringToHash ("Shot");
		aimWeightFloat = Animator.StringToHash ("AimWeight");
		angularSpeedFloat = Animator.StringToHash ("AngularSpeed");
		openBool = Animator.StringToHash ("Open");
	}
}
