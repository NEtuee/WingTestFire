using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

	public bool start = false;

	public Text text;

	Vector2 prevPos;
	Vector2 currentPos;

	RaycastHit2D ray;

	void Update()
	{
		currentPos = transform.position;
		if(start)
		{
			prevPos = transform.position;
			start = false;
		}

		if(prevPos != currentPos)
		{
			Vector2 dir = currentPos - prevPos;
			float dist = Vector2.Distance(prevPos,currentPos);
			ray = Physics2D.Raycast(prevPos,dir.normalized,dist);
			if(ray.collider != null)
			{
				Debug.Log(ray.collider.gameObject.name);
				if(ray.collider.GetComponent<SpriteRenderer>().color == Color.black)
					ray.collider.GetComponent<SpriteRenderer>().color = Color.white;
				else
					ray.collider.GetComponent<SpriteRenderer>().color = Color.black;
			}
		}

		prevPos = currentPos;
	}
}
