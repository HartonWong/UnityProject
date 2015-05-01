using UnityEngine;

public class MedKitManager: MonoBehaviour
{
	public PlayerHealth playerHealth;
	public GameObject medKit;
	public float spawnTime = 3f;
	public Vector3 spawnValues;
	
	
	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	
	
	void Spawn ()
	{
		if(playerHealth.currentHealth <= 0f)
		{
			return;
		}
		
		Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x),2f, Random.Range(-spawnValues.z,spawnValues.z));
		Instantiate (medKit, spawnPosition, medKit.transform.rotation);
	}
}
