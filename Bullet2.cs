using UnityEngine;
using System.Collections;

public class Bullet2 : MonoBehaviour
{
	public float speed=10f;
	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		transform.Translate (Vector3.down * Time.deltaTime * speed);
		if (transform.position.y <= -14f)
			Destroy (gameObject);
	}
	void OnTriggerEnter2D(Collider2D other){
		if ((other.CompareTag ("Player")))
			Destroy (gameObject);
	}
}