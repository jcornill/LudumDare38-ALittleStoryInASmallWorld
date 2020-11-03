using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobs : TileEntity {

	public float aggroRange;
	public bool hostile;
	public string mobName;

	public float speed;
	float actualSpeed;
	float time;

	public float attackSpeed;
	float actualAttackSpeed;
	float attackTime;

	Tile tilePos = null;
	public int onDeathQuestProgress;  

	// Use this for initialization
	void Start ()
	{
		time = Time.time;
		attackTime = Time.time;
		Init ();
	}

	void tryAttack()
	{
		if (Time.time < attackTime + actualAttackSpeed || tilePos.entity != world.player)
			return;
		attackTime = Time.time;
		actualAttackSpeed = attackSpeed;
		Attack (world.player);
	}

	void Update()
	{
		bool move = true;
		Vector3 pPos = world.player.transform.position;
		float distance = Vector3.Distance (pPos, transform.position);
		if (distance <= aggroRange && hostile)
		{
			if (Time.time < time + actualSpeed || world.player.blockMoving)
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
				if (tilePos.IsWalkable() && (tilePos.x != 28 || mobName != "Rat"))
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
		}
	}
	public override void Death ()
	{
		if (mobName == "Rat")
		{
			Quest q = QuestManager.Instance.GetQuestFromId (1);
			if (q != null)
				q.AddCompletion ();
			q = QuestManager.Instance.GetQuestFromId (2);
			if (q != null)
				q.AddCompletion ();
		}
		base.Death ();
	}
}
