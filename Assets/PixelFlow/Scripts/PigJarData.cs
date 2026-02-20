using PixelFlow;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PigJarData
{
    public int id;
    public int count = 3; // Number of shots available
    public e_Color color;
    public bool isPlaying = false; // On conveyor and active
}
