using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTile : Tile {

	public List<string> dialogsOut;
	public List<string> dialogsIn;
	int dialogOutId = -1;
	int dialogInId = -1;
	UIManager ui;

	void Start()
	{
		ui = GameObject.Find ("Canvas").GetComponent<UIManager> ();
	}

	void Update()
	{
		if (dialogInId != -1 || dialogOutId != -1)
		{
			if (Input.GetKeyDown (KeyCode.E))
				ProcessDiag ();
		}
	}

	public override void OnPlayerMove (int playerPosX, int playerPosY)
	{
		base.OnPlayerMove (playerPosX, playerPosY);
		if (QuestManager.Instance.indexQuest != 8)
			return;
		if (playerPosX == x && playerPosY == y)
		{
			ProcessDiag ();
		}
	}

	void ProcessDiag()
	{
		if (GameObject.Find ("Player").GetComponent<Player> ().orientation == Direction.SOUTH)
		{
			dialogOutId++;
			if (dialogOutId < dialogsOut.Count)
			{
				ui.strText = dialogsOut [dialogOutId];
				ui.blockMoving = true;
				ui.isText = true;
				ui.UpdateText ();
			}
			else
			{
				dialogOutId = -1;
				ui.strText = "";
				ui.blockMoving = false;
				ui.isText = false;
				ui.UpdateText ();
			}
			GameObject.Find ("Player").GetComponent<Player> ().blockMoving = ui.blockMoving;

		}
		else if (GameObject.Find ("Player").GetComponent<Player> ().orientation == Direction.NORTH)
		{
			dialogInId++;
			if (dialogInId < dialogsIn.Count)
			{
				ui.strText = dialogsIn [dialogInId];
				ui.blockMoving = true;
				ui.isText = true;
				ui.UpdateText ();
			}
			else
			{
				dialogOutId = -1;
				ui.strText = "";
				ui.blockMoving = false;
				ui.isText = false;
				ui.UpdateText ();
			}
			GameObject.Find ("Player").GetComponent<Player> ().blockMoving = ui.blockMoving;
		}
	}
}
