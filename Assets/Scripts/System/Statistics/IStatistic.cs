
public interface IStatistic<T>
{
   void Load();
   void Save();
   T GetValue(SaveKey key, T defaultVal);
   void SetValue(SaveKey key, T value);
}