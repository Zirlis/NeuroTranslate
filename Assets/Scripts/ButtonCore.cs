using UnityEngine;
using UnityEngine.UI;


namespace NeuroTranslate
{
    public abstract class ButtonCore : MonoBehaviour
    {
        private Button _button;

        protected virtual void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ButtonMethod);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ButtonMethod);
        }

        protected abstract void ButtonMethod();
    }
}