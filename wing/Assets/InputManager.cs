using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public PlayerAttack attack;
	public GameObject player;

	Touch touchs;

	bool isTouched = false;
	Vector2 playerOrigin;
	Vector2 touchOrigin;

	void Update () {
		if(Input.touchCount > 0)
		{
			for(int i = 0; i < Input.touchCount; ++i)
			{
				touchs = Input.GetTouch(i);
				if(touchs.position.x > Screen.width / 3)
				{
					if(touchs.phase == TouchPhase.Began)
					{
						Vector3 pos = Camera.main.ScreenToWorldPoint(touchs.position);
						pos.z = 0;
						attack.transform.position = pos;
					}
					else if(touchs.phase == TouchPhase.Moved)
					{
						Vector3 pos = Camera.main.ScreenToWorldPoint(touchs.position);
						pos.z = 0;
						attack.transform.position = pos;
					}
				}
				else
				{
					if(touchs.phase == TouchPhase.Began)
					{
						playerOrigin = player.transform.position;
						touchOrigin = Camera.main.ScreenToWorldPoint(touchs.position);
					}
					else if(touchs.phase == TouchPhase.Moved)
					{
						Vector2 pos = new Vector2(playerOrigin.x,playerOrigin.y - (touchOrigin.y - Camera.main.ScreenToWorldPoint(touchs.position).y));
						player.transform.position = pos;
					}
				}
			}
		}
	}
}
