using UnityEngine;


namespace NeuroTranslate
{
    public sealed class MultiTouchEnabled : MonoBehaviour
    {
        void Start()
        {
            Input.multiTouchEnabled = false;
        }
    }
}