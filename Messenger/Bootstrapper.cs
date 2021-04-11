﻿using Api;
using Core;
using DataAccess;
using JetBrains.Annotations;
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
                x.AddRegistry<ApiRegistry>();
                x.AddRegistry<CoreRegistry>();
                x.AddRegistry<DataAccessRegistry>();
                x.ForSingletonOf<ApplicationWindow>().Use<ApplicationWindow>();
                x.ForSingletonOf<MainWindow>().Use<MainWindow>();
                x.ForSingletonOf<IDialogListViewModel>().Use<DialogListViewModel>();
                x.ForSingletonOf<ISearchResultViewModel>().Use<SearchResultViewModel>();
            });
        }
    }
}