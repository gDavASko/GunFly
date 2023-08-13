using System.Threading.Tasks;

public interface IDamageConfigAccessor
{
    Task<IDamageConfig> GetConfig(string id);
}