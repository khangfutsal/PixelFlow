using DG.Tweening;
using PixelFlow;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotBufferManager : MonoBehaviour
{
    public static SlotBufferManager Instance { get; private set; }

    public List<SlotBuffer> L_slotBuffer = new List<SlotBuffer>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void GotoSlotBuffer(PigJar pigJar)
    {
        var slotBufferEmpty = L_slotBuffer.Find(s => !s.IsOccupied());
        if (slotBufferEmpty == null) return;

        slotBufferEmpty.SetPigJar(pigJar);
        pigJar.SubscribeOnSlotBuffer(() => { ClearSlotBuffer(slotBufferEmpty); });

        Transform vSlotBufferEmpty = slotBufferEmpty.GetTransform();

        pigJar.transform.DOKill();
        Sequence sq = DOTween.Sequence();

        sq.Append(pigJar.transform.DOMove(vSlotBufferEmpty.position, 1f));
        sq.Join(pigJar.transform.DORotate(vSlotBufferEmpty.rotation.eulerAngles, 0.3f));
        sq.Join(pigJar.transform.DOScale(vSlotBufferEmpty.localScale, 0.1f));
        sq.OnComplete(() =>
         {
             pigJar.EnableInteract();
         });
    }

    public void ArrangeSlotBuffersAnimation()
    {
        Debug.Log($"[Arrange]");
        for (int i = 0; i < L_slotBuffer.Count - 1; i++)
        {
            var slotBuffer_i = L_slotBuffer[i];
            if (slotBuffer_i.IsOccupied()) continue;

            for (int j = i + 1; j < L_slotBuffer.Count; j++)
            {
                var slotBuffer_j = L_slotBuffer[j];
                if (!slotBuffer_j.IsOccupied()) continue;

                MoveSlotBufferAnimation(slotBuffer_j, slotBuffer_i);
                break;

            }
        }
    }

    public void ClearSlotBuffer(SlotBuffer slotBuffer)
    {
        slotBuffer.ClearSlot();
        ArrangeSlotBuffersAnimation();
    }

    public void MoveSlotBufferAnimation(SlotBuffer s1, SlotBuffer s2)
    {
        var pigJar1 = s1.GetPigJar();

        var vPos2 = s2.GetTransform();

        s1.ClearSlot();
        s2.SetPigJar(pigJar1);

        pigJar1.transform.DOKill();
        pigJar1.transform.DOMove(vPos2.position, 1f);
    }

    public bool HasEmptySlot()
    {
        return L_slotBuffer.Any(s => !s.IsOccupied());
    }

}
