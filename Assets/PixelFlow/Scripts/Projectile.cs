using UnityEngine;

namespace PixelFlow
{
    public class Projectile : MonoBehaviour
    {
        [Header("Projectile Settings")]
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifetime = 5f;

        private Vector3 targetPosition;
        private bool hasTarget = false;
        private Gem gem;

        private void Start()
        {
            Destroy(gameObject, lifetime);
        }

        public void Initialize(Vector3 target, Gem gem)
        {
            targetPosition = target;
            this.gem = gem;
            hasTarget = true;

            // Set visual color

        }

        private void Update()
        {
            if (!hasTarget) return;

            // Move towards target
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Check if reached target
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                gem.DestroyGem();
                Destroy(gameObject);
            }
        }
    }
}
