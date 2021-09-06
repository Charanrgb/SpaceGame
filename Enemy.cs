using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public GameObject enemyBullet;
	public float speed=5f;
	public static float timeUntilEnemyArrives=15f;
	public float time1=1f;
	public GameObject player;
	public GameObject SpaceShipFire;
	public int hitSize=10;
	public GameObject explosion;
	public static bool defeated;
	int phase;
	int deaths;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("spawnBullets", timeUntilEnemyArrives, 0.5f); //bullet start time,delay time
		//InvokeRepeating("enemy", 5f,1f);    //enemy move start time,delay time
		defeated=false;
		deaths = 0;
		phase = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (deaths == 2)
			phase = 2;
		else if (deaths >= 3)
			phase = 3;
		else
			phase = 1;
		SpaceShipFire.transform.position = new Vector3 (transform.position.x, transform.position.y + 1); //particle effect
		time1 += Time.deltaTime;
		if (time1 >=timeUntilEnemyArrives) {
			defeated = false;
			transform.position = Vector3.MoveTowards (transform.position, new Vector3(player.transform.position.x,8.15f,0f), speed * Time.deltaTime);
		}

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (hitSize <= 0){
			Instantiate (explosion, transform.position, transform.rotation);
			fallingObjects.score += 100;
			defeated = true;
			transform.position = new Vector3 (7f, 25f, 0f);
			time1 = 1f;
			hitSize = 10;
			deaths++;
		}
		if (other.CompareTag ("PlayerLaser")) {
			hitSize--;
		}

	}
	void spawnBullets()
	{
		if (defeated != true && phase == 1)
			Instantiate (enemyBullet, transform.position, transform.rotation);
		else if (defeated != true && phase == 2) {
			Instantiate (enemyBullet, new Vector3(transform.position.x+1f,transform.position.y,transform.position.z), transform.rotation);
			Instantiate (enemyBullet, new Vector3(transform.position.x-1f,transform.position.y,transform.position.z), transform.rotation);
		}
		else if (defeated != true && phase == 3) {
			Instantiate (enemyBullet, transform.position, transform.rotation);
			Instantiate (enemyBullet, new Vector3(transform.position.x+1f,transform.position.y,transform.position.z), transform.rotation);
			Instantiate (enemyBullet, new Vector3(transform.position.x-1f,transform.position.y,transform.position.z), transform.rotation);
		}
		
	}
}