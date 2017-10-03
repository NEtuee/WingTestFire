using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    List<Vector2> positionList;

    public GameObject [] cur = new GameObject[4];

    public GameObject move;

    float t = 0;

    void Update()
    {
        t += 0.5f * Time.deltaTime;
       // move.transform.position = Curve(t,cur[0].transform.position, cur[1].transform.position, cur[2].transform.position, cur[3].transform.position);
        move.transform.position = Vector2.Lerp(move.transform.position,cur[3].transform.position,Time.deltaTime * 5);
        // if (t >= 1)
        //     t = 0;
        if(Vector2.Distance(move.transform.position,cur[3].transform.position) < 0.1f)
            move.transform.position = cur[0].transform.position;
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
}
