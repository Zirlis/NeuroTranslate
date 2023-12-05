using UnityEngine;


namespace NeuroTranslate
{
    public sealed class NextText : ButtonCore
    {
        [SerializeField] private TextBox _textBox;

        protected override void ButtonMethod()
        {
            _textBox.NextText();
        }
    }
}