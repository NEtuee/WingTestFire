using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour {

	public enum Team
	{
		Friend,
		Enemy,
	}

	public Team team = Team.Enemy;
	public float damage = 1;
	public bool deleteWhenCol = false;
	public bool safeMode = false;

}
