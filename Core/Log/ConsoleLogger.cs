using System;

namespace Core.Log
{
    public sealed class ConsoleLogger<T> : ILogger<T> where T : class
    {
        public void Info(string message)
        {
            Console.WriteLine($"{DateTime.Now} {typeof(T).Name}: {message}");
        }

        // todo мб потом реализуется
        public void Error(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}