﻿using System.ComponentModel.DataAnnotations;

namespace ApiRest.WebApi.DTO
{
    public class RegisterUserRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required] 
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
