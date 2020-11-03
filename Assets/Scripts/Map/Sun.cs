using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

	public float rotationSpeed;
	World world;


	// Use this for initialization
	void Start () {
		world = GameObject.Find ("World").GetComponent<World> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3 (30, transform.eulerAngles.y + rotationSpeed, 0);
		if (transform.eulerAngles.y <= 269.9f + rotationSpeed && transform.eulerAngles.y >= 270.1f - rotationSpeed)
			world.SpawnRats ();
	}
}
