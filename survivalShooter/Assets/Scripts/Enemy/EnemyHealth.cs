using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;	//how fast is the enemy disappearing/sinking through the floor
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> (); //not from the parent object but 
																//search from the child object for type ParticleSystem
																//under Zombunny->HitParticles there is Particle System properties
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)	//is the enemy dead?
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime); //-Vector3.up is moving down
							//always *Time.deltaTime to move per seconds instead of per frame
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)	//if enemy is dead, no need to take damage again
            return;

        enemyAudio.Play ();		//play the enemy hurt sound effect

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;	//set the position of the hitParticles at the hitPoint
        hitParticles.Play();				//play that particle effect

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;	//making the enemy no longer an obstacle because 
											//trigger collider is not an obstacle in the game

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;		//reset the enemy's audio to deathClip
        enemyAudio.Play ();					//and play that audio
    }


    public void StartSinking ()
    {
        GetComponent <NavMeshAgent> ().enabled = false;		//not moving the dead enemy along with the player
		//note .isActive=false is to turn off the whole game object
		//note .enabled=false is to turn off only the component of that game object
        GetComponent <Rigidbody> ().isKinematic = true;		//so that Unity is not going to recalculate
															//what to do with this object
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);		//after two seconds, the enemy should be completely underground and we can 
										//destroy it
    }
}
