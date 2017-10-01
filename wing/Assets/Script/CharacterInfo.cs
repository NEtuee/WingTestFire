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
	
	public void Copy(CharacterInfo info)
	{
		name = info.name;
		inGameImg = info.inGameImg;
		illust = info.illust;
		maxHp = info.maxHp;
		explanation = info.explanation;

		image = info.image;
		isLock = info.isLock;
	}
}
