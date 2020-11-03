using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanneauPnj : TileEntity {

	public List<string> dialogs;
	int dialogId = -1;

	public int healthModifier;
	public int foodModifier;
	public int karmaModifier;

	protected UIManager ui;

	public float speed = 0.5f;
	float actualSpeed;
	float time;

	public float attackSpeed = 1f;
	float actualAttackSpeed;
	float attackTime;
	public bool hostile;

	protected Tile tilePos = null;

	// Use this for initialization
	void Start () {
		Init ();
		ui = GameObject.Find ("Canvas").GetComponent<UIManager> ();
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
	}

	public override void Action (Player player)
	{
		dialogId++;
		if (dialogId < dialogs.Count)
		{
			ui.strText = dialogs [dialogId];
			ui.blockMoving = true;
			ui.isText = true;
			ui.UpdateText ();
		}
		else
		{
			dialogId = -1;
			ui.strText = "";
			ui.blockMoving = false;
			ui.isText = false;
			ui.UpdateText ();
			QuestManager.Instance.playerFood += foodModifier;
			QuestManager.Instance.playerKarma += karmaModifier;
			player.Heal (healthModifier);
		}
		player.blockMoving = ui.blockMoving;
	}

	public override void Death ()
	{
		QuestManager.Instance.playerKarma -= 10;
		base.Death ();
	}
}
