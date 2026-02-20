using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PixelFlow.UI
{
    public class WinUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI rewardText;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Animation Settings")]
        [SerializeField] private float fadeInDuration = 0.5f;

        private void Awake()
        {
            if (restartButton != null)
                restartButton.onClick.AddListener(OnRestartClicked);
            
            if (nextLevelButton != null)
                nextLevelButton.onClick.AddListener(OnNextLevelClicked);
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

            // Display reward
            if (rewardText != null && GameManager.Instance != null)
            {
                // Get reward from level config if available
                int reward = 100; // Default
                rewardText.text = $"Reward: {reward} Coins";
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

        private void OnRestartClicked()
        {
            //if (GameManager.Instance != null)
            //{
            //    GameManager.Instance.RestartLevel();
            //}
        }

        private void OnNextLevelClicked()
        {
            //if (GameManager.Instance != null)
            //{
            //    GameManager.Instance.NextLevel();
            //    gameObject.SetActive(false);
            //}
        }
    }
}
