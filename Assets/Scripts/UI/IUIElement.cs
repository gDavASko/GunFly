using UnityEngine;

public interface IUIElement
{
    string Id { get; }
    RectTransform RectTransform { get; }

    void ShowWithParams(params object[] parameters);
    void Hide(bool force);
}