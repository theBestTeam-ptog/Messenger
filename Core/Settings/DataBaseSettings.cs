using Core.IoC;
using JetBrains.Annotations;

namespace Core.Settings
{
    [PutInIoC, UsedImplicitly]
    public class DataBaseSettings : IDataBaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}