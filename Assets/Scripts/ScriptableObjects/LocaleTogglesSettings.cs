using UnityEngine;


namespace NeuroTranslate
{
    [CreateAssetMenu(fileName = "LocaleTogglesSettings", menuName = "ScriptableObjects/LocaleTogglesSettings")]
    public sealed class LocaleTogglesSettings : ScriptableObject
    {
        [SerializeField] private float _indentionFromEdge;
        [SerializeField] private float _indentionFromTop;
        [SerializeField] private float _indentionFromBottom;
        [SerializeField] private float _distanceBetweenToggles;

        public float IndentionFromEdge => _indentionFromEdge;
        public float IndentionFromTop => _indentionFromTop;
        public float IndentionFromBottom => _indentionFromBottom;
        public float DistanceBetweenToggles => _distanceBetweenToggles;
    }
}