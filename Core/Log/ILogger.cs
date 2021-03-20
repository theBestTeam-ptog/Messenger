namespace Core.Log
{
    public interface ILogger<TLocator> where TLocator: class
    {
        void Info(string message);
        void Error(string message);
    }
}