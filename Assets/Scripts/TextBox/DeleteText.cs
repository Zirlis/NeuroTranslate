using UnityEngine;


namespace NeuroTranslate
{
    public class DeleteText : ButtonCore
    {
        [SerializeField] private TextBox _textBox;
        protected override void ButtonMethod()
        {
            _textBox.DeleteText();
        }
    }
}