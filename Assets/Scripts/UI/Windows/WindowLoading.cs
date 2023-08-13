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

        private System.Action OnLevelLoaded = null;

        [Inject]
        public void Construct(GameEvents onLevelLoaded)
        {
            OnLevelLoaded = onLevelLoaded.OnGameLoaded;
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

            gameObject.SetActive(false);

            OnLevelLoaded?.Invoke();
        }
    }
}