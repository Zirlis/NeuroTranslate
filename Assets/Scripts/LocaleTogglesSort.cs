using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace NeuroTranslate
{
    public sealed class LocaleTogglesSort : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TextBox _textBox;

        private LocaleTogglesSettings _localeTogglesSettings;
        private RectTransform[] _toggleRectTransform;
        private List<GameObject> _localeToggles = new List<GameObject>();
        private ToggleGroup _toggleGroup;
        private Transform _toggleGroupTransform;
        #endregion

        private void Awake()
        {
            _toggleGroup = GetComponent<ToggleGroup>();
            _toggleGroupTransform = GetComponent<Transform>();
        }

        public void SpawmToggles()
        {
            foreach (var toggle in _localeToggles)
            {
                toggle.GetComponent<LocaleSelector>().SetTextBox(_textBox);
                toggle.GetComponent<Toggle>().group = _toggleGroup;
                Instantiate(toggle, _toggleGroupTransform);
            }

            _localeToggles.Clear();
            Sort();
        }

        public void Sort()
        {
            var transform = GetComponent<Transform>();
            int togglesCount = transform.childCount;
            _toggleRectTransform = new RectTransform[togglesCount];

            for (int i = 0; i < togglesCount; i++)
            {
                _toggleRectTransform[i] = transform.GetChild(i).GetComponent<RectTransform>();
            }

            var width = Screen.width;
            var height = Screen.height;

            float toggleWidth = (1 - 2 * _localeTogglesSettings.IndentionFromEdge) /
                (togglesCount + (togglesCount - 1) * _localeTogglesSettings.DistanceBetweenToggles);
            float toggleHeight = 1 - _localeTogglesSettings.IndentionFromTop - _localeTogglesSettings.IndentionFromBottom;
            var toggleSize = toggleWidth  * width < toggleHeight * height ? toggleWidth * width : toggleHeight * height;

            for (int i = 0; i < togglesCount; i++)
            {
                _toggleRectTransform[i].anchorMin = new Vector2(_localeTogglesSettings.IndentionFromEdge + toggleSize / width * i *
                    (1 + _localeTogglesSettings.DistanceBetweenToggles), 1 - _localeTogglesSettings.IndentionFromTop - toggleSize / height);
                _toggleRectTransform[i].anchorMax = new Vector2(_localeTogglesSettings.IndentionFromEdge + toggleSize / width * i *
                    (1 + _localeTogglesSettings.DistanceBetweenToggles) + toggleSize / width, 1 - _localeTogglesSettings.IndentionFromTop);
                _toggleRectTransform[i].anchoredPosition = Vector2.zero;
                _toggleRectTransform[i].gameObject.SetActive(true);
            }

        }

        public void DefiningResources(LocaleTogglesSettings localeTogglesSettings)
        {
            if (localeTogglesSettings != null)
            {
                _localeTogglesSettings = localeTogglesSettings;
            }
            else
            {
                Debug.LogError("LocaleTogglesSettings == null");
            }
        }

        public void AddToToggleList(GameObject localeToggle)
        {
            if (localeToggle != null)
            {
                _localeToggles.Add(localeToggle);
            }
            else
            {
                Debug.LogError("LocaleToggle == null");
            }
        }

        public void RemoveFromToggleList(GameObject localeToggle)
        {
            if (localeToggle != null)
            {                
                if (_localeToggles.Contains(localeToggle))
                {
                    _localeToggles.Remove(localeToggle);
                }
            }
            else
            {
                Debug.LogError("LocaleToggle == null");
            }
        }
    }
}