using System;
using System.Linq;
using System.Reflection;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;
using Core.Extensions;
using JetBrains.Annotations;

namespace Core.IoC
{
    [UsedImplicitly]
    public sealed class PutInIoCScanner<TLocator> : IRegistrationConvention
    {
        public void ScanTypes(TypeSet types, StructureMap.Registry registry)
        {
            var nonPublicClasses = typeof(TLocator).Assembly.GetTypes().Where(WithCustomAttribute);

            foreach (var nonPublicClass in nonPublicClasses)
            {
                foreach (var @interface in nonPublicClass.GetInterfaces())
                {
                    registry.For(@interface).Use(nonPublicClass);
                }
            }
        }

        private static bool WithCustomAttribute([System.Diagnostics.CodeAnalysis.NotNull] Type type) => 
            type.GetCustomAttribute<PutInIoCAttribute>().NotNull();
    }
}