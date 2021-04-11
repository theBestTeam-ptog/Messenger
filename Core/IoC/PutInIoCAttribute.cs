using System;
using JetBrains.Annotations;

namespace Core.IoC
{
    [UsedImplicitly]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PutInIoCAttribute : Attribute
    {
    }
}