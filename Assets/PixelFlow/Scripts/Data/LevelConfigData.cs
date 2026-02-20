using UnityEngine;

namespace PixelFlow.Data
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "PixelFlow/Level Config Data")]
    public class LevelConfigData : ScriptableObject
    {
        [Header("Level Information")]
        public int id;

        [Header("Gameplay Settings")]
        [Tooltip("Maximum number of times the dispenser can dispense items")]
        public int limitCountDispenser = 5;

        [Header("Rewards")]
        [Tooltip("Coins awarded when player wins this level")]
        public int rewardCoin = 100;

        [Header("Optional Settings")]
        public string levelName;
        [TextArea(3, 5)]
        public string levelDescription;
    }
}
