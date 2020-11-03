using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : TileEntity
{

	public float speed;
	float actualSpeed;
	float time;

	public float foodSpeed;
	float foodTime;

	Tile tilePos = null;
	public bool blockMoving;
	public bool hasSword;

	public int powder { get; set; }
	public string moveText = "";

	public Vector2 targetCoord = new Vector2 (0, 0);
	public Vector2 targetCoord2 = new Vector2 (0, 0);

	public int compteurBidon = 0;

	// Use this for initialization
	void Start ()
	{
		hasSword = false;
		blockMoving = false;
		time = Time.time;
		foodTime = Time.time;
		Init ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (hasSword)
		{
			hasSword = false;
			damage = 20;
		}
		if (targetCoord2.x != 0 || targetCoord2.y != 0)
		{
			if (targetCoord2.y > tile.y)
			{
				orientation = Direction.NORTH;
				transform.position = new Vector3 (transform.position.x, transform.position.y + 1, 0);
			}
			else if (targetCoord2.x < tile.x)
			{
				orientation = Direction.WEST;
				transform.position = new Vector3 (transform.position.x - 1, transform.position.y, 0);
			}
			else if (targetCoord2.y < tile.y)
			{
				orientation = Direction.SOUTH;
				transform.position = new Vector3 (transform.position.x, transform.position.y - 1, 0);
			}
			else if (targetCoord2.x > tile.x)
			{
				orientation = Direction.EAST;
				transform.position = new Vector3 (transform.position.x + 1, transform.position.y, 0);
			}
			OnMove ();
			transform.eulerAngles = new Vector3 (0, 0, 180 - ((int)orientation * 90));
			if (targetCoord2.x == tile.x && targetCoord2.y == tile.y)
			{
				TileEntity te = GameObject.Find ("Pnj (Dadghost)").GetComponent<TileEntity> ();
				te.tile.entity = null;
				te.tile = World.instance.GetTile (20, 91);
				te.transform.position = te.tile.transform.position;
				te.tile.entity = te;
				transform.eulerAngles = new Vector3 (0, 0, 180 - ((int)Direction.SOUTH * 90));
				GameObject.Find ("Canvas").GetComponent<UIManager> ().lockBS = false;
				if (compteurBidon > 0)
				{
					GameObject.Find ("Canvas").GetComponent<UIManager> ().SetAlpha (compteurBidon);
					compteurBidon--;
					return;
				}
				targetCoord2 = new Vector2 (0, 0);
				QuestManager.Instance.indexQuest = 14;
			}
			return;
		}

		if (targetCoord.x != 0 || targetCoord.y != 0)
		{
			if (compteurBidon < 100)
			{
				GameObject.Find ("Canvas").GetComponent<UIManager> ().SetAlpha (compteurBidon);
				compteurBidon++;
				return;
			}
			GameObject.Find ("Canvas").GetComponent<UIManager> ().lockBS = true;
			if (targetCoord.y > tile.y)
			{
				orientation = Direction.NORTH;
				transform.position = new Vector3 (transform.position.x, transform.position.y + 1, 0);
			}
			else if (targetCoord.x < tile.x)
			{
				orientation = Direction.WEST;
				transform.position = new Vector3 (transform.position.x - 1, transform.position.y, 0);
			}
			else if (targetCoord.y < tile.y)
			{
				orientation = Direction.SOUTH;
				transform.position = new Vector3 (transform.position.x, transform.position.y - 1, 0);
			}
			else if (targetCoord.x > tile.x)
			{
				orientation = Direction.EAST;
				transform.position = new Vector3 (transform.position.x + 1, transform.position.y, 0);
			}
			world.MoveWorld (orientation);
			OnMove ();
			for (int i = 0; i < World.width; i++) {
				for (int j = 0; j < World.height; j++) {
					world.GetTile(i, j).OnPlayerMove(tile.x, tile.y);
				}
			}
			transform.eulerAngles = new Vector3 (0, 0, 180 - ((int)orientation * 90));
			if (targetCoord.x == tile.x && targetCoord.y == tile.y)
			{
				targetCoord = new Vector2 (0, 0);
				QuestManager.Instance.indexQuest = 13;
				targetCoord2 = new Vector2 (20, 93);
			}
			return;
		}

		if (moveText.Length > 0)
		{
			if (Time.time < time + 0.5f)
				return;
			time = Time.time;
			switch (moveText [0])
			{
			case 'z':
				orientation = Direction.NORTH;
				tilePos = world.GetTile (tile.x, (tile.y >= World.height - 1 ? -1 : tile.y) + 1);
				if (tilePos.IsWalkable ())
				{
					transform.position = new Vector3 (transform.position.x, transform.position.y + 1, 0);
					moveText = moveText.Remove (0, 1);
					world.MoveWorld (Direction.NORTH);
				}
				break;
			case 'd':
				orientation = Direction.EAST;
				tilePos = world.GetTile ((tile.x >= World.width - 1 ? -1 : tile.x) + 1, tile.y);
				if (tilePos.IsWalkable ())
				{
					transform.position = new Vector3 (transform.position.x + 1, transform.position.y, 0);
					moveText = moveText.Remove (0, 1);
					world.MoveWorld (Direction.EAST);
				}
				break;
			case 's':
				orientation = Direction.SOUTH;
				tilePos = world.GetTile (tile.x, (tile.y <= 0 ? World.height : tile.y) - 1);
				if (tilePos.IsWalkable ())
				{
					transform.position = new Vector3 (transform.position.x, transform.position.y - 1, 0);
					moveText = moveText.Remove (0, 1);
					world.MoveWorld (Direction.SOUTH);
				}
				break;
			case 'q':
				orientation = Direction.WEST;
				tilePos = world.GetTile ((tile.x <= 0 ? World.width : tile.x) - 1, tile.y);
				if (tilePos.IsWalkable ())
				{
					transform.position = new Vector3 (transform.position.x - 1, transform.position.y, 0);
					moveText = moveText.Remove (0, 1);
					world.MoveWorld (Direction.WEST);
				}
				break;
			default:
				moveText = moveText.Remove (0, 1);
				break;
			}
			OnMove ();
			for (int i = 0; i < World.width; i++) {
				for (int j = 0; j < World.height; j++) {
					world.GetTile(i, j).OnPlayerMove(tile.x, tile.y);
				}
			}
			transform.eulerAngles = new Vector3 (0, 0, 180 - ((int)orientation * 90));
			return;
		}


		if (Input.GetKeyDown (KeyCode.E) && tilePos && tilePos.entity != null)
		{
			if (orientation == Direction.NORTH)
				tilePos = world.GetTile (tile.x, (tile.y >= World.height - 1 ? -1 : tile.y) + 1);
			else if (orientation == Direction.WEST)
				tilePos = world.GetTile ((tile.x <= 0 ? World.width : tile.x) - 1, tile.y);
			else if (orientation == Direction.SOUTH)
				tilePos = world.GetTile (tile.x, (tile.y <= 0 ? World.height : tile.y) - 1);
			else if (orientation == Direction.EAST)
				tilePos = world.GetTile ((tile.x >= World.width - 1 ? -1 : tile.x) + 1, tile.y);
			if (tilePos && tilePos.entity != null)
				tilePos.entity.Action (this);
		}
		if (Input.GetKeyDown (KeyCode.F) && tilePos && tilePos.entity != null && tilePos.entity != this)
		{
			if (orientation == Direction.NORTH)
				tilePos = world.GetTile (tile.x, (tile.y >= World.height - 1 ? -1 : tile.y) + 1);
			else if (orientation == Direction.WEST)
				tilePos = world.GetTile ((tile.x <= 0 ? World.width : tile.x) - 1, tile.y);
			else if (orientation == Direction.SOUTH)
				tilePos = world.GetTile (tile.x, (tile.y <= 0 ? World.height : tile.y) - 1);
			else if (orientation == Direction.EAST)
				tilePos = world.GetTile ((tile.x >= World.width - 1 ? -1 : tile.x) + 1, tile.y);
			if (tilePos && tilePos.entity != null && tilePos.entity != this)
				Attack (tilePos.entity);
		}
		bool move = true;
		if (Time.time < time + actualSpeed || blockMoving)
			return;
		if (Time.time > foodTime + foodSpeed)
		{
			foodTime = Time.time;
			QuestManager.Instance.DecreaseFood (0.2f);
			float dg = QuestManager.Instance.playerFood / 100.0f;
			if (QuestManager.Instance.playerKarma < 0 && dg < 0)
				dg *= 20f;
			Heal (dg);
			GameObject.Find ("Canvas").GetComponent<UIManager> ().UpdateFoodText (dg);
		}
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.Z))
		{
			orientation = Direction.NORTH;
			tilePos = world.GetTile (tile.x, (tile.y >= World.height - 1 ? -1 : tile.y) + 1);
			if (tilePos.IsWalkable())
			{
				transform.position = new Vector3 (transform.position.x, transform.position.y + 1, 0);
				if (QuestManager.Instance.indexQuest < 14)
					world.MoveWorld (Direction.NORTH);
			}
		}
		else if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.Q))
		{
			orientation = Direction.WEST;
			tilePos = world.GetTile ((tile.x <= 0 ? World.width : tile.x) - 1, tile.y);
			if (tilePos.IsWalkable())
			{
				transform.position = new Vector3 (transform.position.x - 1, transform.position.y, 0);
				if (QuestManager.Instance.indexQuest < 14)
					world.MoveWorld (Direction.WEST);
			}
		}
		else if (Input.GetKey (KeyCode.S))
		{
			orientation = Direction.SOUTH;
			tilePos = world.GetTile (tile.x, (tile.y <= 0 ? World.height : tile.y) - 1);
			if (tilePos.IsWalkable())
			{
				transform.position = new Vector3 (transform.position.x, transform.position.y - 1, 0);
				if (QuestManager.Instance.indexQuest < 14)
					world.MoveWorld (Direction.SOUTH);
			}
		}
		else if (Input.GetKey (KeyCode.D))
		{
			orientation = Direction.EAST;
			tilePos = world.GetTile ((tile.x >= World.width - 1 ? -1 : tile.x) + 1, tile.y);
			if (tilePos.IsWalkable())
			{
				transform.position = new Vector3 (transform.position.x + 1, transform.position.y, 0);
				if (QuestManager.Instance.indexQuest < 14)
					world.MoveWorld (Direction.EAST);
			}

		}
		else
			move = false;
		if (move)
		{
			actualSpeed = speed;
			time = Time.time;
			if (QuestManager.Instance.indexQuest < 14)
			{
				for (int i = 0; i < World.width; i++)
				{
					for (int j = 0; j < World.height; j++)
					{
						world.GetTile (i, j).OnPlayerMove (tile.x, tile.y);
					}
				}
			}
			if (tilePos.entity != null)
				tilePos.entity.Push (orientation);
			transform.eulerAngles = new Vector3 (0, 0, 180 - ((int)orientation * 90));
			OnMove ();
			if (tile.GetComponent<MeshRenderer> () == null)
				return;
			switch (tile.GetComponent<MeshRenderer> ().material.name.Split(' ')[0])
			{
			case "Sand":
				actualSpeed += 0.4f;
				break;
			case "Herbe":
				actualSpeed += 0.1f;
				break;
			case "HerbeSpecial":
				actualSpeed += 0.1f;
				break;
			case "Champs":
				actualSpeed += 0.2f;
				break;
			case "Route":
				actualSpeed -= 0.050f;
				break;
			case "Water":
				actualSpeed += 0.4f;
				break;
			case "Rail":
				actualSpeed += 0.5f;
				break;
			}
		}
	}

	public override void Death ()
	{
		GameObject.Find ("Canvas").GetComponent<UIManager> ().Dead.SetActive (true);
	}
}
