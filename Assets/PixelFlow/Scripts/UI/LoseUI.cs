using UnityEngine;
using UnityEngine.UI;

namespace PixelFlow.UI
{
    public class LoseUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Button retryButton;
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Animation Settings")]
        [SerializeField] private float fadeInDuration = 0.5f;

        private void Awake()
        {
            if (retryButton != null)
                retryButton.onClick.AddListener(OnRetryClicked);
        }

        private void OnEnable()
        {
            ShowWithAnimation();
        }

        private void ShowWithAnimation()
        {
            if (canvasGroup != null)
            {
                StartCoroutine(FadeIn());
            }
        }

        private System.Collections.IEnumerator FadeIn()
        {
            canvasGroup.alpha = 0f;
            float elapsed = 0f;

            while (elapsed < fadeInDuration)
            {
                elapsed += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeInDuration);
                yield return null;
            }

            canvasGroup.alpha = 1f;
        }

        private void OnRetryClicked()
        {
            //if (GameManager.Instance != null)
            //{
            //    GameManager.Instance.RestartLevel();
            //}
        }
    }
}
