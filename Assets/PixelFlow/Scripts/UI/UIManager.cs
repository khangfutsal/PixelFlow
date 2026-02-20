using UnityEngine;
using TMPro;

namespace PixelFlow.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("UI Panels")]
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;

        [Header("HUD Elements")]
        [SerializeField] private TextMeshProUGUI dispenserCountText;
        [SerializeField] private TextMeshProUGUI gemCountText;

        private void Start()
        {
            HideAllPanels();
        }

        private void Update()
        {
            UpdateHUD();
        }

        private void HideAllPanels()
        {
            if (winPanel != null) winPanel.SetActive(false);
            if (losePanel != null) losePanel.SetActive(false);
        }

        public void ShowWinPanel()
        {
            if (winPanel != null)
            {
                winPanel.SetActive(true);
            }
        }

        public void ShowLosePanel()
        {
            if (losePanel != null)
            {
                losePanel.SetActive(true);
            }
        }

        private void UpdateHUD()
        {
            //if (GameManager.Instance == null) return;

            //// Update dispenser count
            //if (dispenserCountText != null && GameManager.Instance.dispenser != null)
            //{
            //    Dispenser dispenser = GameManager.Instance.dispenser;
            //    dispenserCountText.text = $"Dispenser: {dispenser.GetCurrentCount()}/{dispenser.GetLimitCount()}";
            //}

            //// Update gem count (if needed)
            //if (gemCountText != null)
            //{
            //    // This would require tracking in GameManager
            //    // gemCountText.text = $"Gems: {remainingGems}";
            //}
        }
    }
}
