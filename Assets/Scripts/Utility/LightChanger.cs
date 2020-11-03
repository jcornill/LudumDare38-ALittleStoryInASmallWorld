using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChanger : MonoBehaviour {

	public float intMin;
	public float intMax;
	public float changeSpeed;

	void Start()
	{
		InvokeRepeating ("ChangeLight", 0, changeSpeed);
	}
		
	void ChangeLight () {
		GetComponent<Light> ().intensity = Random.Range (intMin, intMax);
	}
}
