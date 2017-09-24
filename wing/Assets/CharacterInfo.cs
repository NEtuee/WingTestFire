using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharacterInfo {

	public string name;
	public Sprite inGameImg;
	public Sprite illust;
	public float maxHp;
	public string explanation;

	public Image image;

	public bool isLock = false;

	void Start()
	{
		image.sprite = inGameImg;
	}
	
}
