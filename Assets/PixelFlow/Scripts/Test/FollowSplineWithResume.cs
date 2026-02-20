using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class FollowSplineWithResume : MonoBehaviour
{
    public SplineContainer spline;
    public float speed = 0.01f;
    [Range(0, 1)] public float startT;

    private float t;

    void Start()
    {
        t = startT;
    }

    void Update()
    {
        t += Time.deltaTime * speed;
        t = Mathf.Repeat(t, 1f);
        transform.position = spline.EvaluatePosition(t);
    }
}
