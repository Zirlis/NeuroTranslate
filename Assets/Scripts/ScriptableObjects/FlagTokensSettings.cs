using UnityEngine;


namespace NeuroTranslate
{
    [CreateAssetMenu(fileName = "FlagTokensSettings", menuName = "ScriptableObjects/FlagTokensSettings")]
    public sealed class FlagTokensSettings : ScriptableObject
    {
        [SerializeField] private Color _shadowColor;

        public Color ShadowColor => _shadowColor;
    }
}