using Identity.Services.Interfaces.Models.User;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Identity.Services.Interfaces.Models.Filtration
{
    public class FilterViewModel
    {
        public FilterViewModel(List<UserResponseCoreModel> users, Guid? user, string email)
        {
            Users = new SelectList(users, "Id", "Email", email);
            SelectedUser = user;
            SelectedEmail = email;
        }

        public SelectList Users { get; private set; }
        public Guid? SelectedUser { get; private set; }
        public string SelectedEmail { get; private set; }
    }
}

