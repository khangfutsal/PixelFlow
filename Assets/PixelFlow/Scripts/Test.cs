using DG.Tweening;
using PixelFlow;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal.Converters;
using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    public Transform a;
    public Transform b;

    private void Start()
    {
        a.transform.DOMove(b.transform.position, 2f).OnComplete(() =>
        {
            a.SetParent(b);
        });
    }

}
