using System;
using System.Collections;
using Events;
using TMPro;
using UnityEngine;

namespace GunFly.UI
{
    public class WindowLoading : UIElementBase
    {
        [SerializeField] private TextMeshProUGUI _loadingPercentage = null;

        private GameEvents _gameEvents = null;

        public override string Id => "WindowLoading";

        private void StartWindow()
        {
            StartCoroutine(LoadGamePlay());
        }

        private IEnumerator LoadGamePlay()
        {
            for (float i = 0; i <= 1; i += 0.01f)
            {
                _loadingPercentage.text = $"{(i * 100f).Round(0)}%";
                yield return new WaitForEndOfFrame();
            }

            _gameEvents.OnGameLoaded?.Invoke();
        }

        public override void ShowWithParams(params object[] parameters)
        {
            _gameEvents = parameters.Get<GameEvents>();
            base.ShowWithParams(parameters);
            StartWindow();
        }
    }
}