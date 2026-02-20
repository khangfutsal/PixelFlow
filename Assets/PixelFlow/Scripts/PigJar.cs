using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace PixelFlow
{
    public class PigJar : MonoBehaviour
    {
        [Header("Reference Settings")]
        [SerializeField] private BoxCollider boxCollider;

        [Header("PigJar Properties")]
        [SerializeField] private PigJarData data;

        [Header("Shooting Settings")]
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float shootInterval = 1f;
        [SerializeField] private float raycastDistance = 20f;
        [SerializeField] private LayerMask gemLayer;

        [Header("Visual Settings")]
        [SerializeField] private Transform shootPoint;

        [SerializeField] private TextMeshPro countTxT;
        [SerializeField] private Color disable_countColor;
        [SerializeField] private Color enable_countColor;


        private float shootTimer = 0f;
        private Animator animator;

        private Action onSlotQueue;
        private Action onSlotBuffer;
        private Action onPigJarDeath;

        public PigJarData GetData() => data;


        private void Awake()
        {
            animator = GetComponent<Animator>();

            if (shootPoint == null)
                shootPoint = transform;
        }


        private void Update()
        {
            if (!data.isPlaying || data.count <= 0) return;
            TryShootGem();

        }

        public void SetData(PigJarData pigJarData)
        {
            data = pigJarData;
            UpdateVisual();
        }

        public void UpdateVisual()
        {
            UpdateCountText();
        }

        public void ReadyToShoot()
        {
            data.isPlaying = true;
            onPigJarDeath = null;
            onSlotQueue = null;
            DisableInteract();
        }


        private void TryShootGem()
        {
            Vector3 dir = shootPoint.forward;
            dir.Normalize();

            Vector3 origin = shootPoint.position;

            RaycastHit hit;

            if (Physics.Raycast(origin, dir, out hit, raycastDistance, gemLayer))
            {
                Gem gem = hit.collider.GetComponent<Gem>();
                if (gem != null &&
                    !gem.GetGemData().isDestroyed &&
                    gem.GetGemData().color == data.color &&
                    !gem.IsAiming())
                {
                    gem.SetIsAiming();
                    ShootProjectile(gem);

                    if (data.count <= 0)
                    {
                        DestroyPigJar();
                    }
                    Debug.Log("Hit gem on Z axis!");
                }
            }


        }

        private void UpdateCountText()
        {
            countTxT.text = data.count.ToString();
        }

        private void MinusCountShoot()
        {
            data.count--;
            UpdateCountText();
        }

        private void ShootProjectile(Gem gem)
        {
            if (projectilePrefab == null)
            {
                Debug.LogWarning("Projectile prefab not assigned!");
                return;
            }

            PlayShootAnimation();

            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Projectile proj = projectile.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.Initialize(gem.GetPosition(), gem);
            }
            MinusCountShoot();
        }

        public void OnClickedToConveyor()
        {

            data.isPlaying = true;
            StopIdleAnimation();
        }

        private void PlayIdleAnimation()
        {
            if (animator != null)
            {
                animator.SetBool("IsIdle", true);
            }
            else
            {
                // Simple idle animation using scale
            }
        }

        private void StopIdleAnimation()
        {
            if (animator != null)
            {
                animator.SetBool("IsIdle", false);
            }
            else
            {
                StopAllCoroutines();
            }
        }

        private void PlayShootAnimation()
        {
            if (animator != null)
            {
                animator.SetTrigger("Shoot");
            }
            else
            {
                // Simple shoot animation
            }
        }

        private void DestroyPigJar()
        {
            Debug.Log($"[Death]");
            data.isPlaying = false;
            onPigJarDeath?.Invoke();
            this.gameObject.SetActive(false);
        }

        public void Subscribe_OnPigJarDeath(Action action)
        {
            onPigJarDeath += action;
        }


        public void OnClicked()
        {
            onSlotQueue?.Invoke();
            onSlotBuffer?.Invoke();


            onSlotQueue = null;
            onSlotBuffer = null;
        }

        public void SetInteractWithText(bool value)
        {
            if (value)
            {
                SetCountTextColor(enable_countColor);
                EnableInteract();
            }
            else
            {
                SetCountTextColor(disable_countColor);
                DisableInteract();
            }
        }

        public void EnableInteract()
        {
            boxCollider.enabled = true;
        }

        public void DisableInteract()
        {
            boxCollider.enabled = false;
        }

        private void SetCountTextColor(Color color)
        {
            countTxT.color = color;
        }

        public void SubscribeOnSlotQueue(Action action)
        {
            onSlotQueue += action;
        }

        public void SubscribeOnSlotBuffer(Action action)
        {
            onSlotBuffer += action;
        }

        private void OnDrawGizmos()
        {
            if (data.isPlaying)
            {
                Gizmos.color = UnityEngine.Color.yellow;
                Vector3 origin = shootPoint != null
          ? shootPoint.position
          : transform.position;

                Vector3 direction = transform.forward;

                Gizmos.DrawRay(origin, direction * raycastDistance);
            }
        }

    }
}
