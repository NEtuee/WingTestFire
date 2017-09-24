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
	public Image illust;

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
				obj.localPosition = new Vector3(-390 + (count * 200),0,0);

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
		selected.GetComponent<InfoUpdate>().InfoLoad(name,exp,illust);
	}

	void Update()
	{
		if(last.position.x < Screen.width)
		{
			Vector3 pos = last.localPosition;
			pos.x += 200;
			first.localPosition = pos;
			last = first;
			first = GetFirst();
		}
		else if(first.position.x > 0)
		{
			Vector3 pos = first.localPosition;
			pos.x -= 200;
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

			if(Mathf.Abs(check) < 3f)
			{
				float left = scroll.localPosition.x % 100;
				Vector3 pos = scroll.localPosition;

				if(Mathf.Abs(left) > 50)
				{
					if(left > 0)
					{
						pos.x += 100 - left;
					}
					else
					{
						pos.x -= 100 + left;
					}
				}
				else
				{
					if(left > 0)
					{
						pos.x -= left;
					}
					else
					{
						pos.x += -left;
					}
				}
				pos.x = (float)Math.Round((double)pos.x);
				scroll.localPosition = pos;
			}
		}

	}
}
