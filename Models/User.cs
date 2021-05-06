using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Username { get; set; }
        private string Password { get; set; }
        public RoleEnum Role { get; set; }

        public User(string firstName, string lastName, string username, string password, RoleEnum role)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Role = role;
        }

        public User Login(string username, string password)
        {
            if (Username != username)
            {
                return null;
            }

            if (Password != password)
            {
                throw new Exception("Wrong password");
            }

            return this;
        }

        //public User LoginPassword(string password)
        //{
        //    if (Password != password)
        //    {
        //        return null;
        //    }

        //    return this;
        //}
    }
}
