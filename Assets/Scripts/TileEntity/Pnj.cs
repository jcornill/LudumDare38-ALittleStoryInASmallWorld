using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pnj : TileEntity {

	public int npcID;
	UIManager ui;
	public string moveText = "";

	float sspeed = 0.5f;
	public float speed = 0.5f;
	float actualSpeed;
	float time;

	Tile tilePos = null;

	public float attackSpeed = 1f;
	float actualAttackSpeed;
	float attackTime;
	public bool hostile;

	// Use this for initialization
	void Start () {
		ui = GameObject.Find ("Canvas").GetComponent<UIManager> ();
		Init ();
		time = Time.time;
	}

	void tryAttack()
	{
		if (Time.time < attackTime + actualAttackSpeed || tilePos.entity != world.player)
			return;
		attackTime = Time.time;
		actualAttackSpeed = attackSpeed;
		Attack (world.player);
	}

	// Update is called oncespeedframe
	void Update () {
		bool move = true;
		Vector3 pPos = world.player.transform.position;
		if (hostile)
		{
			if (Time.time < time + actualSpeed)
				return;
			time = Time.time;
			actualSpeed = speed;
			if (pPos.y > transform.position.y)
			{
				orientation = Direction.NORTH;
				tilePos = world.GetTile (tile.x, (tile.y >= World.height - 1 ? -1 : tile.y) + 1);
				if (tilePos.IsWalkable())
					transform.position = new Vector3 (transform.position.x, transform.position.y + 1, 0);
			}
			else if (pPos.x < transform.position.x)
			{
				orientation = Direction.WEST;
				tilePos = world.GetTile ((tile.x <= 0 ? World.width : tile.x) - 1, tile.y);
				if (tilePos.IsWalkable())
					transform.position = new Vector3 (transform.position.x - 1, transform.position.y, 0);
			}
			else if (pPos.y < transform.position.y)
			{
				orientation = Direction.SOUTH;
				tilePos = world.GetTile (tile.x, (tile.y <= 0 ? World.height : tile.y) - 1);
				if (tilePos.IsWalkable())
					transform.position = new Vector3 (transform.position.x, transform.position.y - 1, 0);
			}
			else if (pPos.x > transform.position.x)
			{
				orientation = Direction.EAST;
				tilePos = world.GetTile ((tile.x >= World.width - 1 ? -1 : tile.x) + 1, tile.y);
				if (tilePos.IsWalkable())
					transform.position = new Vector3 (transform.position.x + 1, transform.position.y, 0);
			}
			else
				move = false;
			if (move)
			{
				tryAttack ();
				OnMove ();
				transform.eulerAngles = new Vector3 (0, 0, 180 - ((int)orientation * 90));
				switch (tile.GetComponent<MeshRenderer> ().material.name.Split (' ') [0])
				{
				case "Sand":
					actualSpeed += 0.4f;
					break;
				case "Herbe":
					break;
				case "Champs":
					actualSpeed += 0.75f;
					break;
				case "Route":
					actualSpeed -= 0.050f;
					break;
				case "Water":
					actualSpeed += 1;
					break;
				case "Rail":
					actualSpeed += 1;
					break;
				}
			}
			return;
		}
		if (Time.time < time + sspeed)
			return;
		time = Time.time;
		if (moveText.Length > 0)
		{
			switch (moveText [0])
			{
			case 'z':
				orientation = Direction.NORTH;
				tilePos = world.GetTile (tile.x, (tile.y >= World.height - 1 ? -1 : tile.y) + 1);
				if (tilePos.IsWalkable ())
				{
					transform.position = new Vector3 (transform.position.x, transform.position.y + 1, 0);
					moveText = moveText.Remove (0, 1);
				}
				break;
			case 'd':
				orientation = Direction.EAST;
				tilePos = world.GetTile ((tile.x >= World.width - 1 ? -1 : tile.x) + 1, tile.y);
				if (tilePos.IsWalkable ())
				{
					transform.position = new Vector3 (transform.position.x + 1, transform.position.y, 0);
					moveText = moveText.Remove (0, 1);
				}
				break;
			case 's':
				orientation = Direction.SOUTH;
				tilePos = world.GetTile (tile.x, (tile.y <= 0 ? World.height : tile.y) - 1);
				if (tilePos.IsWalkable ())
				{
					transform.position = new Vector3 (transform.position.x, transform.position.y - 1, 0);
					moveText = moveText.Remove (0, 1);
				}
				break;
			case 'q':
				orientation = Direction.WEST;
				tilePos = world.GetTile ((tile.x <= 0 ? World.width : tile.x) - 1, tile.y);
				if (tilePos.IsWalkable ())
				{
					transform.position = new Vector3 (transform.position.x - 1, transform.position.y, 0);
					moveText = moveText.Remove (0, 1);
				}
				break;
			default:
				moveText = moveText.Remove (0, 1);
				break;
			}
			OnMove ();
			transform.eulerAngles = new Vector3 (0, 0, 180 - ((int)orientation * 90));
		}
		else if (moveText.Length == 0 && npcID == 2 && QuestManager.Instance.indexQuest == 3)
		{
			orientation = Direction.EAST;
			QuestManager.Instance.indexQuest = 4;
		}
		else if (moveText.Length == 0 && (npcID == 3 || npcID == 4) && QuestManager.Instance.indexQuest == 7)
		{
			QuestManager.Instance.indexQuest = 8;
		}
		else if (moveText.Length == 0 && (npcID == 3 || npcID == 4) && QuestManager.Instance.indexQuest == 10)
		{
			QuestManager.Instance.indexQuest = 11;
			TileEntity te = GameObject.Find ("Pnj (Dadghost)").GetComponent<TileEntity> ();
			te.tile = World.instance.GetTile (82, 86);
			te.transform.position = te.tile.transform.position;
			for (int i = 0; i < te.transform.childCount; i++)
			{
				te.transform.GetChild (i).GetComponent<MeshRenderer> ().enabled = true;
			}
			te.tile.entity = te;
			te.enabled = true;
			te.GetComponent<MeshRenderer> ().enabled = true;
		}
	}

	public override void Action (Player player)
	{
		string str = null;
		Quest q = null;
		if (npcID == 0)
		{
			if (QuestManager.Instance.indexQuest == 0 && !QuestManager.Instance.ContainsQuestId (0))
			{
				new Quest (npcID, 0);
			}
			if (QuestManager.Instance.indexQuest == 1 && !QuestManager.Instance.ContainsQuestId (3))
			{
				new Quest (npcID, 3);
			}
			if (QuestManager.Instance.indexQuest == 5 && !QuestManager.Instance.ContainsQuestId (6))
			{
				new Quest (npcID, 6);
			}
			if (QuestManager.Instance.indexQuest == 11 && !QuestManager.Instance.ContainsQuestId (10))
			{
				new Quest (npcID, 10);
			}
			if (QuestManager.Instance.indexQuest == 14 && !QuestManager.Instance.ContainsQuestId (11))
			{
				new Quest (npcID, 11);
			}
		}
		else if (npcID == 1)
		{
			if (!QuestManager.Instance.ContainsQuestId (1))
			{
				new Quest (npcID, 1);
				world.SpawnRats ();
			}
			else if (!QuestManager.Instance.ContainsQuestId (2))
			{
				new Quest (npcID, 2);
				world.SpawnRats ();
			}
		}
		else if (npcID == 2)
		{
			if (QuestManager.Instance.indexQuest == 2 && !QuestManager.Instance.ContainsQuestId (4))
			{
				new Quest (npcID, 4);
			}
			if (QuestManager.Instance.indexQuest == 4 && !QuestManager.Instance.ContainsQuestId (5))
			{
				new Quest (npcID, 5);
			}
		}
		else if (npcID == 3 || npcID == 4)
		{
			print (QuestManager.Instance.indexQuest);
			if (QuestManager.Instance.indexQuest == 6 && !QuestManager.Instance.ContainsQuestId (7))
			{
				new Quest (npcID, 7);
			}
		}
		else if (npcID == 5)
		{
			print (QuestManager.Instance.indexQuest);
			if (QuestManager.Instance.indexQuest == 8 && !QuestManager.Instance.ContainsQuestId (8))
			{
				new Quest (npcID, 8);
			}
		}
			
		q = QuestManager.Instance.GetQuestFromNpc (npcID);
		if (q != null)
			str = q.GetDialog ();

		if (str == null)
		{
			ui.strText = "";
			ui.isText = false;
			ui.blockMoving = false;
			ui.UpdateText ();
		}
		else
		{
			ui.strText = str;
			ui.isText = true;
			ui.blockMoving = !q.inProgress;
			ui.UpdateText ();
		}
		player.blockMoving = ui.blockMoving;
	}
	public override void Death ()
	{
		if (npcID == 0)
		{
			QuestManager.Instance.playerKarma -= 10;
			base.Death ();
		}
		if (hostile)
		{
			QuestManager.Instance.playerKarma -= 10;
			base.Death ();
		}
		Quest q = QuestManager.Instance.GetQuestFromId (5);
		if (q != null)
		{
			q.FailQuest ();
		}
	}

	public void ReallyDeath()
	{
		base.Death ();
	}
}