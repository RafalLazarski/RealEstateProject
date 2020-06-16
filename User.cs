using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProject
{
    [Serializable]
    public class User
    {
        public static int UsersCount = 0;
        public int UserID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Archive { get; set; }
        public UserPreferences UserPreferences { get; set; }
        public List<string> MailsList { get; set; }
        public bool NewMessage { get; set; }

        public User(string login, string password, string name, string surname, UserPreferences userPreferences)
        {
            ++UsersCount;
            this.UserID = UsersCount;
            this.Login = login;
            this.Password = password;
            this.Name = name;
            this.Surname = surname;
            this.UserPreferences = userPreferences;
            this.Archive = false;
            this.MailsList = new List<string>();
            this.NewMessage = false;            
        }

    }
}
