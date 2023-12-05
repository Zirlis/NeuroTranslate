using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace NeuroTranslate
{
    public sealed class LoadingPanel : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TranslateButton _translateButton;
        [SerializeField] private Translate _translate;

        private LoadingPanelSettings _loadingPanelSettings;        

        private GameObject _loadingGameObject;
        private Transform _parent;
        private float _heigthOnPanel;

        private RectTransform[] _loadingPanels;
        private TextMeshProUGUI[] _loadingProgress;

        private List<LanguageToggle> _languageToggles = new List<LanguageToggle>();

        private int _languagesBeenAdded;

        [SerializeField] private TextMeshProUGUI _fakeTMPro;
        #endregion

        public List<LanguageToggle> LanguageToggles => _languageToggles;


        private void Awake()
        {
            _parent = GetComponent<Transform>();
        }

        public void CalculateHeigthOnPanel()
        {
            _heigthOnPanel = (1 - _loadingPanelSettings.IndentionFromTop - _loadingPanelSettings.IndentionFromBottom) /
                (_loadingPanelSettings.MaxDisplayedLoading + (_loadingPanelSettings.MaxDisplayedLoading - 1) *
                _loadingPanelSettings.DistanceBetweenLoading);
        }

        public void AddLoadingPanels()
        {
            _loadingPanels = new RectTransform[_languageToggles.Count];
            _loadingProgress = new TextMeshProUGUI[_languageToggles.Count];

            for (int i = 0; i < _languageToggles.Count; i++)
            {
                _loadingPanels[i] = Instantiate(_loadingGameObject, _parent).GetComponent<RectTransform>();
                _loadingPanels[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(_languageToggles[i].LanguageText);
                _loadingProgress[i] = _loadingPanels[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            }

            for (int i = 0; i < _loadingPanels.Length; i++)
            {
                _loadingPanels[i].anchorMin = new Vector2(_loadingPanelSettings.IndentionFromEdge, 
                    1 - _loadingPanelSettings.IndentionFromTop - _heigthOnPanel * (i + 1 + _languagesBeenAdded) -
                    _loadingPanelSettings.DistanceBetweenLoading * (i + _languagesBeenAdded));

                _loadingPanels[i].anchorMax = new Vector2(1 - _loadingPanelSettings.IndentionFromEdge,
                    1 - _loadingPanelSettings.IndentionFromTop - _heigthOnPanel * (i + _languagesBeenAdded) -
                    _loadingPanelSettings.DistanceBetweenLoading * (i + _languagesBeenAdded));

                _loadingPanels[i].anchoredPosition = Vector2.zero;
            }

            var progress = new TextMeshProUGUI[_loadingPanels.Length + 1]; 
            var languages = new string[_languageToggles.Count + 1];
            var localeCode = new int[_languageToggles.Count + 1];

            /// <summary>
            /// Ниже творятся страшные вещи в виду того, что данный функционал предназначен для едитора
            /// и на андроиде без костылей нормально не функцианирует
            /// </summary>

            if (_loadingPanels.Length == _languageToggles.Count)
            {
                progress[0] = _fakeTMPro;
                languages[0] = "ukrainian";
                localeCode[0] = 9;

                for (int i = 1; i < _loadingPanels.Length + 1; i++)
                {
                    progress[i] = _loadingPanels[i - 1].GetChild(1).GetComponent<TextMeshProUGUI>();
                    languages[i] = _languageToggles[i - 1].Language;
                    localeCode[i] = _languageToggles[i - 1].LocaleCode;
                }
            }
            else
            {
                Debug.LogError(_loadingPanels.Length != _languageToggles.Count);
            }

            _translate.AddLanguages(languages, localeCode, progress);
            _languagesBeenAdded += _languageToggles.Count;
            _languageToggles.Clear();
        }

        public void DefiningResources(LoadingPanelSettings loadingPanelSettings, GameObject loadingGameObject)
        {
            if (loadingPanelSettings != null)
            {
                _loadingPanelSettings = loadingPanelSettings;
            }
            else
            {
                Debug.LogError("LoadingPanelSettings == mull");
            }

            if (loadingGameObject != null)
            {
                _loadingGameObject = loadingGameObject;
            }
            else
            {
                Debug.LogError("LoadingGameObject== null");
            }
        }

        public void AddToLanguagesList(LanguageToggle languageToggle)
        {
            if (languageToggle != null)
            {
                _languageToggles.Add(languageToggle);
                _translateButton.SetInteractable(true);
            }
            else
            {
                Debug.LogError("LanguageToggle == null");
            }
        }

        public void RemoveFromLanguagesList(LanguageToggle languageToggle)
        {
            if (languageToggle != null)
            {
                if (_languageToggles.Contains(languageToggle))
                {
                    _languageToggles.Remove(languageToggle);
                    if (_languageToggles.Count == 0)
                    {
                        _translateButton.SetInteractable(false);
                    }
                }
            }
            else
            {
                Debug.LogError("LanguageToggle == null");
            }
        }
    }
}