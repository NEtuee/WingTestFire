using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public List<EnemyScript> enemyList;
	public GameObject enemy;

	public void AddEnemy(Vector2 pos)
	{
		EnemyScript e = Instantiate(enemy,pos,Quaternion.identity).GetComponent<EnemyScript>();
		enemyList.Add(e);
	}

	public void AddEnemy(Vector2 pos, GameObject g)
	{
		EnemyScript e = Instantiate(g,pos,Quaternion.identity).GetComponent<EnemyScript>();
		e.Progress();
		enemyList.Add(e);
	}

	public void Progress()
	{
		for(int i = 0; i < enemyList.Count;)
		{
			if(enemyList[i] == null)
				enemyList.Remove(enemyList[i]);
			else
			{
				enemyList[i].Progress();
				++i;
			}
		}
	}
}
