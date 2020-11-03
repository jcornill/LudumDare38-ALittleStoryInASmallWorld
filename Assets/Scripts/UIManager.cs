using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public bool isText;
	public string strText;
	public bool blockMoving;

	public GameObject panelText;
	public GameObject textContinue;

	public Text FoodText;
	public Text LifeText;
	public Text gunpowder;

	public GameObject blackScreen;
	public GameObject Dead;
	public GameObject EndnE;
	public bool lockBS = false;

	float hpRegen;

	// Use this for initialization
	void Start () {
		blackScreen.SetActive (true);
		isText = false;
		blockMoving = false;
		UpdateText ();
		SetAlpha (0);
	}

	public void UpdateText()
	{
		panelText.transform.Find ("Text").GetComponent<Text> ().text = strText;
		panelText.SetActive (isText);
		strText = "";
		textContinue.SetActive (blockMoving);
		UpdateGunPowder (0);
	}

	public void UpdateLifeText(int life)
	{
		LifeText.text = "Health : " + life + " % (" + (hpRegen > 0 ? "+" : "") + hpRegen + "/s)";
	}

	public void UpdateFoodText(float regen)
	{
		hpRegen = regen;
		hpRegen = hpRegen * 100.0f;
		hpRegen = Mathf.Round(hpRegen) / 100.0f;
		FoodText.text = "Food : " + (int)QuestManager.Instance.playerFood + " % (-0.2/s)";
	}

	public void UpdateGunPowder(int powder)
	{
		gunpowder.gameObject.SetActive (powder != 0);
		gunpowder.text = "Gunpowder : " + powder + " %";
	}

	// Update is called once per frame
	void Update ()
	{
		if (blockMoving)
			return;
		if (Input.GetKeyDown (KeyCode.W) ||
			Input.GetKeyDown (KeyCode.Z) ||
			Input.GetKeyDown (KeyCode.S) ||
			Input.GetKeyDown (KeyCode.D) ||
			Input.GetKeyDown (KeyCode.A) ||
			Input.GetKeyDown (KeyCode.Q))
		{
			if (isText)
			{
				isText = false;
				UpdateText ();
			}
		}
	}
	public void SetAlpha(float pct)
	{
		if (lockBS)
			return;
		pct /= 100f;
		Color c = blackScreen.GetComponent<Image> ().color;
		c.a = pct;
		blackScreen.GetComponent<Image> ().color = c;
	}
}
