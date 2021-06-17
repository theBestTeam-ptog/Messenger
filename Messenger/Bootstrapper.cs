using Api;
using Core;
using DataAccess;
using JetBrains.Annotations;
using Messenger.ViewModels;
using Messenger.Views;
using StructureMap;

namespace Messenger
{
    [UsedImplicitly]
    public static class Bootstrapper
    {
        public static IContainer Container;

        public static void Init()
        {
            Container = new Container(x =>
            {
                x.AddRegistry<CoreRegistry>();
                x.AddRegistry<DataAccessRegistry>();
                x.AddRegistry<ApiRegistry>();
                x.For<ApplicationWindow>().Use<ApplicationWindow>();
                x.For<MainWindow>().Use<MainWindow>();
                x.ForSingletonOf<IDialogListViewModel>().Use<DialogListViewModel>();
                x.ForSingletonOf<ISearchResultViewModel>().Use<SearchResultViewModel>();
                //x.ForSingletonOf().Singleton();
            });
        }
    }
}