using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public enum PatternType
	{
		Scatter,
		AtOnce,
		Homing,
	}

	[System.Serializable]
	public struct Behaviour
	{
		public string name;
		public Vector2 value_0;
		public Vector2[] bezier;
		public Vector3 value_1;
		public float speed;
	}

	[System.Serializable]
	public struct Pattern
	{
		public PatternType type;
		public float fireRate;
		public float minAngle;
		public float maxAngle;
		public float angleFactor;
		public float cooldown;
		public float patternCooldown;
		public float speed;
	}

	public bool doing = false;
	public bool reverse = false;
	public bool loop = false;

	public int workCount = 0;

	public GameObject bullet;
	public List<Behaviour> work;
	public Pattern pattern;

	float fireRate = 0;
	float angle = 0;
	float cooldown = 0;
	float patternCooldown = 0;

	public void StartNextWork()
	{
		if(workCount == work.Count && loop)
		{
			reverse = false;
			workCount = 0;
		}

		if(!doing && workCount < work.Count)
		{
			doing = true;

			// if(workCount < 0 && loop)
			// {
			// 	Debug.Log("Check1");
			// 	reverse = false;
			// 	workCount = 0;
			// } //reverse

			StartCoroutine(work[workCount].name,work[workCount]);
			workCount += reverse ? -1 : 1;
		}
	}

	public void Progress()
	{
		StartNextWork();
		if(GameManager.instance.player.gameObject != null)
			PatternProgress();
	}

	void PatternProgress()
	{
		switch(pattern.type)
		{
		case PatternType.AtOnce://firerate == count
			patternCooldown += Time.deltaTime;
			if(patternCooldown >= pattern.patternCooldown)
			{
				cooldown += Time.deltaTime;
				if(pattern.cooldown <= cooldown)
				{
					cooldown = 0;
					float ang = pattern.minAngle;
					while(true)
					{
						if(ang > pattern.maxAngle)
							break;
						GameObject b = Instantiate(bullet,transform.position,Quaternion.identity) as GameObject;
						BulletScript bs = b.GetComponent<BulletScript>();

						bs.speed = pattern.speed;
						bs.dirAngle = ang;
						ang += pattern.angleFactor;
					}

					++fireRate;
					if(fireRate >= pattern.fireRate)
					{
						fireRate = 0;
						patternCooldown = 0;
					}
				}
			}
			break;
		case PatternType.Homing:
			patternCooldown += Time.deltaTime;
			if(patternCooldown >= pattern.patternCooldown)
			{
				cooldown += Time.deltaTime;
				if(cooldown >= pattern.cooldown)
				{
					cooldown = 0;
					Vector2 dir = transform.position - GameManager.instance.player.transform.position;
					dir = -dir.normalized;
					GameObject b = Instantiate(bullet,transform.position,Quaternion.identity) as GameObject;
					BulletScript bs = b.GetComponent<BulletScript>();

					bs.speed = pattern.speed;
					bs.dirAngle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;

					++fireRate;
					if(fireRate >= pattern.fireRate)
					{
						fireRate = 0;
						patternCooldown = 0;
					}
				}
			}
			break;
		case PatternType.Scatter:
			patternCooldown += Time.deltaTime;
		
			if(patternCooldown >= pattern.patternCooldown)
			{
				cooldown += Time.deltaTime;
				if(cooldown >= pattern.cooldown)
				{
					float ang;
					cooldown = 0;
					if(fireRate > 0)
						ang = pattern.minAngle + (pattern.angleFactor * fireRate);
					else
						ang = pattern.minAngle;
					
					if(ang > pattern.maxAngle)
					{
						fireRate = 0;
						patternCooldown = 0;
						break;
					}
					GameObject b = Instantiate(bullet,transform.position,Quaternion.identity) as GameObject;
					BulletScript bs = b.GetComponent<BulletScript>();

					bs.speed = pattern.speed;
					bs.dirAngle = ang;

					ang += pattern.angleFactor;
					++fireRate;
				}
			}
			break;
			
		}
	}

	Vector2 Move(float t,Vector2 p0,Vector2 p1)
    {
        Vector2 dir = p0 - p1;
        Vector2 pos = p0 + (-dir) * t;
        return pos;
    }

    Vector2 Curve(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector2 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }


	IEnumerator MoveNextPoint_constant(Behaviour be) //0번 : 목표 좌표, 속도 = 시간 * 속도
	{
		float t = 0f;
		Vector2 pos = transform.position;

		while(true)
		{
			yield return null;

			t += Time.deltaTime * be.speed;

			transform.position = Move(t,pos,be.value_0);

			if(t > 1)
			{
				transform.position = Move(1,pos,be.value_0);
				break;
			}
		}

		doing = false;
		if(GameManager.instance.gameLoop)
			StartNextWork();
	}

	IEnumerator MoveNextPoint_curve(Behaviour be) //0번 : 목표 좌표, 속도 = 시간 * 속도
	{
		float t = 0f;
		Vector2 pos = transform.position;

		while(true)
		{
			yield return null;

			t += Time.deltaTime * be.speed;

			if(t >= 1)
			{
				transform.position = Curve(1,pos,be.bezier[0],be.bezier[1],be.value_0);
				break;
			}

			transform.position = Curve(t,pos,be.bezier[0],be.bezier[1],be.value_0);
		}

		doing = false;
		if(GameManager.instance.gameLoop)
			StartNextWork();
	}

	IEnumerator MoveNextPoint_Smooth(Behaviour be) //0번 : 목표 좌표, 속도 = 시간 * 속도
	{
		while(true)
		{
			yield return null;

			transform.position = Vector2.Lerp(transform.position,be.value_0,Time.deltaTime * be.speed);

			if(Vector2.Distance(transform.position,be.value_0) < 0.01f)
			{
				transform.position = be.value_0;
				break;
			}
		}

		doing = false;
		if(GameManager.instance.gameLoop)
			StartNextWork();
	}

	IEnumerator LoopfromSpecificPoint(Behaviour be)
	{
		yield return null;

		workCount = (int)be.speed;

		doing = false;
		if(GameManager.instance.gameLoop)
			StartNextWork();
	}

	// IEnumerator Reverse(Behaviour be)
	// {
	// 	//--workCount;
	// 	reverse = true;

	// 	yield return null;

	// 	doing = false;
	// }
}
