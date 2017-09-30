using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public GameObject bullet;
	public float fireRate = 0.1f;
	public float patternRate = 3f;
	public float angleFactor = 4; 
	public float speed = 3;

	public float min = 140;
	public float max = 220;



	float frate = 0;
	float prate = 0;

	float angle;

	public bool rev = false;

	bool patternStart = true;

	Vector3 posOrigin;

	void Start()
	{
		if(rev)
			angle = max;
		else
			angle = min;
		
		posOrigin = transform.position;

		GameManager.instance.enemyCount += 1;
	}

	void Update()
	{
		if(patternStart)
		{
			if(prate >= patternRate)
			{
				Vector3 pos = transform.position;
				pos.x += 1.3f * Time.deltaTime;
				transform.position = pos;

				frate += Time.deltaTime;
				if(frate >= fireRate)
				{
					frate = 0;
					BulletScript b = (Instantiate(bullet,transform.position,Quaternion.identity) as GameObject).GetComponent<BulletScript>();
					b.dirAngle = angle;
					b.speed = speed;

					if(angle >= max && !rev)
					{
						angle = max;
						prate = 0;
						rev = true;
						patternStart = false;
					}
					else if(angle <= min && rev)
					{
						angle = min;
						prate = 0;
						rev = false;
						patternStart = false;
					}
					angle += rev ? -angleFactor : angleFactor;
					Destroy(b.gameObject,5);
			}
		}
		else
			prate += Time.deltaTime;
		}
		else
		{
			Vector3 pos = transform.position;
			pos.x = Mathf.Lerp(pos.x, posOrigin.x - 0.5f, 1 * Time.deltaTime);
			
			if(pos.x <= posOrigin.x)
			{
				pos.x = posOrigin.x;
				patternStart = true;
				prate = patternRate;
			}

			transform.position = pos;
		}


	}

}
