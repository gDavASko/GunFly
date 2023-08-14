using Events;
using Saves;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WindowBattle : UIElementBase
{
    [SerializeField] private TextMeshProUGUI _textLevel = null;
    [SerializeField] private TextMeshProUGUI _textHitPointPercent = null;
    [SerializeField] private Slider _sliderHitPoints = null;
    [SerializeField] private TextMeshProUGUI _textScores = null;

    private IStorableParams _storableParams = null;
    private IUnit _playerUnit = null;
    private IHitPointSystem _playerHPSystem = null;

    public override string Id => "WindowBattle";

    public override void ShowWithParams(params object[] parameters)
    {
        _storableParams = parameters.Get<IStorableParams>();
        _storableParams.OnValueChanged += OnValueChanges;

        _playerUnit = parameters.Get<IUnit>();
        _playerHPSystem = _playerUnit.Transform.GetComponent<IHitPointSystem>();
        _playerHPSystem.OnHPChanged += UpdateHitPoints;

        UpdateAllValues();

        base.ShowWithParams(parameters);
    }

    private void UpdateAllValues()
    {
        _textLevel.text = (_storableParams.Get(SaveKey.LevelNumber, 0) + 1).ToString();
        _textScores.text = _storableParams.Get(SaveKey.Scores, 0).ToString();

        ResetHP();
    }

    private void OnValueChanges(SaveKey key)
    {
        switch (key)
        {
            case SaveKey.Scores:
                _textScores.text = _storableParams.Get(key, 0).ToString();
                break;
        }
    }

    private void UpdateHitPoints(float delataChange = 0)
    {
        float value = _playerHPSystem.HitPointPercent;
        _textHitPointPercent.text = $"{(value * 100).Round(0)}%";
        _sliderHitPoints.value = value;
    }

    private void ResetHP()
    {
        _textHitPointPercent.text = $"100%";
        _sliderHitPoints.value = 1f;
    }
}