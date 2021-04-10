using Core.IoC;
using Domain.Mappers;
using JetBrains.Annotations;
using StructureMap;

namespace Core
{
    [UsedImplicitly]
    public sealed class CoreRegistry : StructureMap.Registry
    {
        public CoreRegistry()
        {
            Scan(s =>
            {
                s.TheCallingAssembly();
                s.Convention<PutInIoCScanner<CoreRegistry>>();
            });

            For(typeof(IMapper<,>)).Singleton();
            For(typeof(IDuplexMapper<,>)).Singleton();
        }
    }
}