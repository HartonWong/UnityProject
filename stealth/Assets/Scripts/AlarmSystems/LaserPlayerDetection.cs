using UnityEngine;
using System.Collections;

public class LaserPlayerDetection : MonoBehaviour {
	private GameObject player;
	private LastPlayerSighting lastPlayerSighting;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag (Tags.player);
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
	}
	
	void OnTriggerStay(Collider other)
	{
		if(GetComponent<Renderer>().enabled && other.gameObject==player)
		{
			lastPlayerSighting.position=other.transform.position; 
		}
	}
}
