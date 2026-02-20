using PixelFlow;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Plate : MonoBehaviour
{
    public SplineContainer spline;
    public PigJar occupiedPig;
    public float speed = 0.2f;

    public float t;
    public bool isMoving;
    public bool isFinishedSpline;
    private Action<Plate> OnExit;

    public void StartMoving(SplineContainer spline, PigJar pigJar)
    {
        occupiedPig = pigJar;
        this.spline = spline;



        pigJar.transform.SetParent(this.transform);
        t = 0f;

        isFinishedSpline = false;
        isMoving = true;

        occupiedPig.ReadyToShoot();
        occupiedPig.Subscribe_OnPigJarDeath(ReturnToDispenser);

    }

    void Update()
    {
        if (!isMoving) return;

        t += Time.deltaTime * speed;
        t = Mathf.Clamp01(t);

        Vector3 position = spline.EvaluatePosition(t);

        transform.position = position;

        Vector3 direction = spline.EvaluateTangent(t);
        direction.y = 0f;   // bỏ ảnh hưởng trục Y
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            float angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, angleY - 90f, 90f);
        }

        if (t >= 1f)
        {
            isFinishedSpline = true;
            ReturnToDispenser();
        }
    }


    public void ReturnToDispenser()
    {
        isMoving = false;
        if(occupiedPig != null) occupiedPig.transform.SetParent(null);



        Debug.Log($"Return");
        if (isFinishedSpline)
        {
            if (!SlotBufferManager.Instance.HasEmptySlot())
            {
                // Lose bởi vì full slot buffer
                GameManager.Instance.TriggerLose();
                return;
            }

            SlotBufferManager.Instance.GotoSlotBuffer(occupiedPig);

        }

        OnExit?.Invoke(this);
        occupiedPig = null;
        OnExit = null;

    }

    public void StopMoving()
    {
        isMoving = false;
    }

    public void Subscribe_OnExit(Action<Plate> action)
    {
        OnExit += action;
    }
}
