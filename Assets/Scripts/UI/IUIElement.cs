using UnityEngine;

public interface IUIElement
{
    string Id { get; }
    RectTransform RectTransform { get; }

    void Init(params object[] parameters);
}