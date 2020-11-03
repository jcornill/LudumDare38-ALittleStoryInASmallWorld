using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : TileEntity
{
	public GameObject cannonBall;
	int power = 0;

	// Use this for initialization
	void Start () {
		Init ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Shoot()
	{
		GameObject.Instantiate (cannonBall, tile.transform.position, Quaternion.identity);
	}

	public override void Action (Player player)
	{
		if (player.powder == 0)
			return;
		power = player.powder;
		if (power == 100)
			power = 99;
		player.powder = 0;
		GameObject.Find ("Canvas").GetComponent<UIManager> ().UpdateGunPowder (player.powder);
		Shoot ();
	}
	public override void Push (Direction dir)
	{
		Tile vTile;
		if (dir == Direction.NORTH)
		{
			vTile = world.GetTile (tile.x, tile.y + 1);
			if (vTile is Rail)
			{
				transform.position = new Vector3 (transform.position.x, transform.position.y + 1, 0);
				OnMove ();
			}
		}
		else if (dir == Direction.SOUTH)
		{
			vTile = world.GetTile (tile.x, tile.y - 1);
			if (vTile is Rail)
			{
				transform.position = new Vector3 (transform.position.x, transform.position.y - 1, 0);
				OnMove ();
			}
		}
	}
	public int GetPower()
	{
		return power;
	}
}
