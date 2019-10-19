﻿using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public User GetSafeInstance()
        {
            this.Password = null;
            return this;
        }
    }
}
