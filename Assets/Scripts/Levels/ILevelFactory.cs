using System.Threading.Tasks;

public interface ILevelFactory
{
    public Task<T> CreateLevel<T>(string id) where T : class, ILevel;
}