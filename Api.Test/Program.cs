using Core;
using Ninject;

namespace Api.Test
{
    internal class Program
    {
        private static IKernel _container;

        public virtual void Main()
        {
            _container = new StandardKernel(new Registry());

            _container.Bind<ConnectToDb>().ToSelf().InSingletonScope();
        }
    }
}