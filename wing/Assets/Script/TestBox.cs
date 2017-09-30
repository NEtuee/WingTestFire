using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBox : MonoBehaviour {

	Collider2D save;

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col == save)
			return;
		if(col.GetComponent<PlayerAttack>() != null)
		{
			if(col.GetComponent<PlayerAttack>().isMoving)
			{
				Color c = GetComponent<SpriteRenderer>().color == Color.black ? Color.white : Color.black;
				GetComponent<SpriteRenderer>().color = c;
			}
		}
		save = col;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if(col == save)
			save = null;
	}
}
