using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;


namespace NeuroTranslate
{
    public sealed class TextBox : MonoBehaviour
    {
        #region Fields

        private const string DEFAULT_LANGUAGE = "russian";

        private StringTable _stringTable;
        private string _language;
        [SerializeField] private LocalizeStringEvent _localizeStringEvent;
        private TableReference _tableReference;

        [SerializeField] private string[] _keys;
        private int _currentPage;

        [SerializeField] private Translate _translate;

        [SerializeField] private GameObject _turningToLeft;
        [SerializeField] private GameObject _turningToRight;
        [SerializeField] private GameObject _deleteButton;
        #endregion

        public string[] Keys => _keys;
        public int CurrentPage => _currentPage;

        public void Start()
        {
            ChangeReference();
            DisableButtons();
        }

        public void NextText()
        {
            if (_currentPage + 1 < _keys.Length)
            {
                _currentPage++;
                ChangeReference();
                DisableButtons();
            }
        }

        public void PreviousText()
        {
            if (_currentPage > 0)
            {
                _currentPage--;
                ChangeReference();
                DisableButtons();
            }
        }

        public void DeleteText()
        {
            _translate.AddRequest(_stringTable, _keys[_currentPage], _language);
            _localizeStringEvent.RefreshString();
        }

        public void SetLocale(Locale locale, string language)
        {
            if (locale == LocalizationSettings.AvailableLocales.Locales[0])
            {
                _deleteButton.SetActive(false);
            }
            else
            {
                _deleteButton.SetActive(true);
            }

            if (_tableReference == null)
            {
                _tableReference = _localizeStringEvent.StringReference.TableReference;
            }

            _stringTable = LocalizationSettings.StringDatabase.GetTable(_tableReference, locale);
            _language = language;
        }

        private void ChangeReference()
        {
            _localizeStringEvent.StringReference.SetReference(_localizeStringEvent.StringReference.TableReference, _keys[_currentPage]);
        }

        private void DisableButtons()
        {
            if (_currentPage == 0)
            {
                _turningToLeft.SetActive(false);
            }
            else
            {
                _turningToLeft.SetActive(true);
            }

            if (_currentPage == _keys.Length - 1)
            {
                _turningToRight.SetActive(false);
            }
            else
            {
                _turningToRight.SetActive(true);
            }
        }
    }
}