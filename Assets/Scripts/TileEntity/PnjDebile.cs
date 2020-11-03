using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PnjDebile : PanneauPnj {

	float tttime;

	void Start()
	{
		tttime = Time.time;
		Init ();
		ui = GameObject.Find ("Canvas").GetComponent<UIManager> ();
	}

	void Update()
	{
		if (Time.time < tttime + speed)
			return;
		tttime = Time.time;
		orientation = Direction.EAST;
		tilePos = world.GetTile ((tile.x >= World.width - 1 ? -1 : tile.x) + 1, tile.y);
		if (tilePos.IsWalkable())
			transform.position = new Vector3 (tilePos.transform.position.x + 1, tilePos.transform.position.y, 0);
		OnMove ();
	}
}
