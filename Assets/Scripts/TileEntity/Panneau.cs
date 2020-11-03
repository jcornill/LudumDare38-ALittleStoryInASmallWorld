using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panneau : TileEntity {

	public string text;
	UIManager ui;

	// Use this for initialization
	void Start () {
		ui = GameObject.Find ("Canvas").GetComponent<UIManager> ();
		Init ();
	}

	public override void Action (Player player)
	{
		if (!ui.isText)
		{
			ui.strText = text;
			ui.isText = true;
			ui.UpdateText ();
		}
	}
}
