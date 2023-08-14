using System;
using Events;
using Saves;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIElementBase : MonoBehaviour, IUIElement
{
    //ToDo: Move sounds to sound event system
    private IAudioPlayer _soundShow = null;

    protected IUIElementAnimator _animator = null;

    public abstract string Id { get; }

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

    protected virtual void Awake()
    {
        _animator = GetComponent<IUIElementAnimator>();
    }

    public virtual void ShowWithParams(params object[] parameters)
    {
        Show();
    }

    public virtual void Show(bool force = false)
    {
        _soundShow?.PlaySound();

        if (!force && _animator != null)
        {
            _animator.Animate("Show", null);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public virtual void Hide(bool force = false)
    {
        if (!force && _animator != null)
        {
            _animator.Animate("Hide", (string id) =>
            {
                if (id == "Hide")
                {
                    gameObject.SetActive(false);
                }
            });
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}