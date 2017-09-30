using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToMoveScene : MonoBehaviour {

	public int lv;

	void Update () {
		if(Input.GetMouseButtonDown(0))
			LoadManager.instance.Load(lv);
	}
}
