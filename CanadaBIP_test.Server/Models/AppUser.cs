using Microsoft.AspNetCore.Identity;

namespace CanadaBIP_test.Server.Models
{
    public class AppUser : IdentityUser
    {
    }

    public class ForgotPasswordModel
    {
        public required string Email { get; set; }
    }

    public class ChangePasswordModel
    { 
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
