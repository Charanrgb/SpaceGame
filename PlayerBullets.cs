using UnityEngine;
using System.Collections;

public class PlayerBullets : MonoBehaviour
{
	public float speed=10f;
	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update ()
	{
		transform.Translate (Vector3.up * Time.deltaTime * speed);
		if (transform.position.y >= 13f)
			Destroy (gameObject);
	}
	void OnTriggerEnter2D(Collider2D other){
		if (!(other.CompareTag ("Player"))) 
			Destroy (gameObject);
		

	}
}

