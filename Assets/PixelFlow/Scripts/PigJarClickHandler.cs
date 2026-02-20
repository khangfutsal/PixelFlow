using UnityEngine;

namespace PixelFlow
{
    /// <summary>
    /// Helper component to handle PigJar click interactions
    /// Attach this to PigJar GameObjects to make them clickable
    /// </summary>
    public class PigJarClickHandler : MonoBehaviour
    {
        [SerializeField] private PigJar pigJar;


        private void Awake()
        {
            pigJar = GetComponent<PigJar>();
        }

        private void OnMouseDown()
        {
            Debug.Log($"Test");
            if (!Conveyor.Instance.dispenser.CanDispense()) return;
            Debug.Log($"Test1");
            if (pigJar != null)
            {
                pigJar.OnClicked();
                Conveyor.Instance.AppendPigJar(pigJar);
            }
        }


    }
}
