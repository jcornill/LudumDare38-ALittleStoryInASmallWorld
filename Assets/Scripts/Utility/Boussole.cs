using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boussole : MonoBehaviour {

	public GameObject center;

	public List<GameObject> objectifs;

	void Update()
	{
		if (objectifs.Count > 0)
		{
			if (center && objectifs [0])
			{
				center.transform.LookAt (objectifs [0].transform.position);
				Vector3 rot = center.transform.eulerAngles;
				rot.x = rot.x - 90;
				transform.eulerAngles = rot;
			}
		}
		else
		{
			gameObject.SetActive (false);
			transform.parent.Find ("Sphere").gameObject.SetActive (false);
		}
	}

	public void TargetNext()
	{
		objectifs.RemoveAt (0);
	}

}
