using System.Collections;
using System.Collections.Generic;
using Saves;
using TMPro;
using UnityEngine;

public class StatisticDrawer : MonoBehaviour, IUIElement
{
    [SerializeField] private TextMeshProUGUI _textGames = null;
    [SerializeField] private TextMeshProUGUI _textVictories = null;
    [SerializeField] private TextMeshProUGUI _textScores = null;

    private IStorableParams _storableParams = null;

    public string Id => "StatisticDrawer";

    private RectTransform _rectTransform = null;
    public RectTransform RectTransform
    {
        get
        {
            if (_rectTransform == null)
                _rectTransform = transform.GetComponent<RectTransform>();

            return _rectTransform;
        }
    }

    public void Init(params object[] parameters)
    {
        _storableParams = parameters.Get<IStorableParams>();
        _storableParams.OnValueChanged += OnValueChanges;

        UpdateAllValues();
    }

    private void UpdateAllValues()
    {
        _textGames.text = _storableParams.Get(SaveKey.Sessions, 0).ToString();
        _textVictories.text = _storableParams.Get(SaveKey.Victories, 0).ToString();
        _textScores.text = _storableParams.Get(SaveKey.Credits, 0).ToString();
    }

    private void OnValueChanges(SaveKey key)
    {
        switch (key)
        {
            case SaveKey.Sessions:
                _textGames.text = _storableParams.Get(key, 0).ToString();
                break;

            case SaveKey.Victories:
                _textVictories.text = _storableParams.Get(key, 0).ToString();
                break;

            case SaveKey.Credits:
                _textScores.text = _storableParams.Get(key, 0).ToString();
                break;
        }
    }
}