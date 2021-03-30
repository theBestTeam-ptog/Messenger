using System.Collections.Generic;
using Domain.Models;

namespace Messenger.ViewModels
{
    public interface ISearchResultViewModel
    {
        List<UserViewModel> Users { get; set; }
    }
}