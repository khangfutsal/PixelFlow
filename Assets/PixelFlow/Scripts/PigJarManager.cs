using PixelFlow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigJarManager : MonoBehaviour
{
    public static PigJarManager Instance { get; private set; }
    [SerializeField] private List<PigJar> pigJars;

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

    public PigJar GetPigJarByColor(e_Color color)
    {
        return pigJars.Find(p => p.GetData().color == color);
    }
}
