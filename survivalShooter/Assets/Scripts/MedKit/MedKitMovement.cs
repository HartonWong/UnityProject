using UnityEngine;
using System.Collections;

public class MedKitMovement: MonoBehaviour {

	public float tumble=10f;
	public int healthPoint=20;
	Light light;
	bool trigger=true;

	GameObject player;
	PlayerHealth playerHealth;	//reference to the script PlayerHealth

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();	//from the player object to pull out that PlayerHealth script

		rigidbody.angularVelocity = new Vector3(0,tumble,0);
		light = GetComponent<Light> ();
	}

	void Update(){
		light.enabled=trigger;
		trigger = !trigger;
	}
	void OnTriggerEnter (Collider other)	//if player enter that sphere collider range
	{
		if(other.tag == "Player")
		{
			playerHealth.TakeDamage(-healthPoint);
		}
		Destroy(gameObject);
	}
}
