using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : TileEntity {

	public override void Death ()
	{
		Quest q = QuestManager.Instance.GetQuestFromId (5);
		if (q != null)
		{
			q.AddCompletion ();
			base.Death ();
		}
	}
}
