using UnityEngine;
using System.Collections;

namespace PixelFlow
{
    public class Gem : MonoBehaviour
    {
        [Header("Gem Properties")]
        [SerializeField] private GemData gemData;

        [Header("Visual Settings")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float destroyAnimationDuration = 0.2f;

        public bool IsDestroy() => gemData.isDestroyed;

        private void Awake()
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public GemData GetGemData() => gemData;


        public void SetIsAiming()
        {
            gemData.isAim = true;
        }

        public bool IsAiming() => gemData.isAim;

        public void DestroyGem()
        {
            if (gemData.isDestroyed) return;

            gemData.isDestroyed = true;

            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.OnGemDestroy(this);
            }
            this.gameObject.SetActive(false);
        }


        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}
