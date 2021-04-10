using JetBrains.Annotations;

namespace Core.Extensions
{
    [UsedImplicitly]
    public static class ObjectExtension
    {
        public static bool NotNull([CanBeNull] this object obj) => obj != null;
    }
}