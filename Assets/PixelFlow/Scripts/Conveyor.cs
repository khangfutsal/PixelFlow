using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace PixelFlow
{
    public class Conveyor : MonoBehaviour
    {
        public static Conveyor Instance { get; private set; }

        [Header("References")]
        public SplineContainer spline;
        public Dispenser dispenser;

        [Header("Conveyor Settings")]

        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform startPointPigJar;

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


        public void AppendPigJar(PigJar pigJar)
        {
            var plate = dispenser.GetPlate();
            if (plate == null) return;

            plate.transform.DOKill();

            dispenser.RemovePlate(plate);
            dispenser.MinusCount();

            Sequence sq = DOTween.Sequence();
            sq.Append(plate.transform.DOMove(startPoint.position, 0.3f));
            sq.Join(plate.transform.DORotate(startPoint.rotation.eulerAngles, 0.3f));
            sq.Join(pigJar.transform.DOMove(startPointPigJar.position, 0.6f));
            sq.OnComplete(() =>
            {
                plate.StartMoving(spline, pigJar);
                plate.Subscribe_OnExit(OnPlateExit);
                // Moving                        
            });
            dispenser.ArrangePlates();

        }

        private void OnPlateExit(Plate plate)
        {
            dispenser.RestorePlate(plate);
        }

        void OnDisable()
        {
            DG.Tweening.DOTween.KillAll();
        }

    }
}
