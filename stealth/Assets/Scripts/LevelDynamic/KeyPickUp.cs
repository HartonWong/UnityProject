using UnityEngine;
using System.Collections;

public class KeyPickUp : MonoBehaviour {
	public AudioClip keyGrab;

	private GameObject player;
	private PlayerInventory playerInventory;

	void Awake () {
		player = GameObject.FindGameObjectWithTag (Tags.player);
		playerInventory = player.GetComponent<PlayerInventory>();
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject==player)
		{
			playerInventory.HasKey=true;
			AudioSource.PlayClipAtPoint(keyGrab,transform.position);
			Destroy (gameObject);

		}
	}
}
