using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class Star : MonoBehaviour {
	public int speed=5;

	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.down*Time.deltaTime*speed);
		if(transform.position.y<=-14f)
			Destroy(gameObject);
	}
	void OnTriggerEnter2D(Collider2D other){
		if ((other.CompareTag ("Player"))) {
			Destroy (gameObject);
		}
	}

}
