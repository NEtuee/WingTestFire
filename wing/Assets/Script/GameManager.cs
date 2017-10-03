using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	//public GameObject enemy;
	public HpScript player;
	public bool playerDead = false;
	public bool gameLoop = false;
	public bool gameStart = false;

	public InputManager input;
	public UiManager ui;
	public EnemyManager enemy;

	public GameObject coinObj;

	public float progress = 0;

	public int enemyCount = 0;
	public int score = 0;
	public int coin = 0;

	public GameObject[] enemyArray;

	bool routine = false;
	int save = 0;

	string[] startup = {"PlayerMoveEvent","ShowTextBox","SpawnEnemy","ShowTextBox","Ready"};
	int ptr = 0;

	void Awake()
	{
		instance = this;
		coin = PlayerPrefs.GetInt("Coin");
		//DontDestroyOnLoad(gameObject);
	}

	void Update()
	{
		if(coin != save)
		{
			save = coin;
			PlayerPrefs.SetInt("Coin",coin);
		}

		if(gameStart)
		{
			if(gameLoop)
			{
				input.Progress();
				enemy.Progress();

				if(enemy.enemyList.Count == 0)
				{
					StartCoroutine("SpawnEnemy");
				}
			}
		}
		else
		{
			if(!routine)
			{
				if(ptr == 5)
				{
					gameStart = true;
					gameLoop = true;
					ui.mainUI.SetActive(true);
				}
				else
				{
					StartCoroutine(startup[ptr]);
					++ptr;
				}
			}
		}

		ui.Progress();

	}
	int line = 0;
	int end = 2;
	IEnumerator ShowTextBox()
	{
		gameLoop = false;
		routine = true;
		ui.mainUI.SetActive(false);
		ui.conversationUI.SetActive(true);
		ui.endLine = end;
		ui.currentLine = line;

		ui.Scrolling(line);
		while(true)
		{
			yield return null;
			if(ui.textEnd)
			{
				yield return new WaitForSeconds(1f);
				//line = 2;
				end = 6;
				ui.AnimationEnd();
				ui.conversationUI.SetActive(false);
				//ui.mainUI.SetActive(true);
				break;
			}
		}

		routine = false;
	}

	IEnumerator PlayerMoveEvent()
	{
		gameLoop = false;
		routine = true;
		

		Vector2 save = player.transform.position;
		while(true)
		{
			yield return null;
			Vector2 pos = player.transform.position;

			pos.x += 1.5f * Time.deltaTime;

			if(pos.x >= -6f)
			{
				pos.x = -6f;
				player.transform.position = pos;

				yield return new WaitForSeconds(1f);
				break;
			}

			player.transform.position = pos;
		}

		routine = false;
	}

	IEnumerator SpawnEnemy()
	{
		//gameLoop = false;
		routine = true;

		int i = 0;
		while(true)
		{
			enemy.AddEnemy(new Vector2(9.5f,0),enemyArray[i]);
			yield return new WaitForSeconds(0.5f);
			++i;
			if(i >= 3)
			{
				yield return new WaitForSeconds(1f);
				if(gameStart)
					gameLoop = true;
				break;
			}
		}

		routine = false;
	}

	IEnumerator Ready()
	{
		gameLoop = false;
		routine = true;

		ui.ready.gameObject.SetActive(true);

		while(true)
		{
			yield return new WaitForSeconds(2f);
			ui.ready.text = "GO";
			yield return new WaitForSeconds(1f);
			Destroy(ui.ready.gameObject);
			break;
		}

		routine = false;
	}

	// IEnumerator CoUpdate()
	// {
	// 	while(true)
	// 	{
	// 		yield return null;
	// 		if(enemyCount == 0)
	// 		{
	// 			yield return new WaitForSeconds(1f);
	// 			Spawn();
	// 		}
	// 	}
	// }

	// void Spawn()
	// {
	// 	Instantiate(enemy,new Vector3(4.3f, 0,0),Quaternion.identity);
	// }

}
