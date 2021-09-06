using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shake : MonoBehaviour {
	public Animator camanim;
	// Use this for initialization
	public void camShake(){
		int r = Random.Range (1, 4);
		if(r==1)
		camanim.SetTrigger ("shake");
		else if(r==2)
			camanim.SetTrigger ("shake1");
		else if(r==3)
			camanim.SetTrigger ("shake2");
	}

}
