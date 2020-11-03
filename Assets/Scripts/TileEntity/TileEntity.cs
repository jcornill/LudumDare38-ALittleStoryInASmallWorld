using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEntity : MonoBehaviour {

	public Tile tile {get; set;}

	public Direction orientation;

	protected World world;

	public bool walkable;

	public int hitPoint;
	public int damage;

	public float hpv2;

	// Use this for initialization
	void Start () {
		Init ();
	}

	protected void Init()
	{
		walkable = false;
		orientation = Direction.SOUTH;
		world = GameObject.Find ("World").GetComponent<World> ();
		world.Init ();
		tile = world.GetTile ((int)transform.position.x % World.width, (int)transform.position.y % World.height);
		tile.entity = this;
		hpv2 = (float)hitPoint;
	}

	// Update is called once per frame
	void Update () {
		
	}

	//Need to call this function when the entity move
	protected void OnMove()
	{
		tile.entity = null;
		tile = world.GetTile ((int)transform.position.x % World.width, (int)transform.position.y % World.height);
		tile.entity = this;
	}

	// Call when the player try to move on the same tile of the entity
	public virtual void Action(Player player)
	{
		print ("Action done on " + this);
	}

	public virtual void Push(Direction dir)
	{

	}

	public virtual void Attack(TileEntity TE)
	{
		if (TE is Mobs)
		{
			Mobs m = (Mobs)TE;
			if (m.hostile)
			{
				TE.hpv2 -= damage;
				if (TE.hpv2 <= 0)
					TE.Death ();
			}
		}
		else if (TE is Pnj)
		{
			Pnj m = (Pnj)TE;
			if (m.hostile)
			{
				TE.hpv2 -= damage;
				if (TE.hpv2 <= 0)
					TE.Death ();
			}
		}
		else if (TE is PanneauPnj)
		{
			PanneauPnj m = (PanneauPnj)TE;
			if (m.hostile)
			{
				TE.hpv2 -= damage;
				if (TE.hpv2 <= 0)
					TE.Death ();
			}
		}
		else if (TE is Player)
		{
			TE.hpv2 -= damage;
			if (TE.hpv2 <= 0)
				TE.Death ();
		}
	}

	public virtual void Death()
	{
		Destroy (gameObject);
	}

	public void Heal(float amount)
	{
		hpv2 += amount;
		if (hpv2 > 100)
			hpv2 = 100;
		if (hpv2 <= 0)
			Death ();
		GameObject.Find ("Canvas").GetComponent<UIManager> ().UpdateLifeText ((int)hpv2);
		GameObject.Find ("Canvas").GetComponent<UIManager> ().SetAlpha (100.0f - hpv2);
	}
}
