using UnityEngine;
using UnityEngine.UI;


namespace NeuroTranslate
{
    public class TranslateButton : ButtonCore
    {
        #region Fields

        [SerializeField] private LocaleTogglesSort _localeTogglesSort;
        [SerializeField] private LoadingPanel _loadingPanel;
        [SerializeField] private PageController _pageController;

        [SerializeField] private Toggle[] _languageToggles;
        [SerializeField] private Button _translateButton;
        #endregion

        private void Start()
        {
            SetInteractable(false);
        }

        protected override void ButtonMethod()
        {
            if (_loadingPanel.LanguageToggles.Count > 0)
            {
                _loadingPanel.AddLoadingPanels();
                _localeTogglesSort.SpawmToggles();                
                DisableButton();
            }
        }

        private void DisableButton()
        {
            SetInteractable(false);

            foreach (Toggle languageToggle in _languageToggles)
            {
                if (languageToggle.isOn)
                {
                    languageToggle.interactable = false;
                }
            }
        }

        public void SetInteractable(bool on)
        {
            _translateButton.interactable = on;
        }
    }
}