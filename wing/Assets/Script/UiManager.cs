using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

	public GameObject mainUI;
	public GameObject conversationUI;
	public Image image;
	public Text text;
	
	public Image left;
	public Image right;

	public Text theText;
	public Text name;
	public Text ready;
	public Text coin;
	public TextAsset textFile;
    public string[] textLine;

	public int currentLine = 0;
	public int endLine = 8;

	int line = 0;

	bool isTyping;
	bool cancelTyping;
	public bool textEnd = false;
	public bool animationEnd = false;

	Animator msgbox;

	void Start()
	{
		if(textFile != null)
        {
            textLine = (textFile.text.Split('\n'));
        }

		msgbox = conversationUI.GetComponent<Animator>();
		//Scrolling();
	}

	public void Progress()
	{
		if(mainUI.activeSelf)
		{
			text.text = "Score : " + GameManager.instance.score;
			coin.text = "Coin : " + GameManager.instance.coin;
			image.fillAmount = GameManager.instance.player.GetHp() / GameManager.instance.player.maxHp;
		}
		if(conversationUI.activeSelf)
		{
			if(Input.GetMouseButtonDown(0) && conversationUI.activeSelf && !textEnd)
			{
				if(isTyping)
				{
					cancelTyping = true;
				}
				else
				{
					Scrolling(currentLine);
				}

			}
		}
	}

	public void AnimationEnd()
	{
		animationEnd = true;
		textEnd = false;
		msgbox.SetTrigger("init");
	}

	public void Scrolling(int startLine)
	{
		int mulLine = line + startLine;

		if(mulLine == endLine)
		{
			textEnd = true;
			msgbox.SetTrigger("end");
			return;
		}

		int index = textLine[mulLine].IndexOf("/");
		if(index != -1)
		{
			string[] s = textLine[mulLine].Split('/');
			name.text = s[0];
			Swap(s[1]);
			++line;

			int show = int.Parse(s[2]);

			switch(show)
			{
			case 0://nobody
				left.enabled = false;
				right.enabled = false;
				break;
			case 1://left only
				left.enabled = true;
				right.enabled = false;
				break;
			case 2://right only
				left.enabled = false;
				right.enabled = true;
				break;
			case 3://both
				left.enabled = true;
				right.enabled = true;
				break;
			}

			mulLine = line + startLine;
		}

		StartCoroutine("ScrollText",textLine[mulLine]);
		++line;
	}

	IEnumerator ScrollText(string lineOfText)
    {
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;

        while(isTyping && !cancelTyping && letter < lineOfText.Length - 1)
        {
            theText.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(0.04f);
        }
        theText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }

	void Swap(string side)
	{
		int s = int.Parse(side);
		if(s == 0)
		{
			right.color = new Color(0.5f,0.5f,0.5f,1);
			left.color = new Color(1,1,1,1);
			msgbox.SetTrigger("left");
		}
		else
		{
			left.color = new Color(0.5f,0.5f,0.5f,1);
			right.color = new Color(1,1,1,1);
			msgbox.SetTrigger("right");
		}

	}


}
