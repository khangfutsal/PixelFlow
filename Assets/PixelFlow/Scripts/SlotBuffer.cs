using UnityEngine;
using System.Collections;

namespace PixelFlow
{
    public class SlotBuffer : MonoBehaviour
    {
        [Header("Buffer Settings")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private UnityEngine.Color normalColor = UnityEngine.Color.white;
        [SerializeField] private UnityEngine.Color alertColor = UnityEngine.Color.red;
        [SerializeField] private float blinkSpeed = 2f;
        [SerializeField] private Transform startPointPigJarStandingTf;

        [SerializeField] private PigJar occupyingPigJar = null;
        private bool isAlerting = false;

        public Transform GetTransform() => startPointPigJarStandingTf;

        private void Awake()
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetPigJar(PigJar pigJar)
        {
            occupyingPigJar = pigJar;
        }

        public PigJar GetPigJar()
        {
            return occupyingPigJar;
        }

        public void ClearSlot()
        {
            occupyingPigJar = null;
        }

        public bool IsOccupied()
        {
            return occupyingPigJar != null;
        }

        public void StartAlert()
        {
            if (!isAlerting)
            {
                isAlerting = true;
                StartCoroutine(BlinkAlert());
            }
        }

        public void StopAlert()
        {
            isAlerting = false;
            StopAllCoroutines();

            if (spriteRenderer != null)
            {
                spriteRenderer.color = normalColor;
            }
        }

        private IEnumerator BlinkAlert()
        {
            while (isAlerting)
            {
                if (spriteRenderer != null)
                {
                    float t = Mathf.PingPong(Time.time * blinkSpeed, 1f);
                    spriteRenderer.color = UnityEngine.Color.Lerp(normalColor, alertColor, t);
                }
                yield return null;
            }
        }
    }
}
