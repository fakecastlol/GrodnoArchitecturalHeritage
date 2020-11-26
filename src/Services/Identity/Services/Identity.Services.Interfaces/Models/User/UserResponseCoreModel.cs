﻿using System;
using Identity.Domain.Core.Entities;
using Identity.Services.Interfaces.Models.User.Abstract;

namespace Identity.Services.Interfaces.Models.User
{
    public class UserResponseCoreModel : CoreModel
    {
        public string Email { get; set; }

        public Roles Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastVisited { get; set; }
        public string Location { get; set; }
        public int ArticleCount { get; set; }
        public int MessagesCount { get; set; }
        public int Rank { get; set; }
        public string Avatar { get; set; }
    }
}
