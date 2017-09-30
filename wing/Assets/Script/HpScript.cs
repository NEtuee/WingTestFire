using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HpScript : MonoBehaviour {

	public AttackScript.Team team;
	public float maxHp = 3;
	public bool isDead = false;

	public bool delete = false;

	public GameObject effect = null;

	public Color hitColor;

	float hp;
	Color origin;
	SpriteRenderer spr;

	void Start()
	{
		hp = maxHp;
		spr = GetComponent<SpriteRenderer>();
		origin = spr.color;
	}

	public float GetHp()
	{
		return hp;
	}

	IEnumerator ChangeColor()
	{
		spr.color = hitColor;
		yield return new WaitForSeconds(0.1f);
		spr.color = origin;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		AttackScript atk = col.GetComponent<AttackScript>();
		if(atk != null)
		{
			if(team != atk.team && !atk.safeMode)
			{
				if(atk.attackCount != 0)
					--atk.attackCount;
				else
					return;
				
				hp -= atk.damage;
				StopCoroutine("ChangeColor");
				StartCoroutine("ChangeColor");

				if(effect != null)
					Destroy((Instantiate(effect,transform.position,Quaternion.identity) as GameObject),0.5f);

				if(hp <= 0)
				{
					hp = 0;
					isDead = true;
					this.enabled = false;

					if(team == AttackScript.Team.Enemy)
					{
						GameManager.instance.enemyCount -= 1;
						GameManager.instance.score += 100;
					}
					if(delete)
						Destroy(gameObject);
				}

				if(atk.deleteWhenCol)
					Destroy(atk.gameObject);
			}
		}
	}
}
