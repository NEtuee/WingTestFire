using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

	public Image image;

	InfoUpdate info;

	void Start()
	{
		info = GameObject.FindGameObjectWithTag("ChracterInfo").GetComponent<InfoUpdate>();

		image.sprite = info.info.inGameImg;

		Destroy(info.gameObject);
	}
}
