using Core.IoC;
using JetBrains.Annotations;
using StructureMap;

namespace Api
{
    [UsedImplicitly]
    public sealed class ApiRegistry : Registry
    {
        public ApiRegistry()
        {
            Scan(s =>
            {
                s.TheCallingAssembly();
                s.Convention<PutInIoCScanner<ApiRegistry>>();
            });
        }
    }
}