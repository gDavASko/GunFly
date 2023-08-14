using System.Collections.Generic;
using System.Threading.Tasks;

public class UIFactory : IUIFactory
{
    private IGettableAsset _assetGetter = null;
    private IWeaponFactory _weaponFactory = null;
    private Dictionary<string, IUIElement> _cachedElements = new Dictionary<string, IUIElement>();


    public UIFactory(IGettableAsset assetGetter)
    {
        _assetGetter = assetGetter;
    }

    private async Task<T> CreateUIElement<T>(string id) where T : class, IUIElement
    {
        T element = await _assetGetter.LoadResource<T>(id);
        _cachedElements[id] = element;

        return element as T;
    }


    public async Task<T> GetUIElement<T>(string id) where T : class, IUIElement
    {
        if (_cachedElements.TryGetValue(id, out IUIElement element))
        {
            return element as T;
        }
        else
        {
            var newElement = await CreateUIElement<T>(id);
            return newElement;
        }
    }
}