﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_PROJECT_MPT.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirthday { get; set; }
    }

    public enum SortState
    {
        IdAsc, 
        IdDesc,
        EmailAsc,
        EmailSesc,
        LoginAsc,
        LoginDesc
    }
}
