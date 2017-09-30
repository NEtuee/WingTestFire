using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChracterSelection : MonoBehaviour {

	public GameObject button;
	public GameObject scrollBack;

	public List<CharacterInfo> charList;

	public RectTransform first;
	public RectTransform last;
	public RectTransform selectRect;
	public RectTransform scroll;

	public RectTransform selected;

	public ScrollRect scrollRect;

	public Text name;
	public Text exp;

	public float distBybetw = 150f;

	List<RectTransform> info = new List<RectTransform>();

	void Start()
	{
		int count = 0;
		int firstSpawn = 1;

		// RectTransform obj1 = Instantiate(button).GetComponent<RectTransform>();
		// obj1.SetParent(scrollBack.GetComponent<RectTransform>());
		// obj1.localPosition = new Vector3(-390,0,0);
		// info.Add(obj1);

		while(count < 7)
		{
			for(int i = 0; i < charList.Count; ++i)
			{
				RectTransform obj = Instantiate(button).GetComponent<RectTransform>();
				obj.SetParent(scrollBack.GetComponent<RectTransform>(),false);
				obj.localPosition = new Vector3(-390 + (count * distBybetw),0,0);

				obj.GetComponent<Image>().sprite = charList[i].inGameImg;
				obj.GetComponent<InfoUpdate>().info = charList[i];

				info.Add(obj);

				if(count == 0)
					first = obj;
				else
					last = obj;

				++count;
			}

			if(firstSpawn == 1)
				firstSpawn = 0;
			
		}

		SetNearObj();
	}

	RectTransform GetFirst()
	{
		RectTransform foo = new RectTransform();
		for(int i = 0; i < info.Count; ++i)
		{
			if(i == 0)
			{
				foo = info[i];
				continue;
			}

			if(foo.position.x > info[i].position.x)
				foo = info[i];
		}

		return foo;
	}

	RectTransform GetLast()
	{
		RectTransform foo = new RectTransform();
		for(int i = 0; i < info.Count; ++i)
		{
			if(i == 0)
			{
				foo = info[i];
				continue;
			}

			if(foo.position.x < info[i].position.x)
				foo = info[i];
		}

		return foo;
	}

	RectTransform GetNear()
	{
		RectTransform foo = new RectTransform();
		float check = 0;

		for(int i = 0; i < info.Count; ++i)
		{
			if(info[i].GetComponent<InfoUpdate>().info.isLock)
			{
				continue;
			}

			if(i == 0)
			{
				if(selectRect.position.x > info[i].position.x)
					check = selectRect.position.x - info[i].position.x;
				else
					check = info[i].position.x - selectRect.position.x;

				foo = info[i];
			}
			else
			{
				float one = 0;
				if(selectRect.position.x > info[i].position.x)
					one = selectRect.position.x - info[i].position.x;
				else
					one = info[i].position.x - selectRect.position.x;

				if(one < check)
				{
					check = one;
					foo = info[i];
				}
			}
		}

		return foo;
	}

	public void SetNearObj()
	{
		selected = GetNear();
		selected.SetAsLastSibling();
		selected.GetComponent<InfoUpdate>().InfoLoad(name,exp);
	}

	void Update()
	{
		for(int i =0; i < info.Count; ++i)
		{
			float height = 0;
			Vector2 p1 = selectRect.position;
			Vector2 p2 = info[i].position;
			p1.y = 0;
			p2.y = 0;
			height = Vector2.Distance(p1,p2);
			Vector3 pos = info[i].localPosition;
			float value = Mathf.Abs(height -  300);
			if(height < 300)
			{
				float c = 0.1f + (value / 300);
				if (c > 1)
					c = 1;

				pos.y = value / 5;
				info[i].localScale = new Vector3(1 + value / 1000,1 + value / 1000,1);
				info[i].GetComponent<Image>().color = new Color(c,c,c,1);
			}
			else
			{
				pos.y = 0;
				info[i].localScale = new Vector3(1,1,1);
				if(info[i].GetComponent<Image>() == null)
					Debug.Log("cehck");
				info[i].GetComponent<Image>().color = new Color(0.1f,0.1f,0.1f,1);
			}
			info[i].localPosition = pos;
		}

		if(last.position.x < Screen.width)
		{
			Vector3 pos = last.localPosition;
			pos.x += distBybetw;
			first.localPosition = pos;
			last = first;
			first = GetFirst();
		}
		else if(first.position.x > 0)
		{
			Vector3 pos = first.localPosition;
			pos.x -= distBybetw;
			last.localPosition = pos;
			first = last;
			last = GetLast();
		}
		
			float check = 0;
			if(selectRect.position.x > selected.position.x)
				check = selectRect.position.x - selected.position.x;
			else
				check = selected.position.x - selectRect.position.x;

		if(Mathf.Abs(check) >= 0.1f && Mathf.Abs(scrollRect.velocity.x) < 100)
		{
			scrollRect.velocity = new Vector2(0,0);

			check = 0;
			if(selectRect.position.x > selected.position.x)
				check = selectRect.position.x - selected.position.x;
			else
				check = selected.position.x - selectRect.position.x;

			if(selectRect.position.x > selected.position.x)
			{

				if(selectRect.position.x > selected.position.x)
				{
					Vector3 pos = scroll.position;
					pos.x += check * Time.deltaTime * 5;
					scroll.position = pos;
				}
			}
			else
			{
				if(selectRect.position.x < selected.position.x)
				{
					Vector3 pos = scroll.position;
					pos.x -= check * Time.deltaTime * 5;
					scroll.position = pos;
				}
			}

			// if(Mathf.Abs(check) < 3f)
			// {
			// 	float left = scroll.localPosition.x % 100;
			// 	Vector3 pos = scroll.localPosition;

			// 	if(Mathf.Abs(left) > 50)
			// 	{
			// 		if(left > 0)
			// 		{
			// 			pos.x += 100 - left;
			// 		}
			// 		else
			// 		{
			// 			pos.x -= 100 + left;
			// 		}
			// 	}
			// 	else
			// 	{
			// 		if(left > 0)
			// 		{
			// 			pos.x -= left;
			// 		}
			// 		else
			// 		{
			// 			pos.x += -left;
			// 		}
			// 	}
			// 	pos.x = (float)Math.Round((double)pos.x);
			// 	scroll.localPosition = pos;
			// }
		}

	}
}
