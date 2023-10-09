using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SuperMarketBackend.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        [BindRequired]
        public string UserName { get; set; }
        [BindRequired]
        public string? FullName { get; set; }
        [BindRequired]
        public string? Password { get; set; }
        [BindRequired]
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public int UserType { get; set; }
    }
}
