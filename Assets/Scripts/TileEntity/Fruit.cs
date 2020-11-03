using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : TileEntity
{
	public int karmaModifier;
	public int food;
	public int healModifier;
	public string fruitName;

	void Start()
	{
		Init ();
	}

	public override void Action (Player player)
	{
		QuestManager.Instance.playerKarma += karmaModifier;
		QuestManager.Instance.IncreaseFood (food);
		player.Heal (healModifier);
		Death ();
	}

	public override void Death ()
	{
		if (fruitName == "Choux" && QuestManager.Instance.indexQuest == 0)
		{
			QuestManager.Instance.indexQuest = 1;
			TileEntity te = GameObject.Find ("Pnj (Dadghost)").GetComponent<TileEntity> ();
			te.tile = World.instance.GetTile (45, 52);
			te.transform.position = te.tile.transform.position;
			for (int i = 0; i < te.transform.childCount; i++)
			{
				te.transform.GetChild (i).GetComponent<MeshRenderer> ().enabled = true;
			}
			te.tile.entity = te;
			te.enabled = true;
			te.GetComponent<MeshRenderer> ().enabled = true;
			GameObject.Find ("Boussole").GetComponent<Boussole> ().TargetNext ();
		}
		base.Death ();
	}

}
