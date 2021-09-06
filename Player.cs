using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEditor.SceneManagement;
public class Player : MonoBehaviour {
	public float speed=5f;
	public float movement=0f;
	Vector3 pos;
	int count=0;
	private Rigidbody2D rigid;
	public GameObject bullets; //holds link to bullet prefab
	public GameObject explosion;//particle system explosion
	public GameObject SpaceShipFire; 
	public static int lives=3;
	public Text livesText;
	bool enablePowerup1=false;
	bool enablePowerup3=false;
	public int powerUpCount = 1;
	public float timeLeft = 10f;
	public float timeLeft1=10f;
	public Text scoreText;
	public Text GameOverScoreText;
	public GameObject powerUp1;			// LIVES POWERUP
	public GameObject powerUp2;			//BULLET POWERUP
	public GameObject gameOverPanel;
	public AudioSource PlayerHit;
	public GameObject shield;
	public GameObject powerUp3;
	// Use this for initialization
	void Start () {
		shield.SetActive (false);
		lives = 100;
		rigid = GetComponent<Rigidbody2D> ();
		livesText.text = "Lives : "+lives.ToString();
		InvokeRepeating ("powerUp1Fall", 10f, Random.Range(15f,20f));
		InvokeRepeating ("powerUp2Fall", 10f, 8f);
		InvokeRepeating ("powerUp3Fall", 10f, 8f);
		InvokeRepeating ("shootBullets", 1f, 0.3f);
		gameOverPanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		shield.transform.position = transform.position;
		if (enablePowerup3 == false)
			shield.SetActive (false);
		else {
			Debug.Log ("LOL");
			shield.SetActive (true);
			shield.transform.position = gameObject.transform.position;
		}
		scoreText.text= "Score : " + fallingObjects.score.ToString ();
		livesText.text = "Lives : "+lives.ToString();
		pos = new Vector3 (transform.position.x, transform.position.y - 1);
		SpaceShipFire.transform.position = pos;
		movement1 ();
		//EDGES
		if (transform.position.x >= 18f)
			transform.position = new Vector3 (18f, transform.position.y, 0f);
		else if (transform.position.x <= -12.5f)
			transform.position = new Vector3 (-12.5f, transform.position.y, 0f);
			
		if (enablePowerup1 == true)
			timeLeft -= Time.deltaTime;
		if (enablePowerup3 == true)
			timeLeft1 -= Time.deltaTime;
		if (timeLeft <= 0) {						//TIME LEFT FOR	BULLETS POWERUP
			timeLeft = 10f;
			enablePowerup1 = false;
			powerUpCount = 1;
			count = 0;
		}
		if (timeLeft1 <= 0) {						//TIME LEFT FOR SHIELD POWERUP
			timeLeft1 = 10f;
			enablePowerup3 = false;
			shield.SetActive (false);
		}
	}
	void movement1()
	{
		movement = CrossPlatformInputManager.GetAxis ("Horizontal");
		//CONTROL MOVEMENT
		rigid.velocity = new Vector2 (movement * speed, rigid.velocity.y);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (!other.CompareTag ("PlayerLaser") && !other.CompareTag ("PowerUp1") && !other.CompareTag ("PowerUp2")) {
			//PlayerHit.Play ();
			lives--;
			if (lives == 0) {
				livesText.text = "Lives : " + lives.ToString ();
				Instantiate (explosion, transform.position, transform.rotation);
				Destroy (SpaceShipFire);
				gameObject.SetActive (false);
				//gameOverPanel.SetActive (true);
				GameOverScoreText.text = "Score : " + fallingObjects.score.ToString ();


			}
		} else if (other.CompareTag ("PowerUp1") || other.CompareTag ("PowerUp2")) {
			if (other.CompareTag ("PowerUp2")) {	//BULLETS POWERUP
				count += 1;
				enablePowerup1 = true;
				if (count >= 2)
					powerUpCount = 2;
			}
			if (other.CompareTag ("PowerUp1")) 		//LIVES POWERUP
				lives++;
		}
		if (other.CompareTag ("powerUp3")) { 	
			shield.SetActive (true);				//shield POWERUP
			enablePowerup3 = true;
		}
	}	
		
	void powerUp1Fall()
	{
		Instantiate (powerUp1, new Vector3(Random.Range(-12f,12f),12f,0f), transform.rotation);
	}
	void powerUp2Fall()
	{
		Instantiate (powerUp2, new Vector3(Random.Range(-12f,12f),12f,0f), transform.rotation);
	}
	void powerUp3Fall()
	{
		Instantiate (powerUp3, new Vector3(Random.Range(-12f,12f),12f,0f), transform.rotation);
	}
	void shootBullets()
	{
		if (enablePowerup1 == false)
			Instantiate (bullets, transform.position, transform.rotation);
		else if (enablePowerup1 == true && powerUpCount == 2) {
			Instantiate (bullets, transform.position, transform.rotation);
			Instantiate (bullets, new Vector3 (transform.position.x+0.5f, transform.position.y, transform.position.z), transform.rotation);
			Instantiate (bullets, new Vector3 (transform.position.x-0.5f, transform.position.y, transform.position.z), transform.rotation);
		}	
		else {
			Instantiate (bullets, new Vector3 (transform.position.x+0.5f, transform.position.y, transform.position.z), transform.rotation);
			Instantiate (bullets, new Vector3 (transform.position.x-0.5f, transform.position.y, transform.position.z), transform.rotation);
		}
	}

}
