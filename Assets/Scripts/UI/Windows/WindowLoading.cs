using System;
using System.Collections;
using Events;
using TMPro;
using UnityEngine;
using Zenject;

namespace GunFly.UI
{
    public class WindowLoading : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup = null;
        [SerializeField] private TextMeshProUGUI _loadingPercentage = null;

        private GameEvents _gameEvents = null;

        [Inject]
        public void Construct(GameEvents gameEvents)
        {
            _gameEvents = gameEvents;
        }

        private void Start()
        {
            StartCoroutine(LoadGamePlay());
        }

        private IEnumerator LoadGamePlay()
        {
            for (float i = 0; i <= 1; i += 0.01f)
            {
                _loadingPercentage.text = $"{(i * 100f).Round(0)}%";
                yield return null;
            }

            for (float i = 0; i <= 1; i += 0.01f)
            {
                _canvasGroup.alpha = i;
                yield return null;
            }

            _gameEvents.OnGameLoaded?.Invoke();
        }
    }
}