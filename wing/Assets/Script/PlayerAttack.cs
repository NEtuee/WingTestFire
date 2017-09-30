using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

	public bool isMoving = false;
	public bool check = false;
	public int attackCount = 1;

	public float atkAngle = 180;

	public Text text;

	Vector2 formerPos;
	Vector2 prevPos;
	Vector2 currentPos;

	RaycastHit2D ray;
	EdgeCollider2D col;
	TrailRenderer trail;

	AttackScript atk;

	public float attackAngle = 0f;
	public float revAngle = 0f;

	public Vector2 angle;
	bool angleCheck = false;

	void Start()
	{
		col = GetComponent<EdgeCollider2D>();
		atk = GetComponent<AttackScript>();
		trail = GetComponent<TrailRenderer>();
		prevPos = transform.position;
		formerPos = transform.position;
	}

	bool AngleCheck(Vector2 a1, float a2)
	{
		bool re;
		if(a1.x < a1.y)
			re = a1.x < a2 ? (a1.y < a2 ? true : false) : true;
		else
			re = a1.x > a2 ? (a1.y < a2 ? true : false) : false;

		return re;
	}

	void Update()
	{
		Vector2 one = transform.position;

		currentPos = one;

		if(prevPos != currentPos)
		{
			Vector2 dir = prevPos - currentPos;
			attackAngle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
			revAngle = attackAngle - 180;

			if(attackAngle < 0)
				attackAngle = 360 + attackAngle;
			if(revAngle < 0)
				revAngle = 360 + revAngle;

			check = AngleCheck(angle,revAngle);
			//==================
			isMoving = true;
			atk.safeMode = false;

			if(angleCheck && !check)
				atk.safeMode = true;

			Vector2 [] points = {(formerPos - one),(prevPos - one),new Vector2(0,0)};

			col.points = points;
			//====================
		}
		else if(isMoving && currentPos == prevPos)
		{
			// Vector2 [] v = {prevPos,new Vector2()};
			// col.points = v;
			atk.safeMode = true;
			isMoving = false;
			//angleCheck = false;
		}

		if(!InputManager.instance.atkTouching)
			angleCheck = false;

		if(atk.attackCount == 0)
		{
			Debug.Log("Check");
			atk.attackCount = attackCount;
			angle = new Vector2(attackAngle + (atkAngle / 2) > 360 ? (attackAngle + (atkAngle / 2)) - 360 : attackAngle + (atkAngle / 2)
			 , attackAngle - (atkAngle / 2) < 0 ? (attackAngle - (atkAngle / 2)) + 360 : attackAngle - (atkAngle / 2));
				angleCheck = true;
		}

		if(atk.safeMode)
				trail.startColor = Color.white;
		else
			trail.startColor = Color.red;

		formerPos = prevPos;
		prevPos = currentPos;
	}
}
