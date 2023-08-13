using System.Threading.Tasks;

public interface IUIFactory
{
    public Task<T> GetUIElement<T>(string id)
        where T : class, IUIElement;
}