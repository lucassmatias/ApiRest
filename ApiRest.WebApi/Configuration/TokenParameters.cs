﻿using ApiRest.Abstractions;

namespace ApiRest.WebApi.Configuration
{
    public class TokenParameters : ITokenParameters
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Id { get; set; }
    }
}
