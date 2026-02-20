using UnityEngine;
using System.Collections.Generic;

namespace PixelFlow.Data
{
    [System.Serializable]
    public class GemData
    {
        public int id;
        public Vector3 position;
        public e_Color color;
    }

    [System.Serializable]
    public class PigJarData
    {
        public int id;
        public e_Color color;
        public int count;
    }

    [System.Serializable]
    public class SlotQueueData
    {
        public e_Color queueColor;
        public List<PigJarData> pigJars = new List<PigJarData>();
    }

    [System.Serializable]
    public class LevelRenderData
    {
        public int id;
        public List<SlotQueueData> slotQueues = new List<SlotQueueData>();
        public List<GemData> gems = new List<GemData>();
    }
}
