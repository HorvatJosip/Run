namespace Run.Core
{
    public interface IConfigurationService
    {
        string Get(string key);
        T Get<T>(string key);

        void Set(string key, object value);
    }
}