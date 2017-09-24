using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLoadManager : MonoBehaviour {


	public void Load(int lv)
	{
		LoadManager.instance.Load(lv);
	}
}
