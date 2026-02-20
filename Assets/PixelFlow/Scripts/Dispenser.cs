using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

namespace PixelFlow
{
    public class Dispenser : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private List<Plate> L_plate = new List<Plate>();


        [Header("Dispenser Settings")]
        [SerializeField] private int limitCountDispenser = 5;

        [Header("Visual Settings")]
        [SerializeField] private TextMeshPro dispenserCountTxT;


        [Header("Plates Settings")]
        [SerializeField] private Transform reachingPlatesPointTf;
        [SerializeField] private Transform startPointPlatesTf;
        [SerializeField] private Transform platesHolder;
        [SerializeField] private Plate platePrefab;

        [SerializeField] private float spacingPlateX = -0.115f;

        private int currentCount;

        private void Start()
        {
            currentCount = limitCountDispenser;
            InitializeQueues();
        }

        private void InitializeQueues()
        {
            for (int i = 0; i < limitCountDispenser; i++)
            {
                float x = startPointPlatesTf.position.x + (i * spacingPlateX);
                Vector3 v = new Vector3(x, startPointPlatesTf.position.y, startPointPlatesTf.position.z);
                var plateObj = Instantiate(platePrefab, v, Quaternion.identity, platesHolder);
                L_plate.Add(plateObj);
            }
        }

        public bool CanDispense()
        {
            return currentCount > 0 && currentCount <= 5;
        }


        public void RestorePlate(Plate plate)
        {
            Sequence sq = DOTween.Sequence();
            sq.Append(plate.transform.DOMove(reachingPlatesPointTf.position, 0.3f));
            sq.Join(plate.transform.DORotate(reachingPlatesPointTf.rotation.eulerAngles, 0.3f));
            sq.OnComplete(() =>
            {
                PlusCount();
                AddPlate(plate);
                ArrangePlates();
                // Moving                        
            });
        }

        public void ArrangePlates()
        {
            for (int i = 0; i < L_plate.Count; i++)
            {
                var plate = L_plate[i];
                if (plate == null) continue;

                plate.transform.DOKill();

                float x = startPointPlatesTf.position.x + (i * spacingPlateX);
                Vector3 v = new Vector3(x, plate.transform.position.y, plate.transform.position.z);
                plate.transform.DOMove(v, 2f);
            }
        }

        public void RemovePlate(Plate plate)
        {
            L_plate.Remove(plate);
        }

        public void AddPlate(Plate plate)
        {
            if (plate == null) return;
            L_plate.Add(plate);
        }


        public int GetCurrentCount()
        {
            return currentCount;
        }


        public void MinusCount()
        {
            currentCount -= 1;
            UpdateCountTxT();
        }

        public void PlusCount()
        {
            currentCount += 1;
            UpdateCountTxT();
        }

        public void UpdateCountTxT()
        {
            dispenserCountTxT.text = $"{currentCount}/{limitCountDispenser}";
        }


        public Plate GetPlate()
        {
            return L_plate.Find(p => !p.isMoving);
        }

        public void StopMovingPlates()
        {
            foreach (Plate plate in L_plate)
            {
                plate.StopMoving();
            }
        }

    }
}