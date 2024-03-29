﻿namespace SoftTeK.BusinessAdvisors.Dto.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
