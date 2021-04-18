using Api;
using Core;
using DataAccess;
using DataAccess.Mappers;
using JetBrains.Annotations;
using Messenger.Pages;
using Messenger.ViewModels;
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
                x.For<Authorization>().Use<Authorization>().Singleton();
                x.For<Registration>().Use<Registration>().Singleton();
                x.For<ApplicationWindow>().Use<ApplicationWindow>();
                x.For<MainWindow>().Use<MainWindow>();
                x.ForSingletonOf<IDialogListViewModel>().Use<DialogListViewModel>();
                x.ForSingletonOf<ISearchResultViewModel>().Use<SearchResultViewModel>();
                //x.ForSingletonOf().Singleton();
            });
        }
    }
}