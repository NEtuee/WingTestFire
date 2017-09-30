using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	public float speed;
	public float dirAngle;

	void Update()
	{
		Vector3 dir = new Vector3(Mathf.Cos((dirAngle * Mathf.Deg2Rad)),Mathf.Sin((dirAngle * Mathf.Deg2Rad)),0);

		transform.position += dir * speed * Time.deltaTime;

	}
}
