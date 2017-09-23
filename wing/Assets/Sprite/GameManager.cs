using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public GameObject enemy;
	public HpScript player;
	public bool playerDead = false;

	public int enemyCount = 0;
	public int score = 0;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		StartCoroutine("CoUpdate");
	}

	IEnumerator CoUpdate()
	{
		while(true)
		{
			yield return null;
			if(enemyCount == 0)
			{
				yield return new WaitForSeconds(1f);
				Spawn();
			}
		}
	}

	void Spawn()
	{
		Instantiate(enemy,new Vector3(4.3f, 0,0),Quaternion.identity);
	}

	public void LoadScene()
	{
		SceneManager.LoadScene(0);
	}
}
