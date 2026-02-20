using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using PixelFlow.Data;

namespace PixelFlow
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Game State")]
        public GameState currentState = GameState.Playing;

        [Header("References")]
        public Conveyor conveyor;

        [Header("Level Settings")]
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private int currentLevelId = 1;

        [Header("Prefabs")]
        [SerializeField] private GameObject gemPrefab;
        [SerializeField] private GameObject pigJarPrefab;

        [Header("UI References")]
        [SerializeField] private GameObject winUI;
        [SerializeField] private GameObject loseUI;

        private List<Gem> activeGems = new List<Gem>();
        private int totalGems = 0;
        private int destroyedGems = 0;

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

        public void TriggerWin()
        {
            currentState = GameState.Won;
            conveyor.dispenser.StopMovingPlates();
            Debug.Log($"[Gameplay State] Won");
        }


        public void TriggerLose()
        {
            currentState = GameState.Lost;
            conveyor.dispenser.StopMovingPlates();
            Debug.Log($"[Gameplay State] Lost");
        }

        /*  Code cũ
        private void Start()
        {
            LoadLevel(currentLevelId);
            
            if (winUI != null) winUI.SetActive(false);
            if (loseUI != null) loseUI.SetActive(false);
        }

        private void Update()
        {
            // Handle PigJar clicking
            if (Input.GetMouseButtonDown(0) && currentState == GameState.Playing)
            {
                //HandlePigJarClick();
            }
        }

        public void LoadLevel(int levelId)
        {
            // Load level data from JSON
            TextAsset levelJson = Resources.Load<TextAsset>($"Levels/Level{levelId}");
            
            if (levelJson == null)
            {
                Debug.LogError($"Level{levelId}.json not found in Resources/Levels/");
                return;
            }

            LevelRenderData levelData = JsonUtility.FromJson<LevelRenderData>(levelJson.text);
            
            if (levelData == null)
            {
                Debug.LogError("Failed to parse level data");
                return;
            }

            SpawnLevel(levelData);

            // Apply level config if available
            if (levelConfig != null && dispenser != null)
            {
                dispenser.SetLimitCount(levelConfig.limitCountDispenser);
            }
        }

        private void SpawnLevel(LevelRenderData levelData)
        {
            // Clear existing objects
            ClearLevel();

            // Spawn gems
            //foreach (GemData gemData in levelData.gems)
            //{
            //    SpawnGem(gemData);
            //}

            totalGems = levelData.gems.Count;
            destroyedGems = 0;

            Debug.Log($"Level loaded: {totalGems} gems spawned");
        }

        private void SpawnGem(GemData gemData)
        {
            if (gemPrefab == null)
            {
                Debug.LogError("Gem prefab not assigned!");
                return;
            }

            //GameObject gemObj = Instantiate(gemPrefab, gemData.position, Quaternion.identity);
            //Gem gem = gemObj.GetComponent<Gem>();
            
            //if (gem != null)
            //{
            //    //gem.id = gemData.id;
            //    //gem.SetColor(gemData.color);
            //    activeGems.Add(gem);
            //}
        }

        private void ClearLevel()
        {
            foreach (Gem gem in activeGems)
            {
                if (gem != null)
                {
                    Destroy(gem.gameObject);
                }
            }
            activeGems.Clear();
        }

        public void OnGemDestroyed(Gem gem)
        {
            destroyedGems++;
            activeGems.Remove(gem);

            Debug.Log($"Gem destroyed: {destroyedGems}/{totalGems}");

            // Check win condition
            if (destroyedGems >= totalGems)
            {
                TriggerWin();
            }
        }

        public void TriggerWin()
        {
            if (currentState != GameState.Playing) return;

            currentState = GameState.Won;
            Debug.Log("YOU WIN!");

            if (winUI != null)
            {
                winUI.SetActive(true);
            }
        }

        public void TriggerLose()
        {
            if (currentState != GameState.Playing) return;

            currentState = GameState.Lost;
            Debug.Log("YOU LOSE!");

            if (loseUI != null)
            {
                loseUI.SetActive(true);
            }
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void NextLevel()
        {
            currentLevelId++;
            LoadLevel(currentLevelId);
            currentState = GameState.Playing;
            
            if (winUI != null) winUI.SetActive(false);
        }
        */
    }


    public enum GameState
    {
        Playing,
        Won,
        Lost
    }
}
