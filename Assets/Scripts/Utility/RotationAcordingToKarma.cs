using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAcordingToKarma : MonoBehaviour {

	public float currentSpeed;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector3 rot = transform.eulerAngles;
		currentSpeed = QuestManager.Instance.playerKarma / 100.0f;
		if (currentSpeed < -20)
			rot.z += -20;
		else if (currentSpeed > 20)
			rot.z += 20;
		else
			rot.z += currentSpeed;
		transform.eulerAngles = rot;
	}
}
