using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
	NORTH = 0,
	EAST = 1,
	SOUTH = 2,
	WEST = 3
}

public class World : MonoBehaviour {

	public GameObject tilePrefab;
	public static int width = 100;
	public static int height = 100;
	public bool recreate;
	public static World instance;
	public List<GameObject> lMobs;
	Tile[,] tileMap;

	int worldPosX;
	int worldPosY;

	int indexX;
	int indexY;
	bool isInit = false;
	public Player player;

	// Use this for initialization
	void Start () {
		Init ();
	}

	public void Init()
	{
		if (!isInit) {
			instance = this;
			indexX = 0;
			indexY = 0;
			worldPosX = (int)transform.position.x + width;
			worldPosY = (int)transform.position.y + height;
			player = GameObject.Find ("Player").GetComponent<Player> ();
			tileMap = new Tile[width, height];
			if (recreate) {
				for (int i = 0; i < 20000; i++) {
					Destroy (transform.GetChild (0).gameObject);
					if (transform.childCount == 0)
						break;
				}
				for (int i = 0; i < width; i++) {
					for (int j = 0; j < height; j++) {
						GameObject GO = GameObject.Instantiate (tilePrefab);
						GO.transform.position = new Vector3 (i + 0.5f, j + 0.5f, 0);
						GO.transform.SetParent (this.transform);
						tileMap [i, j] = GO.GetComponent<Tile> ();
						tileMap [i, j].SetPos (i, j);
						tileMap [i, j].Init ();
					}
				}
			} else {
				for (int i = 0; i < transform.Find("Tiles").childCount; i++) {
					Vector2 pos = new Vector2 (transform.Find("Tiles").GetChild (i).position.x, transform.Find("Tiles").GetChild (i).position.y);
					tileMap [(int)pos.x % width, (int)pos.y % height] = transform.Find("Tiles").GetChild (i).GetComponent<Tile> ();
					tileMap [(int)pos.x % width, (int)pos.y % height].SetPos ((int)pos.x % width, (int)pos.y % height);
					tileMap [(int)pos.x % width, (int)pos.y % height].Init ();
				}
			}
			isInit = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (QuestManager.Instance.indexQuest == 15)
			StartWar ();
		if (transform.Find ("Pnjs").childCount == 0)
			GameObject.Find ("Canvas").GetComponent<UIManager> ().EndnE.SetActive(true);
	}

	public void MoveWorld(Direction dir)
	{
		if (dir == Direction.EAST) {
			for (int i = 0; i < height; i++) {
				Vector3 newPos = tileMap [indexX, i].transform.position;
				newPos.x = worldPosX + 0.5f;
				tileMap [indexX, i].transform.position = newPos;
				tileMap [indexX, i].OnMove ();
			}
			indexX++;
			worldPosX++;
			if (indexX >= width)
				indexX = 0;
		} else if (dir == Direction.WEST) {
			indexX--;
			worldPosX--;
			if (indexX < 0)
				indexX = width - 1;
			for (int i = 0; i < height; i++) {
				Vector3 newPos = tileMap [indexX, i].transform.position;
				newPos.x = worldPosX - width + 0.5f;
				tileMap [indexX, i].transform.position = newPos;
				tileMap [indexX, i].OnMove ();
			}
		} else if (dir == Direction.NORTH) {
			for (int i = 0; i < width; i++) {
				Vector3 newPos = tileMap [i, indexY].transform.position;
				newPos.y = worldPosY + 0.5f;
				tileMap [i, indexY].transform.position = newPos;
				tileMap [i, indexY].OnMove ();
			}
			indexY++;
			worldPosY++;
			if (indexY >= height)
				indexY = 0;
		} else if (dir == Direction.SOUTH) {
			indexY--;
			worldPosY--;
			if (indexY < 0)
				indexY = height - 1;
			for (int i = 0; i < width; i++) {
				Vector3 newPos = tileMap [i, indexY].transform.position;
				newPos.y = worldPosY - height + 0.5f;
				tileMap [i, indexY].transform.position = newPos;
				tileMap [i, indexY].OnMove ();
			}
		}
	}

	public void SpawnRats()
	{
		if (!QuestManager.Instance.ContainsQuestId (1))
			return;
		int nbRat = 0;
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				if (tileMap [i, j].entity != null && tileMap [i, j].entity is Mobs)
				{
					Mobs m = (Mobs)tileMap [i, j].entity;
					if (m != null && m.mobName == "Rat")
						nbRat++;
				}
			}
		}
		if (nbRat == 3)
			return;
		Tile t = GetTile (12, 75);
		if (t.IsWalkable ())
		{
			GameObject go = GameObject.Instantiate (lMobs [0]);
			go.transform.position = t.transform.position;
			nbRat++;
			if (nbRat == 3)
				return;
		}
		t = GetTile (12, 74);
		if (t.IsWalkable ())
		{
			GameObject go = GameObject.Instantiate (lMobs [0]);
			go.transform.position = t.transform.position;
			nbRat++;
			if (nbRat == 3)
				return;
		}
		t = GetTile (12, 73);
		if (t.IsWalkable ())
		{
			GameObject go = GameObject.Instantiate (lMobs [0]);
			go.transform.position = t.transform.position;
			nbRat++;
			if (nbRat == 3)
				return;
		}
	}

	public void StartWar()
	{
		for (int i = 0; i < transform.Find("Pnjs").childCount; i++)
		{
			TileEntity te = transform.Find("Pnjs").GetChild(i).GetComponent<TileEntity> ();
			if (te is Pnj)
			{
				Pnj p = (Pnj)te;
				p.hostile = true;
			}
			else if (te is PanneauPnj)
			{
				PanneauPnj p = (PanneauPnj)te;
				p.hostile = true;
			}
			print (te);
			if (i < 30)
				te.tile = GetTile (i, 80);
			else
				te.tile = GetTile (i % 30, 81);
			te.transform.position = te.tile.transform.position;
		}
		QuestManager.Instance.indexQuest = 16;
	}

	public Tile GetTile(int x, int y)
	{
		return tileMap [x, y];
	}
}
