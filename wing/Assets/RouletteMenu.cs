using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteMenu : MonoBehaviour {

	public GameObject button;
	public GameObject canvas;
	public float count = 5;
	public float width = 100;
	public float height = 100;
	public float selectAngle = 130;
	public float friction = 100;
	public float wl = 1000;

	public List<Transform> tp;

	public float angle;

	public float move = 0;

	bool isTouched = false;

	public float accelMul = 5;
	public float accel = 0;
	Touch th;

	void Start()
	{
		angle = 360 / count;

		width = Screen.width / 3.7f;
		height = Screen.height / 3f;

		for(int i = 0; i < count; ++i)
		{
			Vector3 pos = new Vector3(Mathf.Cos((i * angle * Mathf.Deg2Rad)) * width, Mathf.Sin((i * angle * Mathf.Deg2Rad)) * height,0);
			GameObject g = Instantiate(button, transform.position + pos,Quaternion.identity);
			g.transform.SetParent(transform,false);
			tp.Add(g.transform);
		}
	}

	void Update () {
		int near = 0;
		float ang = 360;
		float nearAng = 0;

		for(int i = 0; i < count; ++i)
		{
			float a = i * angle + move;
			float origin = move - i * angle;

			Vector3 pos = new Vector3(Mathf.Cos((a * Mathf.Deg2Rad)) * width, Mathf.Sin((a * Mathf.Deg2Rad)) * height,0);

			float q = Mathf.Atan2(pos.y,pos.x) * Mathf.Rad2Deg;

			if(q < 0)
				q = q + 360;
			
			float check;

			if(selectAngle < q)
				check = q - selectAngle;
			else
				check = selectAngle - q;

			if(check < ang)
			{
				ang = check;
				near = i;
				nearAng = q;
			}

			tp[i].position = transform.position + pos;
		}

		if(move >= 360 || move <= -360)
			move = 0;

		TouchCheck();

		if(!isTouched)
		{
			if(accel != 0)
			{
				move += accel * Time.deltaTime;

				if(accel < 0)
				{
					accel += friction * Time.deltaTime;
					if(accel >= 0)
						accel = 0;
				}
				else if(accel > 0)
				{
					accel -= friction * Time.deltaTime;
					if(accel <= 0)
						accel = 0;
				}
			}
			else if(Mathf.Abs(accel) < 100)
			{
				accel = 0;

				if(ang > 0.1f)
				{
					if(nearAng < selectAngle)
						move += ang * Time.deltaTime * 5;
					else
						move -= ang * Time.deltaTime * 5;
				}
			}
		}
	}

	Vector3 oldPos;
	void TouchCheck()
	{

		if(Input.GetMouseButtonDown(0))
		{
			if(Input.mousePosition.x > Screen.width / 1.6f && Input.mousePosition.y < Screen.height / 2)
			{
				oldPos = Input.mousePosition;
				accel = 0;
				isTouched = true;
			}
		}

		if(Input.GetMouseButton(0))
		{
			if(oldPos != Input.mousePosition && isTouched)
			{
				float dist = Vector2.Distance(oldPos,Input.mousePosition);
				if(oldPos.y < Input.mousePosition.y)
				{
					move -= dist / 5;
				}
				else
					move += dist / 5;

				oldPos = Input.mousePosition;
			}
		}

		if(Input.GetMouseButtonUp(0))
		{
			if(isTouched)
			{
				if(oldPos != Input.mousePosition)
				{
					accel = Vector3.Distance(oldPos,Input.mousePosition) * accelMul;
					if(oldPos.y < Input.mousePosition.y)
						accel *= -1;
				}
				isTouched = false;
			}
		}
		// if(Input.touchCount != 0)
		// {
		// 	Touch[] touch = Input.touches;
			
		// 	for(int i =0; i < touch.Length; ++i)
		// 	{
		// 		switch(touch[i].phase)
		// 		{
		// 		case TouchPhase.Began:
		// 			if(touch[i].position.x > Screen.width / 1.5f && touch[i].position.y < Screen.height / 2)
		// 			{
		// 				th = touch[i];
		// 				oldPos = th.position;
		// 				isTouched = true;
		// 			}
		// 			break;
		// 		case TouchPhase.Moved:
		// 			if(touch[i].fingerId == th.fingerId && isTouched)
		// 			{
		// 				if(oldPos != th.position)
		// 				{
		// 					float dist = Vector2.Distance(oldPos,th.position);
		// 					if(oldPos.y < th.position.y)
		// 					{
		// 						move -= dist / 5;
		// 					}
		// 					else
		// 						move += dist / 5;
							
		// 					oldPos = th.position;
		// 				}
		// 			}
		// 			break;
		// 		case TouchPhase.Ended:
		// 			if(touch[i].fingerId == th.fingerId)
		// 				isTouched = false;
		// 			break;
		// 		}
		// 	}
		// }
	}
}
