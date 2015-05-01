using UnityEngine;
using UnityEngine.UI;		//in order to use UI component
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;			//when player is damage, the image to flash on the screen
    public AudioClip deathClip;
	public AudioClip medKitClip;
	public AudioClip playerHurt;
    public float flashSpeed = 5f;		//how quickly the damage image flash on the screen
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);		//completely red, and 10% opaque


    Animator anim;		//reference to animator source
    AudioSource playerAudio;
    PlayerMovement playerMovement;	//reference to the script that we have made previously
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> (); //to get the component of the script we have written, we just use the name of the script
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;		
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
			//if not damage, transition from red color to clear color with the flash speed
        }
		damaged = false;	//reset damage to false to make it normally false
    }

	//function to be called by other function
    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;	//substracted the health by the amount taken

        healthSlider.value = currentHealth;	//update health slider value

		if (amount > 0) {
			playerAudio.clip = playerHurt;
			playerAudio.Play ();		//also play the player hurt sound
		} else {
			playerAudio.clip = medKitClip;		//attached dead audio to player instead of hurt
			playerAudio.Play ();
		}
        if(currentHealth <= 0 && !isDead)	//if current health is less than zero and it is not ALREADY dead
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");  //trigger the Die animation

        playerAudio.clip = deathClip;		//attached dead audio to player instead of hurt
        playerAudio.Play ();				//play that new audio

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

	//public function that is to restart the game
    public void RestartLevel ()
    {
		if (Input.GetKeyDown(KeyCode.R))
		{
			//then reload this level (not going to change the difficulties)
			Application.LoadLevel(Application.loadedLevel);
		}
    }
}
