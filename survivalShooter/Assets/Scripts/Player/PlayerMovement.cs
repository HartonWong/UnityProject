using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed=6f;
	
	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	
	float camRayLength= 100f;
	
	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();//get the component that is Animator type
		playerRigidbody = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate()//update with physics
	{
		float horizontal = Input.GetAxisRaw ("Horizontal"); //Raw axis will either be -1,0,1 while normal axis range from -1 to 1
		//with raw axis when Horizontal is pressed, Axis is snapped to left/right 
		//immediately instead of ramping up to 1
		//Horizontal is input default by Unity, see under Unity Edit->Project Settings->Input 
		float vertical = Input.GetAxisRaw ("Vertical");
		Move (horizontal, vertical);
		Turning ();
		Animating (horizontal, vertical); 
	}
	
	void Move(float horizontal,float vertical)
	{
		movement.Set (horizontal, 0f, vertical);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position+movement);
	}
	
	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);		//start shooting the ray from the mouse position
		RaycastHit floorHit;		//where that camara ray is going to hit the floor
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) //type "out" is going to get information out of floorHit
			//if that camera ray DOES hit the floor
		{
			Vector3 playerToMouse=floorHit.point-transform.position;	//difference between the current player is the rotation that NEED to be done
			playerToMouse.y=0f;			//playerToMouse.y should be zero, this statement is just an insurance statement
			
			Quaternion newRotation= Quaternion.LookRotation(playerToMouse);	//looking from the "rotation that NEED to be done"
			playerRigidbody.MoveRotation(newRotation);
		}
	}
	
	void Animating(float horizontal, float vertical)
	{
		bool walking = false;
		if (horizontal != 0f || vertical != 0f) 
		{
		walking = (horizontal != 0f || vertical != 0f) ;
		anim.SetBool("IsWalking",walking);
		}
	}
	
}

