using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fallingObjects : MonoBehaviour {
	public GameObject explosion;
	public float speed = 10f; 
	float RandomXpos=0f;
	private shake shakeanim;
	public int hitSize=1;
	public static int score = 0;
	float timeUntilEnemy=1f;
	int count=0;
	int count1=0;
	// Use this for initialization
	void Start () {
		score = 0;
		shakeanim = GameObject.FindGameObjectWithTag ("ScreenShake").GetComponent<shake>();
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.down * Time.deltaTime * speed);
		//CHECKING IF OBJECTS FELL
		if (transform.position.y < -15f) {
			fallFromAbove ();
		}
	}
	void fallFromAbove()
	{
		resetSize ();
		RandomXpos = Random.Range (-12f, 17f);                                     //---------------------
		transform.position = new Vector3 (RandomXpos, 12f, 0f);
	}

	void resetSize()
	{
		if (this.gameObject.CompareTag ("Asteroid")) {
			hitSize = 3;
			speed = 4;
		} else if (this.gameObject.CompareTag ("Small Asteroid")) {
			hitSize = 2;
			speed = 5;
		} else if (this.gameObject.CompareTag ("Rock")) {
			hitSize = 1;
			speed = 7;
		} else {
			hitSize = 1;
			speed = 7;
		}
		
	}

	void OnTriggerEnter2D(Collider2D other){
		
		if (other.gameObject.CompareTag ("PlayerLaser")) {
			hitSize--;
			if (hitSize <= 0) {
				shakeanim.camShake ();
				if (this.gameObject.CompareTag ("Asteroid"))
					score += 10;
				else if (this.gameObject.CompareTag ("Small Asteroid"))
					score += 5;
				else if (this.gameObject.CompareTag ("Rock"))
					score += 1;
				else
					score += 1;
				
				Instantiate (explosion, transform.position, transform.rotation);
				fallFromAbove ();
			} 
		}
		else if(other.gameObject.CompareTag("Player"))
			{
				hitSize=0;
				Instantiate (explosion, transform.position, transform.rotation);
				fallFromAbove ();
			}
		else if(other.gameObject.CompareTag("Shield"))
		{
			hitSize=0;
			Instantiate (explosion, transform.position, transform.rotation);
			fallFromAbove ();
		}

			
}
}