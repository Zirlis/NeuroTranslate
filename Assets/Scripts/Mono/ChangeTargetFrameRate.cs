using UnityEngine;


namespace NeuroTranslate
{
    public  sealed class ChangeTargetFrameRate : MonoBehaviour
    {
        private const int FRAME_RATE = 60;

        void Start()
        {
            Application.targetFrameRate = FRAME_RATE;
        }
    }
}