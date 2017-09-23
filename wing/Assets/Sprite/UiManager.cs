using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

	public Image image;
	public Text text;

	void Start()
	{
		
	}

	void Update()
	{
		text.text = "Score : " + GameManager.instance.score;
		image.fillAmount = GameManager.instance.player.GetHp() / GameManager.instance.player.maxHp;
	}
}
