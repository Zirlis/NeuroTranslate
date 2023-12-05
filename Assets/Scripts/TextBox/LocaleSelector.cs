using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;


namespace NeuroTranslate
{
    public sealed class LocaleSelector : MonoBehaviour
    {
        #region Fields

        [SerializeField] private string _localCode;
        [SerializeField] private TextBox _textBox;

        [SerializeField] private string _language;
        [SerializeField] private int _localID;
        [SerializeField] private Color _shadow;
        private Toggle _toggle;
        private Image _image;
        #endregion

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _image = GetComponent<Image>();

            ChangeLocal(_toggle.isOn);
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(ChangeLocal);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(ChangeLocal);
        }

        public void SetTextBox(TextBox textBox)
        {
            if (textBox != null)
            {
                _textBox = textBox;
            }
            else
            {
                Debug.LogError("TextBox == null");
            }
        }

        private void ChangeLocal(bool select)
        {

            if (select)
            {
                StartCoroutine(SetLocale());
                _image.color = Color.white;
                _toggle.interactable = false;
            }
            else
            {
                _image.color = _shadow;
                _toggle.interactable = true;
            }
        }       

        private IEnumerator SetLocale()
        {
            yield return LocalizationSettings.InitializationOperation;
            var locale = LocalizationSettings.AvailableLocales.Locales[_localID];
            LocalizationSettings.SelectedLocale = locale;
            _textBox.SetLocale(locale, _language);
        }
    }
}