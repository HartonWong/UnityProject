using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;		//grab the enemy's anim
    GameObject player;	//reference to the player
    PlayerHealth playerHealth;	//reference to the script PlayerHealth
    EnemyHealth enemyHealth;
    bool playerInRange;	//is the player in range to attack?
    float timer;		//keep everything in sync


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();	//from the player object to pull out that PlayerHealth script
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)	//if player enter that sphere collider range
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }


	void OnTriggerExit (Collider other) //if player exits that sphere collider range
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0) 
			//to make sure not attack so frequently and player is in range
        {
            Attack ();
        }

        if(playerHealth.currentHealth <= 0)		//note that enemy object is assess playerHealth script's public variable currentHealth
        {
            anim.SetTrigger ("PlayerDead");	//turn on the Idle animation of the enemy
        }
    }


    void Attack ()
    {
        timer = 0f;	//reset the timer after attack

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);		//call the Take Damage function in playerHealth
        }
    }
}
