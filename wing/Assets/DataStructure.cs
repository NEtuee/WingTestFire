using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructure : MonoBehaviour {

	public List<CharacterInfo> charList;

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);

		GameObject s = new GameObject();
		DontDestroyOnLoad(s);
		s.AddComponent<InfoUpdate>();
		InfoUpdate charInfo = s.GetComponent<InfoUpdate>();
		charInfo.info = new CharacterInfo();
		s.tag = "ChracterInfo";

		charInfo.info.Copy(charList[PlayerPrefs.GetInt("CharInfo")]);
	}
}
