using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circular : MonoBehaviour {

	public float angle = 0;
	public float width = 1f;
	public float height = 1f;

	void Update () {
		
		transform.position = new Vector3(Mathf.Cos((angle * Mathf.Deg2Rad)) * width,Mathf.Sin((angle * Mathf.Deg2Rad)) * height,0);
	}
}
