using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;

namespace PixelFlow
{
    public class SlotQueue : MonoBehaviour
    {

        [SerializeField] private Queue<PigJar> qPigJar = new Queue<PigJar>();
        [SerializeField] private List<Vector3> slotPositions = new List<Vector3>();

        [Header("Setup PigJar Spawned")]
        [SerializeField] private Transform posTf;
        [SerializeField] private float spacingZ = -1.585f;

        [SerializeField] private List<PigJarData> L_pigJarData;


        private void Start()
        {
            // Generate slot positions
            GenerateSlotQueue();

        }

        private void GenerateSlotQueue()
        {
            for (int i = 0; i < L_pigJarData.Count; i++)
            {
                var pigJarData = L_pigJarData[i];
                if (pigJarData == null) return;

                float distZ = posTf.position.z + (i * spacingZ);

                var pigJar = PigJarManager.Instance.GetPigJarByColor(pigJarData.color);
                pigJar.SetData(pigJarData);
                pigJar.SetInteractWithText(false);

                var pigJarObj = Instantiate(pigJar, new Vector3(posTf.position.x, 0, distZ), Quaternion.identity);

                pigJarObj.transform.SetParent(transform);

                slotPositions.Add(pigJarObj.transform.position);
                qPigJar.Enqueue(pigJarObj);
            }
            EnablePigJarPeek();
        }


        // Note Xóa
        public void EnqueuePigJar(PigJar pigJar)
        {
            qPigJar.Enqueue(pigJar);
            UpdateQueuePositions();
        }

        public void EnablePigJarPeek()
        {
            if (qPigJar.Count == 0)
                return;

            PigJar pigJar = PeekPigJar();
            pigJar.SetInteractWithText(true);
            pigJar.SubscribeOnSlotQueue(DequeuePigJar);
        }

        public void DequeuePigJar()
        {
            if (qPigJar.Count == 0)
                return;

            qPigJar.Dequeue();
            UpdateQueuePositions();
        }

        public PigJar PeekPigJar()
        {
            if (qPigJar.Count == 0)
                return null;

            return qPigJar.Peek();
        }

        public int GetCount()
        {
            return qPigJar.Count;
        }


        private void UpdateQueuePositions()
        {
            int index = 0;
            foreach (PigJar pigJar in qPigJar)
            {
                if (index < slotPositions.Count)
                {
                    pigJar.transform.DOMove(slotPositions[index], 0.5f);
                }
                index++;
            }
            EnablePigJarPeek();
        }

    }
}
