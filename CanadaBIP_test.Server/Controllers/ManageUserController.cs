using CanadaBIP_test.Server.Data;
using CanadaBIP_test.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text;

namespace CanadaBIP_test.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageUserController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public ManageUserController(
            ApplicationDbContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager
        )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("forgotPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            var userConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            if (user == null || !userConfirmed)
            {
                return BadRequest("The user either does not exist or is not confirmed.");
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var callbackUrl = new UriBuilder("https://localhost:5173/reset-password");
            callbackUrl.Query = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"email", model.Email},
                {"code", code},
            }).ReadAsStringAsync().Result;

            var bodyMessage = "<hr/><br/>Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>.<br/><br/><hr/>";

            using var cmd = _context.Result.CreateDbCommand();
            cmd.CommandText = "[dbo].[sp_SendMail_ForgotPassword]";
            cmd.CommandType = CommandType.StoredProcedure;
            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@recipients", SqlDbType.NVarChar) { Value = model.Email });
            cmd.Parameters.Add(new SqlParameter("@body", SqlDbType.NVarChar) { Value = bodyMessage });
            var res = await cmd.ExecuteNonQueryAsync();

            return Ok("Reset password email sent");
        }

        [HttpPost("changePassword")]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }

            var appUser = await _userManager.GetUserAsync(User);

            if (appUser == null)
            {
                return Unauthorized();
            }

            var result = await _userManager.ChangePasswordAsync(appUser, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(appUser.Id);

                if (user != null)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return Ok("Password changed successfully");
                }
            }

            return NoContent();
        }
    }
}
