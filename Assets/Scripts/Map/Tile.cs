using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public bool noWalkable;

	public int x { get; private set; }
	public int y { get; private set; }
	public TileEntity entity {get; set;}

	bool isOnScreen ;

	// Use this for initialization
	public void Init () {
		entity = null;
	}

	public void SetPos(int px, int py)
	{
		x = px;
		y = py;
	}

	// Called when the player move
	public virtual void OnPlayerMove (int playerPosX, int playerPosY)
	{
		isOnScreen = false;
		if (x >= playerPosX - 21 && x <= playerPosX + 20 && y >= playerPosY - 13 && y <= playerPosY + 12)
			isOnScreen = true;

		if (y >= playerPosY - 13 && y <= playerPosY + 12)
		{
			if (playerPosX - 21 < 0 && x >= playerPosX - 21 + World.width)
				isOnScreen = true;
			if (playerPosX + 20 >= World.width && x <= playerPosX + 20 - World.width)
				isOnScreen = true;
		}
		if (x >= playerPosX - 21 && x <= playerPosX + 20 || playerPosX - 21 < 0 && x >= playerPosX - 21 + World.width || playerPosX + 20 >= World.width && x <= playerPosX + 20 - World.width)
		{
			if (playerPosY - 13 < 0 && y >= playerPosY - 13 + World.height)
				isOnScreen = true;
			if (playerPosY + 12 >= World.height && y <= playerPosY + 12 - World.height)
				isOnScreen = true;
		}
		if (GetComponent<MeshRenderer> () != null)
		{
			if (!isOnScreen)
			{
				GetComponent<MeshRenderer> ().enabled = false;
			}
			else
			{
				GetComponent<MeshRenderer> ().enabled = true;
			}
		}
		if (playerPosX > 28)
		{
			Quest q = QuestManager.Instance.GetQuestFromNpc (1);
			if (q != null)
				q.FailQuest ();
		}
	}


	//Call when the tile move
	public void OnMove()
	{
		if (entity) {
			Vector3 keepPos;
			keepPos.x = entity.gameObject.transform.position.x - (int)entity.gameObject.transform.position.x;
			keepPos.y = entity.gameObject.transform.position.y - (int)entity.gameObject.transform.position.y;
			Vector3 newPos = transform.position;
			newPos.x = newPos.x - 0.5f + keepPos.x;
			newPos.y = newPos.y - 0.5f + keepPos.y;
			newPos.z = entity.gameObject.transform.position.z;
			entity.gameObject.transform.position = newPos;
		}
	}

	public bool IsWalkable()
	{
		if (GetComponent<MeshRenderer> ().material.name.Split(' ')[0] == "HerbeSpecial" && QuestManager.Instance.indexQuest >= 14)
			return false;
		return ((entity == null || entity.walkable) && noWalkable == false);
	}
}
