using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharacterInfo {

	public int num;
	public string name;
	public string _class;
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
		num = info.num;
		name = info.name;
		_class = info._class;
		inGameImg = info.inGameImg;
		illust = info.illust;
		maxHp = info.maxHp;
		explanation = info.explanation;

		image = info.image;
		isLock = info.isLock;
	}
}
