using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject asteroid;
	public GameObject enemy;
	public Vector3 spawnValues;
	public int hazardCount;

	//Hazard spawn Wave
	public float spawnWait;
	public float startWait;
	public float waveWait;

	//Display Text
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	//private members
	private int score;

	//Flag to end game
	private bool gameOver;
	private bool restart;

	void Start()
	{
		//display nothing on GUI text of Gameover and restart
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";

		//Reset score and update
		score = 0;
		UpdateScore ();

		//Start the game timer
		StartCoroutine (SpawnWave ());
	}

	void Update()
	{
		//if we are in restart stage
		if (restart) 
		{
			//and R key is pressed
			if (Input.GetKeyDown(KeyCode.R))
		    {
				//then reload this level (not going to change the difficulties)
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWave()
	{
		yield return new WaitForSeconds(startWait);
		while (true) 
		{
			for (int i=0; i<hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				if ((Random.Range(0,2))==1)
				{				
					Instantiate (enemy, spawnPosition, spawnRotation);
					Debug.Log("Asteroid created");
				}
				else
				{
					Instantiate (asteroid, spawnPosition, spawnRotation);
					Debug.Log("Enemy created");
				}
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			//if gameOver true,print out text for user to choose restart or not
			if (gameOver) {
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	//public function to be called by player to indicate game over
	public void GameOver()
	{
		gameOverText.text = "Game Over!!";
		gameOver = true;
	}

}
