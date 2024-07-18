using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public float speed;
	void Update () {
		RotateMovement ();
	}

	void RotateMovement(){
		transform.Rotate (0, 0, speed * Time.deltaTime);
	}
}
