﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject
{
    public class User
    {
        public static int UsersCount = 1;
        public int UserID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsAdmin { get; set; }
        public User(string login, string password, string name, string surname, bool isAdmin)
        {
            this.UserID = UsersCount;
            this.Login = login;
            this.Password = password;
            this.Name = name;
            this.Surname = surname;
            this.IsAdmin = isAdmin;
            UsersCount++;
        }

    }
}
