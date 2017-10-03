using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

	public PlayerAttack attack;
	public GameObject player;

	public bool atkTouching = false;

	public static InputManager instance;

	Touch touchs;
	Touch atkTouch;
	Touch movementTouch;

	bool isTouched = false;
	Vector2 playerOrigin;
	Vector2 touchOrigin;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		atkTouch.fingerId = -1;
		movementTouch.fingerId = -1;
	}

	public void Progress () {
		if(Input.touchCount > 0 && !GameManager.instance.player.isDead)
		{
			for(int i = 0; i < Input.touchCount; ++i)
			{
				touchs = Input.GetTouch(i);

				switch(touchs.phase)
				{
				case TouchPhase.Began:
					if(touchs.position.x > Screen.width / 3)
					{
						atkTouch = Input.GetTouch(i);
						atkTouching = true;
						Vector3 pos = Camera.main.ScreenToWorldPoint(touchs.position);
				 		pos.z = 0;
				 		attack.transform.position = pos;
					}
					else
					{
						movementTouch = Input.GetTouch(i);
						playerOrigin = player.transform.position;
				 		touchOrigin = Camera.main.ScreenToWorldPoint(touchs.position);
					}
					break;
				case TouchPhase.Moved:
					if(touchs.fingerId == atkTouch.fingerId)
					{
						Vector3 pos = Camera.main.ScreenToWorldPoint(touchs.position);
				 		pos.z = 0;
				 		attack.transform.position = pos;
					}
					else if(touchs.fingerId == movementTouch.fingerId)
					{
						Vector2 pos = new Vector2(playerOrigin.x,playerOrigin.y - (touchOrigin.y - Camera.main.ScreenToWorldPoint(touchs.position).y));
				 		player.transform.position = pos;
					}
					break;
				case TouchPhase.Ended:
					if(touchs.fingerId == atkTouch.fingerId)
					{
						atkTouch.fingerId = -1;
						atkTouching = false;
					}
					else if(touchs.fingerId == movementTouch.fingerId)
					{
						movementTouch.fingerId = -1;
					}
					break;
				}
			}
		}
	}
}
