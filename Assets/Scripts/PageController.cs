using System.Collections;
using UnityEngine;


namespace NeuroTranslate
{
    public sealed class PageController : MonoBehaviour
    {
        #region Fields
        [SerializeField] private PageIndicator _pageIndicator;
        [SerializeField] private GameObject _plug;
        
        private PageSettings _pageSettings;

        private RectTransform _pages;
        private int _pagesCount;
        private int _currentPage;

        private float _screenWidth;
        #endregion

        public int PagesCount => _pagesCount;
                
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(PageMovement());
            }
        }

        public void PageControllerMain()
        {
            _pages = GetComponent<RectTransform>();
            _pagesCount = _pages.childCount;
            _screenWidth = Screen.width;

            if (_pagesCount > 0)
            {
                for (int i = 0; i < _pagesCount; i++)
                {
                    var page = _pages.GetChild(i);
                    page.GetComponent<RectTransform>().anchoredPosition = new Vector2(_screenWidth * i *
                        _pageSettings.DistanceBetweebPages, 0);
                    page.gameObject.SetActive(true);
                }
            }
        }

        public void DefiningResources(PageSettings pageSettings)
        {
            if (pageSettings != null)
            {
                _pageSettings = pageSettings;
            }
            else
            {
                Debug.LogError("PageSettings == null");
            }
        }

        public void NextPage()
        {
            _currentPage++;
            _pageIndicator.SelectPage(_currentPage);
        }

        public void PreviousPage()
        {
            _currentPage--;
            _pageIndicator.SelectPage(_currentPage);
        }

        private IEnumerator PageMovement()
        {
            var startMousePosition = Input.mousePosition.x;
            float mouseDistance;

            do
            {
                mouseDistance = Input.mousePosition.x - startMousePosition;

                if (mouseDistance > _pageSettings.DeadZone * _screenWidth || mouseDistance < -_pageSettings.DeadZone * _screenWidth)
                {
                    if (_currentPage == 0  && mouseDistance > 0|| _currentPage == _pagesCount - 1 && mouseDistance < 0)
                    {
                        float distanceOnEdge = mouseDistance > 0 ? Mathf.Sqrt(mouseDistance) : -Mathf.Sqrt(-mouseDistance);
                        _pages.anchoredPosition = new Vector2(-_screenWidth * _currentPage * _pageSettings.DistanceBetweebPages +
                            distanceOnEdge, 0);
                    }
                    else
                    {
                        _pages.anchoredPosition = new Vector2(-_screenWidth * _currentPage * _pageSettings.DistanceBetweebPages +
                            mouseDistance, 0);
                    }                    

                    _plug.SetActive(true);
                }

                yield return null;
            } while (!Input.GetMouseButtonUp(0));

            if (mouseDistance < _screenWidth * -_pageSettings.DistanceForTurningThePage && _currentPage + 1 < _pagesCount)
            {                
                NextPage();
            }
            else if (mouseDistance > _screenWidth * _pageSettings.DistanceForTurningThePage && _currentPage > 0)
            {                
                PreviousPage();
            }

            StartCoroutine(PagesGoToPageCountPosition());
        }

        private IEnumerator PagesGoToPageCountPosition()
        {
            float currentPosition = _pages.anchoredPosition.x;
            float desiredPosition = -_screenWidth * _currentPage * _pageSettings.DistanceBetweebPages;
            float speed = (currentPosition - desiredPosition) / _pageSettings.TimeForTurning;

            if (currentPosition > desiredPosition)
            {
                while (currentPosition > desiredPosition)
                {
                    currentPosition -= speed * Time.deltaTime;
                    _pages.anchoredPosition -= new Vector2(speed * Time.deltaTime, 0);

                    if (currentPosition < desiredPosition)
                    {
                        _pages.anchoredPosition = new Vector2(desiredPosition, 0);
                    }

                    yield return null;
                }
            }           
            else if (currentPosition < desiredPosition)
            {
                while (currentPosition < desiredPosition)
                {
                    currentPosition -= speed * Time.deltaTime;
                    _pages.anchoredPosition -= new Vector2(speed * Time.deltaTime, 0);
                    
                    if (currentPosition > desiredPosition)
                    {
                        _pages.anchoredPosition = new Vector2(desiredPosition, 0);
                    }

                    yield return null;
                }
            }

            _plug.SetActive(false);
        }
    }
}