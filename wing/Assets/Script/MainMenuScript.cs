using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

	public Image image;
	public Text text;
	public Sprite els;

	InfoUpdate info;

	void Start()
	{
		Debug.Log(Screen.width+ ", " + Screen.height);
		GameObject als = GameObject.FindGameObjectWithTag("ChracterInfo");
		if(als == null)
			image.sprite = els;
		else
		{
			info = GameObject.FindGameObjectWithTag("ChracterInfo").GetComponent<InfoUpdate>();
			image.sprite = info.info.inGameImg;
			PlayerPrefs.SetInt("CharInfo",info.info.num);
			Debug.Log(info.info.num);
		}

		text.text = "GOLD : " + PlayerPrefs.GetInt("Coin");

		//Destroy(info.gameObject);
	}
}
