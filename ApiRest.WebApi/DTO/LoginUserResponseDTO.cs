﻿namespace ApiRest.WebApi.DTO
{
    public class LoginUserResponseDTO
    {
        public string Token { get; set; }
        public bool Login { get; set; }
        public List<string> Errors { get; set; }
    }
}
