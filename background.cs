using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour {
	public AudioSource bg;
	public float speed = 3f;
	Vector2 offset;
	private Renderer renderer;
	public Material[] materials;
	private void awake()
	{
		
	}
	// Use this for initialization
	void Start () {
		bg.playOnAwake = true;
		 offset = new Vector2 (0, speed);
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Renderer>().material.mainTextureOffset += offset*Time.deltaTime;
	}
}
