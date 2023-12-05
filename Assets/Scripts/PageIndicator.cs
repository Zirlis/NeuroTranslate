using UnityEngine;
using UnityEngine.UI;


namespace NeuroTranslate
{
    public sealed class PageIndicator : MonoBehaviour
    {
        private RectTransform[] _indicatorsRectTransform;
        private Image[] _indicatorsImage;
        private PageIndicatorSettings _pageIndicatorSettings;
        private int _pagesCount;

        public void AddIndicators(GameObject _pageIndicatorGameObject, PageIndicatorSettings pageIndicatorSettings,
            int pagesCount, Transform parentTransform)
        {
            var parentRectTransform = parentTransform.GetComponent<RectTransform>();
            _pageIndicatorSettings = pageIndicatorSettings;
            _pagesCount = pagesCount;

            var screenWidth = Screen.width;
            float indicatorSize = screenWidth * (1 - 2 * pageIndicatorSettings.IndentionFromEdge) /
                (pageIndicatorSettings.MaxDisplayedPages + (pageIndicatorSettings.MaxDisplayedPages - 1) *
                pageIndicatorSettings.DistanceBetweenIndicators);

            _indicatorsRectTransform = new RectTransform[pagesCount];
            _indicatorsImage = new Image[pagesCount];

            for (int i = 0; i < pagesCount; i++)
            {
                _indicatorsRectTransform[i] = Instantiate(_pageIndicatorGameObject, parentTransform).GetComponent<RectTransform>();
                _indicatorsImage[i] = _indicatorsRectTransform[i].GetComponent<Image>();
            }

            for (int i = 0; i < pagesCount; i++)
            {
                _indicatorsRectTransform[i].anchoredPosition = new Vector2((pagesCount - 1 - i * 2) *
                    -(0.5f * indicatorSize + 0.5f * pageIndicatorSettings.IndentionFromEdge), 0);
            }

            SelectPage();
        }

        public void SelectPage(int index = 0)
        {
            _indicatorsRectTransform[index].localScale = new Vector2(1, 1);
            _indicatorsImage[index].color = Color.white;

            for (int i = 0; i < _pagesCount; i++)
            {
                if (i != index)
                {
                    _indicatorsRectTransform[i].localScale = 
                        new Vector2(_pageIndicatorSettings.NonselectedSize, _pageIndicatorSettings.NonselectedSize);
                    _indicatorsImage[i].color = _pageIndicatorSettings.Shadow;
                }
            }
        }
    }
}