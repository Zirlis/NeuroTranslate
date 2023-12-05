using UnityEngine;


namespace NeuroTranslate
{
    [CreateAssetMenu(fileName = "LoadingPanelSettings", menuName = "ScriptableObjects/LoadingPanelSettings")]
    public sealed class LoadingPanelSettings : ScriptableObject
    {
        [SerializeField] private int _maxDisplayedLoading;
        [SerializeField] private float _indentionFromEdge;
        [SerializeField] private float _indentionFromTop;
        [SerializeField] private float _indentionFromBottom;
        [SerializeField] private float _distanceBetweenLoading;

        public int MaxDisplayedLoading => _maxDisplayedLoading;
        public float IndentionFromEdge => _indentionFromEdge;
        public float IndentionFromTop => _indentionFromTop;
        public float IndentionFromBottom => _indentionFromBottom;
        public float DistanceBetweenLoading => _distanceBetweenLoading;
    }
}