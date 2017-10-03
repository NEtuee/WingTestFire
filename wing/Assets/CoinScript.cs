using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

	float angle = 0f;
	float dist = 0;
	float time = 0;

	Vector2 origin;
	Vector2 startup;
	Vector2 dir;

	bool target = false;

	void Start()
	{
		dist = Random.Range(1f,2f);
		angle = Random.Range(0f,360f);

		origin = transform.position;
		dir = new Vector3(Mathf.Cos((angle * Mathf.Deg2Rad)),Mathf.Sin((angle * Mathf.Deg2Rad)),0);
		startup = origin + (dir * dist);
	}

	void Update()
	{
		if(!target)
		{
			time += Time.deltaTime * dist;
			Vector2 pos = Vector2.Lerp(origin,startup,time);

			if(time >= 1)
			{
				time = 0;
				origin = pos;
				target = true;
			}

			transform.position = pos;
		}
		else
		{
			time += Time.deltaTime * 3;

			Vector2 pos = Vector2.Lerp(origin,GameManager.instance.player.transform.position,time);

			if(time >= 1)
			{
				GameManager.instance.coin += 10;
				Destroy(this.gameObject);
			}

			transform.position = pos;
		}
	}

	Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
    {
        Vector3 P = x * Vector3.Normalize(B - A) + A;
        return P;
    }

}
