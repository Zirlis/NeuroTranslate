using UnityEngine;


namespace NeuroTranslate
{
    [CreateAssetMenu(fileName = "PageIndicatorSettings", menuName = "ScriptableObjects/PageIndicatorSettings")]
    public sealed class PageIndicatorSettings : ScriptableObject
    {
        [SerializeField] private int _maxDisplayedPages;
        [SerializeField] private float _distanceBetweenIndicators;
        [SerializeField] private float _indentionFromEdge;
        [SerializeField] private Color _shadow;
        [SerializeField] private float _nonselectedSize;

        public int MaxDisplayedPages => _maxDisplayedPages;
        public float DistanceBetweenIndicators => _distanceBetweenIndicators;
        public float IndentionFromEdge => _indentionFromEdge;
        public Color Shadow => _shadow;
        public float NonselectedSize => _nonselectedSize;
    }
}