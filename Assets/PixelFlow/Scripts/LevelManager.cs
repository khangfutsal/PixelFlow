using PixelFlow;
using PixelFlow.Data;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private LevelConfigData data;
    [SerializeField] private List<Gem> gems;

    [SerializeField] private int totalGems;
    [SerializeField] private int destroyedGems;

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


    private void Start()
    {
        totalGems = gems.Count;
        destroyedGems = 0;
    }

    public void SpawnLevel()
    {

    }

    public void OnGemDestroy(Gem gem)
    {
        destroyedGems++;
        Debug.Log($"Gem destroyed: {destroyedGems}/{totalGems}");

        // Check win condition
        if (destroyedGems >= totalGems)
        {
            GameManager.Instance.TriggerWin();
        }
    }

}
