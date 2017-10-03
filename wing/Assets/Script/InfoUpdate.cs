﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUpdate : MonoBehaviour {

	public CharacterInfo info;

	public void InfoLoad(Text n, Text e, Text c)
	{
		if(info.isLock)
		{
			n.text = "잠김";
			e.text = "아직 안만듬";
		}
		else
		{
			n.text = info.name;
			e.text = info.explanation;
			c.text = info._class;
		}
	}

}
