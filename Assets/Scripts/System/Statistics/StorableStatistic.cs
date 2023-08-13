using Saves;

public class StorableStatistic<T> : IStatistic<T>
{
    private IStorableParams _storableParams = null;

    public StorableStatistic(IStorableParams storableParams)
    {
        _storableParams = storableParams;
    }

    public void SetValue(SaveKey key, T value)
    {
        _storableParams.Set(key, value);
    }

    public void Load()
    {
        _storableParams.Load();
    }

    public void Save()
    {
        _storableParams.Save();
    }

    public T GetValue(SaveKey key, T defaultVal)
    {
        return (T)_storableParams.Get(key, defaultVal);
    }
}