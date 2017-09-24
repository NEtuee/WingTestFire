using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour {

	public float progress;

	public static LoadManager instance;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		instance = this;
	}

	public void Load(int lv)
	{
		StartCoroutine("LoadScene",lv);
	}

	IEnumerator LoadScene(int lv)
	{
		SceneManager.LoadScene(1);

		yield return new WaitForSeconds(0.1f);

		AsyncOperation async = SceneManager.LoadSceneAsync(lv);
		async.allowSceneActivation = false;

		while(!async.isDone)
		{
			progress = async.progress;
			if(progress == 0.9f)
			{
				progress = 1f;
				async.allowSceneActivation = true;
			}

			yield return null;
		}
	}
}
