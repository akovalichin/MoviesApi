﻿using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DbModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}