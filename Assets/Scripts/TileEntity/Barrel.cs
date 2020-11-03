using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : TileEntity {

	public int powder;

	public override void Action (Player player)
	{
		player.powder += powder;
		if (player.powder > 100)
			player.powder = 100;
		if (player.powder < 0)
			player.powder = 0;
		GameObject.Find ("Canvas").GetComponent<UIManager> ().UpdateGunPowder (player.powder);
	}
}
