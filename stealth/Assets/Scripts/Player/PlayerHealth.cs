using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public float health=100f;
	public float resetAfterDeathTime=5f;
	public AudioClip deathClip;

	private Animator anim;
	private PlayerMovement playerMovement;
	private AudioSource footStepAudio;
	private HashIDs hash;
	private LastPlayerSighting lastPlayerSighting;
	private ScreenFadeInOut screenFadeInOut;
	private float timer=0f;
	private bool playerDead;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		playerMovement = GetComponent<PlayerMovement> ();
		footStepAudio = GetComponent<AudioSource> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
		screenFadeInOut = GameObject.FindGameObjectWithTag (Tags.fader).GetComponent<ScreenFadeInOut> ();
		playerDead = false;
	}

	void Update()
	{
		if (health<=0f)
		{
			if(!playerDead)				//if player has not already dead
			{
				PlayerDying();
			}
			else
			{
				PlayerDead ();
				LevelReset();
			}
		}
	}
	public void TakeDamage(float amount)
	{
		health -= amount;
	}
	void PlayerDying()
	{
		playerDead = true;
		anim.SetBool (hash.deadBool, playerDead);
		AudioSource.PlayClipAtPoint (deathClip, transform.position);
	}

	void PlayerDead()
	{
		if(anim.GetCurrentAnimatorStateInfo(0).fullPathHash==hash.dyingState)
		{
			anim.SetBool(hash.deadBool,false);
		}
		anim.SetFloat (hash.speedFloat, 0f);
		playerMovement.enabled = false;
		lastPlayerSighting.position = lastPlayerSighting.resetPosition;
		footStepAudio.Stop ();
	}

	void LevelReset()
	{
		timer += Time.deltaTime;
		if (timer>=resetAfterDeathTime)
		{
			screenFadeInOut.EndScene();
		}
	}

}
