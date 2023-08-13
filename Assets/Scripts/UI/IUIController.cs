
using Events;
using Saves;

public interface IUIController
{
    void Init(IUIFactory uiFactory, GameEvents gameEvents, IStorableParams _storableParams);
}