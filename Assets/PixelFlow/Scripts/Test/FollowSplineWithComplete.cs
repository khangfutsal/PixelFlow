using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class FollowSplineWithComplete : MonoBehaviour
{
    public SplineContainer spline;
    public float speed = 0.2f;   // tốc độ đi trên spline
    public bool completed = false;

    private float t = 0f;

    void Update()
    {
        if (completed) return;

        t += Time.deltaTime * speed;
        t = Mathf.Clamp01(t);

        Vector3 pos = spline.EvaluatePosition(t);
        transform.position = pos;

        if (t >= 1f)
        {
            OnSplineCompleted();
        }
    }

    void OnSplineCompleted()
    {
        completed = true;
        Debug.Log("Completed spline!");

        // Ví dụ: bay ra ngoài
        FlyOut();
    }

    void FlyOut()
    {
        // cách đơn giản
        transform.position += Vector3.up * 5f;
    }
}
