using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate = 0.5f;
	private float nextFire = 0.0f;
	
	void Update()
	{
		if (Time.time>nextFire)
		{
			nextFire=Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audio.Play ();
		}
		/*if (Time.time>nextFire)
		{
			nextFire=Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audio.Play ();
		}*/
	}
}
