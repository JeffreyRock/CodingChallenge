using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Pages;

namespace WebApplication1.Pages.Results
{
    public class users
    {
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string Profession
        {
            get;
            set;
        }
        public int Age
        {
            get; set;
        }
        public int ID
        {
            get; set;
        }

        public users(string firstname, string lastname, string email, string job, int age, int id)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Profession = job;
            Age = age;
            ID = id;
        }
    }
}
