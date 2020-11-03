using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation0360 : MonoBehaviour {

	public float rotationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 rot = transform.eulerAngles;
		rot.z += rotationSpeed;
		transform.eulerAngles = rot;
	}
}
