using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsTile : Tile {

	bool roof = false;

	public override void OnPlayerMove (int playerPosX, int playerPosY)
	{
		base.OnPlayerMove (playerPosX, playerPosY);
		if (playerPosX == x && playerPosY == y && entity != null)
		{
			if (transform.eulerAngles.z == 0)
			{
				if (entity.orientation == Direction.EAST)
					roof = true;
				else if (entity.orientation == Direction.WEST)
					roof = false;
			}
			else if (transform.eulerAngles.z == 90)
			{
				if (entity.orientation == Direction.NORTH)
					roof = true;
				else if (entity.orientation == Direction.SOUTH)
					roof = false;
			}
			else if (transform.eulerAngles.z == 180)
			{
				if (entity.orientation == Direction.WEST)
					roof = true;
				else if (entity.orientation == Direction.EAST)
					roof = false;
			}
			else if (transform.eulerAngles.z == 270)
			{
				if (entity.orientation == Direction.SOUTH)
					roof = true;
				else if (entity.orientation == Direction.NORTH)
					roof = false;
			}
			for (int i = 0; i < transform.childCount; i++)
			{
				if (transform.GetChild (i).name == "Quad")
					continue;
				transform.GetChild (i).GetComponent<MeshRenderer> ().enabled = roof;
			}
		}
	}
}
