using Core.IoC;
using JetBrains.Annotations;

namespace Core.Settings
{
    [PutInIoC, UsedImplicitly]
    public sealed class ConfigurationHelper : IConfigurationHelper
    {
        public string GetDbHost()
        {
            #if DEBUG
                return "mongodb://localhost:27017";
            #else
                return "mongodb+srv://alexzonic:Flatronw22@cluster0.vvwvz.mongodb.net/MessengerDB?retryWrites=true&w=majority";
            #endif
        }

        public string GetDbName() => "MessengerDB";
    }
}