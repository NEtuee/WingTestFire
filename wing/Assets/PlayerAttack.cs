using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

	public bool isMoving = false;

	public Text text;

	Vector2 formerPos;
	Vector2 prevPos;
	Vector2 currentPos;

	RaycastHit2D ray;
	EdgeCollider2D col;

	void Start()
	{
		col = GetComponent<EdgeCollider2D>();
		prevPos = transform.position;
		formerPos = transform.position;
	}

	void Update()
	{
		Vector2 one = transform.position;

		currentPos = one;

		if(prevPos != currentPos)
		{
			// Vector2 dir = currentPos - prevPos;
			// float dist = Vector2.Distance(prevPos,currentPos);
			// ray = Physics2D.Raycast(prevPos,dir.normalized,dist);
			// if(ray.collider != null)
			// {
			// 	Debug.Log(ray.collider.gameObject.name);
			// 	if(ray.collider.GetComponent<SpriteRenderer>().color == Color.black)
			// 		ray.collider.GetComponent<SpriteRenderer>().color = Color.white;
			// 	else
			// 		ray.collider.GetComponent<SpriteRenderer>().color = Color.black;
			// }
			isMoving = true;
			Vector2 [] points = {(formerPos - one),(prevPos - one),new Vector2(0,0)};

			col.points = points;
		}
		else if(isMoving)
		{
			// Vector2 [] v = {prevPos,new Vector2()};
			// col.points = v;
			isMoving = false;
		}
		formerPos = prevPos;
		prevPos = currentPos;
	}
}
