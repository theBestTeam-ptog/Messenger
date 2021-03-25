using System.Collections.Generic;
using Domain.Models;
using JetBrains.Annotations;

namespace Messenger.ViewModels
{
    [UsedImplicitly]
    public sealed class SearchResultViewModel : ISearchResultViewModel
    {
        public List<UserViewModel> Users { get; set; }
    }
}