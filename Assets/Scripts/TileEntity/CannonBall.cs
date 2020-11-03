using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : TileEntity {

	int speed = 3;
	int actualSpeed;
	int power = 0;
	Tile vTile;

	void Start()
	{
		world = GameObject.Find ("World").GetComponent<World> ();
		actualSpeed = speed;
	}

	// Update is called once per frame
	void Update () {
		if (power == 0)
		{
			power = GameObject.Find ("Cannon").GetComponent<Cannon> ().GetPower ();
		}
		if (actualSpeed < 0)
		{
			actualSpeed = speed;
			Vector3 pos = transform.position;
			pos.x = transform.position.x % World.width;
			pos.y = transform.position.y % World.height;
			vTile = world.GetTile (((int)pos.x >= World.width - 1 ? -1 : (int)pos.x) + 1, (int)pos.y);
			transform.position = new Vector3 (vTile.transform.position.x, vTile.transform.position.y, 0);
			power--;
			if (power <= 0)
				Land ();
		}
		else
		{
			actualSpeed--;
		}
	}

	void Land()
	{
		if (vTile.entity != null)
			vTile.entity.Death ();
		Destroy (gameObject);
	}
}
