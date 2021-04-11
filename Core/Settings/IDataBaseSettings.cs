using JetBrains.Annotations;

namespace Core.Settings
{
    [UsedImplicitly]
    public interface IDataBaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}