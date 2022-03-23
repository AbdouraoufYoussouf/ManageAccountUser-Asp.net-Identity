﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LoginRegisterUser.Models
{
    public class AppUser : IdentityUser
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; }
        [Required, MaxLength(100)]
        public string LastName { get; set; }
        public byte[]? ProfilPicture { get; set; }
    }
}
