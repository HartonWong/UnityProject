using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosions;
    public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start()
	{	
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");//find game object with tag "GameController"
		if (gameControllerObject != null) {					//if we found such object, the class member 
														//gameControllObject is going to get the generated gameController
			gameController=gameControllerObject.GetComponent<GameController>();
		}

		//just in case we can't find gameController, console will show the reason for error 
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}

	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")//if the other object is with boundary tag, do nothing and go no further
        {
            return;
        }

        Instantiate(explosions, transform.position, transform.rotation);//show the explosion of Asteroid at asteroid's position

		if (other.tag=="Player")//if the other object is with player tag, show the explosion at player's position
        {
           Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
        }

		//call the gameController AddScore function to add score.
		gameController.AddScore (scoreValue);

		Destroy(other.gameObject);//destroy the player,which is the other gameObject in contact in this case
		Destroy(gameObject);	//destroy this gameObject, which is the asteroid.
    }
}
