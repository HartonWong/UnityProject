using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public AudioClip shoutingClip;
	public float turnSmoothing =15f;
	public float speedDampTime=0.1f;

	private Animator anim;
	private HashIDs hash;
	private AudioSource audio;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();
		anim.SetLayerWeight (1, 1f);
		audio = GetComponent<AudioSource> ();
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal"); 
		float v = Input.GetAxis ("Vertical");
		bool sneak = Input.GetButton ("Sneak"); 

		MovementManagement (h, v, sneak);
	}

	void Update()
	{
		bool shout = Input.GetButtonDown ("Attract");
		anim.SetBool (hash.shoutBool, shout);
		AudioManagement (shout);
	}

	void MovementManagement(float h, float v,bool sneaking)
	{
		anim.SetBool (hash.sneakBool, sneaking);
		if (h != 0f || v != 0f) 
		{
			Rotating (h,v);
			anim.SetFloat(hash.speedFloat,5.5f,speedDampTime,Time.deltaTime);
		}
		else
		{
			anim.SetFloat(hash.speedFloat,0f);
		}
	}

	void Rotating(float h, float v)
	{
		Vector3 targetDirection = new Vector3 (h, 0f, v);
		Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up); //change the vector3 into a rotational variable

		Quaternion newRotation = Quaternion.Lerp (GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing*Time.deltaTime);

		GetComponent<Rigidbody>().MoveRotation (newRotation);


	}

	void AudioManagement(bool shout)
	{
		if(anim.GetCurrentAnimatorStateInfo(0).fullPathHash==hash.locomotionState)
		{
			if(!audio.isPlaying)
			{
				audio.Play(); 
			}
		}
		else
		{
			audio.Stop();
		}
		if (shout)
		{
			AudioSource.PlayClipAtPoint(shoutingClip,transform.position);
		}
	}
}
