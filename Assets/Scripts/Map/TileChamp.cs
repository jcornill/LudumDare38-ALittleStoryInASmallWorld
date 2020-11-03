using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChamp : Tile {

	public Fruit fruit;

	public float spawnChance = 10000;

	void Update()
	{
		if (fruit == null)
			Debug.LogError ("A tile champ is set without a fruit");
		if (entity == null)
		{
			if (Random.Range (0, spawnChance) < 1)
			{
				GameObject go = GameObject.Instantiate (fruit.gameObject);
				go.transform.position = transform.position;
			}
		}
	}
}
