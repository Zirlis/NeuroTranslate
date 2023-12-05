using UnityEngine;


namespace NeuroTranslate
{
    public sealed class MainSceneController : MonoBehaviour
    {
        #region Fields

        #region Resources

        private GameObject _pageIndicatorGameObject;
        private GameObject _loadingGameObject;
        private PageSettings _pageSettings;
        private PageIndicatorSettings _pageIndicatorSettings;
        private FlagTokensSettings _flagTokensSettings;
        private LoadingPanelSettings _loadingPanelSettings;
        private LocaleTogglesSettings _localeTogglesSettings;
        #endregion


        #region Scripts

        [SerializeField] private PageController _pageController;
        [SerializeField] private LanguageToggle[] _languageToggles;
        private PageIndicator _pageIndicator;
        [SerializeField] private LoadingPanel _loadingPanel;
        [SerializeField] private LocaleTogglesSort _localeTogglesSort;
        #endregion


        [SerializeField] private Transform PageIndicatorParent;
        #endregion

        private void Awake()
        {
            _pageIndicator = GetComponent<PageIndicator>();

            LoadResources();
            _pageController.PageControllerMain();
            _pageIndicator.AddIndicators(_pageIndicatorGameObject, _pageIndicatorSettings, _pageController.PagesCount, PageIndicatorParent);
            _loadingPanel.CalculateHeigthOnPanel();
            _localeTogglesSort.Sort();
        }

        private void LoadResources()
        {
            _pageIndicatorGameObject = Resources.Load<GameObject>("Prefabs/PageIndicator");
            _loadingGameObject = Resources.Load<GameObject>("Prefabs/LoadingPanel");

            _pageSettings = Resources.Load<PageSettings>("ScriptableObjects/PageSettings");
            _pageIndicatorSettings = Resources.Load<PageIndicatorSettings>("ScriptableObjects/PageIndicatorSettings");
            _flagTokensSettings = Resources.Load<FlagTokensSettings>("ScriptableObjects/FlagTokensSettings");
            _loadingPanelSettings = Resources.Load<LoadingPanelSettings>("ScriptableObjects/LoadingPanelSettings");
            _localeTogglesSettings = Resources.Load<LocaleTogglesSettings>("ScriptableObjects/LocaleTogglesSettings");

            ShareResources();
        }

        private void ShareResources()
        {
            _pageController.DefiningResources(_pageSettings);
            _loadingPanel.DefiningResources(_loadingPanelSettings, _loadingGameObject);
            _localeTogglesSort.DefiningResources(_localeTogglesSettings);

            foreach (LanguageToggle languageToggle in _languageToggles)
            {
                languageToggle.DefiningFlagTokensSettings(_flagTokensSettings);
            }
        }
    }
}