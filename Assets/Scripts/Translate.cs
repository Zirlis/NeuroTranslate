using UnityEngine;
using UnityEngine.Localization.Tables;
using TMPro;
using OpenAI_API;
using System;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;


namespace NeuroTranslate
{
    public sealed class Translate : MonoBehaviour
    {
        #region Fields

        [SerializeField] private LocalizeStringEvent _localizeStringEvent;

        private const int DELAY = 20000;
        private const string TRANSLATE_BEGIN_MESSAGE = "-Перевод в процессе-";
        private const string ERROR_MESSAGE = "-Ошибка перевода-";
        private const string ADD_IN_QUEUE_MESSAGE = "-Отправленно на повторный перевод-";

        private StringTable _ruStringTable;
        private TableReference _tableReference;
        private string[] _keys;
        private string[] _originalText;

        private bool _atWork;

        private List<Queue> _transferQueue = new List<Queue>();
        private OpenAIAPI _api = new OpenAIAPI("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");
        #endregion

        public void AddLanguages(string[] languages, int[] localeCode, TextMeshProUGUI[] progress)
        {
            _tableReference = _localizeStringEvent.StringReference.TableReference;
            _ruStringTable = LocalizationSettings.StringDatabase.
                GetTable(_tableReference, LocalizationSettings.AvailableLocales.Locales[0]);

            _keys = new string[_ruStringTable.SharedData.Entries.Count];
            _originalText = new string[_ruStringTable.SharedData.Entries.Count];
            for (int i = 0; i < _ruStringTable.SharedData.Entries.Count; i++)
            {
                _keys[i] = _ruStringTable.SharedData.Entries[i].Key;
                _originalText[i] = _ruStringTable.GetEntry(_keys[i]).GetLocalizedString();
            }

            for (int i = 0; i < localeCode.Length; i++)
            {
                var stringTable = LocalizationSettings.StringDatabase.
                        GetTable(_tableReference, LocalizationSettings.AvailableLocales.Locales[localeCode[i]]);
                foreach (var key in _keys)
                {
                    stringTable.AddEntry(key, TRANSLATE_BEGIN_MESSAGE);
                }
            }

            _localizeStringEvent.RefreshString();

            for (int i = 0; i < progress.Length; i++)
            {
                progress[i].SetText($"0/{_keys.Length}");
            }

            StartCoroutine(WaitFinishWork(languages, localeCode, progress));          
        }

        public void AddRequest(StringTable stringTable, string key, string language)
        {
            var localizedString = stringTable.GetEntry(key).GetLocalizedString();
            if (localizedString != TRANSLATE_BEGIN_MESSAGE && localizedString != ADD_IN_QUEUE_MESSAGE)
            {
                var queue = new Queue(stringTable, key, language);
                _transferQueue.Add(queue);

                stringTable.AddEntry(key, ADD_IN_QUEUE_MESSAGE);

                if (!_atWork)
                {
                    WorkWithQueue();
                }
            }
        }

        private IEnumerator WaitFinishWork(string[] languages, int[] localeCode, TextMeshProUGUI[] progress)
        {
            while (_atWork)
            {
                yield return null;
            }

            Entries(languages, localeCode, progress);
        }

        private async void Entries(string[] languages, int[] localeCode, TextMeshProUGUI[] progress)
        {
            _atWork = true;

            for (int i = 0; i < languages.Length; i++)
            {
                var stringTable = LocalizationSettings.StringDatabase.
                    GetTable(_tableReference, LocalizationSettings.AvailableLocales.Locales[localeCode[i]]);

                for (int j = 0; j < _originalText.Length; j++)
                {
                    if (localeCode[i] != 9 || j == 0)
                    {
                        long start = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                        string response = await ChatGPTTranslate(_originalText[j], languages[i]);
                        stringTable.AddEntry(_keys[j], response);
                        _localizeStringEvent.RefreshString();

                        progress[i].SetText($"{j + 1}/{_keys.Length}");

                        long end = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                        if (i < languages.Length - 1 || j < _originalText.Length - 1)
                        {
                            int delay = (int)(DELAY - (end - start) + 50);

                            if (delay < 50)
                            {
                                delay = 50;
                            }

                            await Task.Delay(delay);
                        }
                    }
                }
            }

            if (_transferQueue.Count > 0)
            {
                await Task.Delay(DELAY);
                WorkWithQueue();
            }
            else
            {
                _atWork = false;
            }
        }                

        private async void WorkWithQueue()
        {
            _atWork = true;

            var queue = new List<Queue>(_transferQueue);
            _transferQueue.Clear();
            for (int i = 0; i < queue.Count; i++)
            {
                long start = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                var originalText = _ruStringTable.GetEntry(queue[i].Key).GetLocalizedString();
                string response = await ChatGPTTranslate(originalText, queue[i].Language);
                queue[i].StringTable.AddEntry(queue[i].Key, response);
                _localizeStringEvent.RefreshString();

                long end = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                if (i < queue.Count - 1)
                {
                    int delay = (int)(DELAY - (end - start) + 50);

                    if (delay < 0)
                    {
                        delay = 50;
                    }

                    await Task.Delay(delay);
                }
            }

            if (_transferQueue.Count > 0)
            {
                await Task.Delay(DELAY);
                WorkWithQueue();
            }
            else
            {
                _atWork = false;
            }
        }

        private async Task<string> ChatGPTTranslate(string text, string language)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Translate the following text to {language}. Answer only with the bare resulting translated text. {text}");

            string question = sb.ToString();

            try
            {
                var chat = _api.Chat.CreateConversation();
                chat.AppendUserInput(question);
                string response = await chat.GetResponseFromChatbotAsync();

                return response;
            }
            catch (Exception)
            {
                await Task.Delay(DELAY);

                try
                {
                    var chat = _api.Chat.CreateConversation();
                    chat.AppendUserInput(question);
                    string response = await chat.GetResponseFromChatbotAsync();

                    return response;
                }
                catch (Exception)
                {
                    await Task.Delay(DELAY);
                    return ERROR_MESSAGE;
                }
            }
        }
    }
}