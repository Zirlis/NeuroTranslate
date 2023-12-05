using UnityEngine;
using UnityEngine.UI;


namespace NeuroTranslate
{
    public sealed class LanguageToggle : MonoBehaviour
    {
        private Toggle _toggle;
        private Image _image;
        private FlagTokensSettings _flagTokensSettings;

        [SerializeField] private LoadingPanel _loadingPanel;
        [SerializeField] private LocaleTogglesSort _localeTogglesSort;
        [SerializeField] GameObject _localeToggle;

        [SerializeField] private string _language;
        [SerializeField] private string _languageText;
        [SerializeField] private int _localeCode;

        public string Language => _language;
        public string LanguageText => _languageText;
        public int LocaleCode => _localeCode;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _image = gameObject.transform.GetChild(0).GetComponent<Image>();
        }

        private void Start()
        {            
            _toggle.isOn = false;
            _image.color = _flagTokensSettings.ShadowColor;
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(AddLanguage);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(AddLanguage);
        }

        private void AddLanguage(bool select)
        {
            if(select)
            {
                _loadingPanel.AddToLanguagesList(this);
                _localeTogglesSort.AddToToggleList(_localeToggle);
                _image.color = Color.white;
            }
            else
            {
                _loadingPanel.RemoveFromLanguagesList(this);
                _localeTogglesSort.RemoveFromToggleList(_localeToggle);
                _image.color = _flagTokensSettings.ShadowColor;
            }
        }

        public void DefiningFlagTokensSettings(FlagTokensSettings flagTokensSettings)
        {
            if (flagTokensSettings != null)
            {
                _flagTokensSettings = flagTokensSettings;
            }
            else
            {
                Debug.LogError($"FlagTokensSettings in {_language[0]} token == null");
            }
        }
    }
}