using Api;
using Core;
using DataAccess;
using DataAccess.Mappers;
using JetBrains.Annotations;
using Messenger.ViewModels;
using StructureMap;
using StructureMap.Pipeline;

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
                // x.For<ApplicationWindow>().Use<ApplicationWindow>().Singleton();
                x.For<MainWindow>().Use<MainWindow>().Transient();
                x.For<IDialogListViewModel>().Use<DialogListViewModel>().Singleton();
                x.For<ISearchResultViewModel>().Use<SearchResultViewModel>().Singleton();
                x.For(typeof(ApplicationWindow)).Use(typeof(ApplicationWindow)).Transient();
            });
        }
    }
}