using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaHotel.Model
{
    public class User
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String UserType { get; set; }
        public bool Active { get; set; }


        public User() { Id = 1; Active = true; }
        public User(string username, string name, string email, string pass, string usertp)
        {
            this.Id = 1;
            this.Username = username;
            this.Name = name;
            this.Email = email;
            this.Password = pass;
            this.UserType = usertp;
            this.Active = true;

        }

        public User(string username, string name, string email, string pass)
        {
            this.Id = 1;
            this.Username = username;
            this.Name = name;
            this.Email = email;
            this.Password = pass;
            this.UserType = "client";
            this.Active = true;

        }
    }
}
