using Core.IoC;
using DataAccess.Repositories;
using JetBrains.Annotations;
using StructureMap;

namespace DataAccess
{
    [UsedImplicitly]
    public sealed class DataAccessRegistry : Registry
    {
        public DataAccessRegistry()
        {
            Scan(s =>
            {
                s.TheCallingAssembly();
                s.Convention<PutInIoCScanner<DataAccessRegistry>>();
            });

            For<Repository>().Use<Repository>();
        }
    }
}