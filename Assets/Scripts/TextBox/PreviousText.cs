using UnityEngine;


namespace NeuroTranslate
{
    public sealed class PreviousText : ButtonCore
    {
        [SerializeField] private TextBox _textBox;

        protected override void ButtonMethod()
        {
            _textBox.PreviousText();
        }
    }
}