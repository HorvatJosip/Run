using Run.Core;
using static Run.DesktopClient.Properties.Settings;

namespace Run.DesktopClient
{
    public class ConfigurationService : IConfigurationService
    {
        public string Get(string key) => Try.Get(() => (string)Default[key]);

        public T Get<T>(string key) => Try.Get(() => (T)Default[key]);

        public void Set(string key, object value)
        {
            if (Try.To(() => Default[key] = value))
                Default.Save();
        }
    }
}
