using UnityEngine;


namespace NeuroTranslate
{
    [CreateAssetMenu(fileName = "PageSettings", menuName = "ScriptableObjects/PageSettings")]
    public sealed class PageSettings : ScriptableObject
    {
        [SerializeField] private float _distanceBetweebPages;
        [SerializeField] private float _distanceForTurningThePage;
        [SerializeField] private float _timeForTurning;
        [SerializeField] private float _deadZone;

        public float DistanceBetweebPages => _distanceBetweebPages;
        public float DistanceForTurningThePage => _distanceForTurningThePage;
        public float TimeForTurning => _timeForTurning;
        public float DeadZone => _deadZone;
    }
}