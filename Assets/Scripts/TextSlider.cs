using System.Collections;
using UnityEngine;
using TMPro;


namespace NeuroTranslate
{
    public sealed class TextSlider : MonoBehaviour
    {
        #region Fields
        [SerializeField] private string[] _text;
        [SerializeField] float _timeBetweenTurning;
        [SerializeField] float _timeBetweenCycle;

        private TextMeshProUGUI _textMeshProUGUI;
        #endregion

        private void Start()
        {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            StartCoroutine(Turning());
        }

        private IEnumerator Turning()
        {
            for (int i = 0; i < _text.Length; i++)
            {
                _textMeshProUGUI.SetText(_text[i]);
                if (i != _text.Length - 1)
                {
                    yield return new WaitForSeconds(_timeBetweenTurning);
                }
            }

            yield return new WaitForSeconds(_timeBetweenCycle);
            StartCoroutine(Turning());
        }
    }
}