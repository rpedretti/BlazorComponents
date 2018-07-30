using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RPedretti.Blazor.Shared.Models;
using RPedretti.Blazor.SignalRServer.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RPedretti.Blazor.SignalRServer.Controllers
{
    /// <summary>
    /// Class responsible for handling JWT requests
    /// </summary>
    [Route("[controller]")]
    public class JwtController : Controller
    {
        #region Fields

        private static Dictionary<string, string> refreshTokens = new Dictionary<string, string>();
        private ILogger<JwtController> _logger;

        #endregion Fields

        #region Methods

        private string BuildJwt(UserAuthenticationModel userModel)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SomeSecureRandomKey"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userModel.Username),
                new Claim(ClaimTypes.NameIdentifier, userModel.Username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMinutes(1)).ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.Role, userModel.Username == "admin" ? "Admin" : "User"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                new JwtHeader(creds),
                new JwtPayload(claims)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string BuildRefreshJwt(UserAuthenticationModel userModel)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SomeSecureRandomKey"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userModel.Username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(5)).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                new JwtHeader(creds),
                new JwtPayload(claims)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion Methods

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        public JwtController(ILogger<JwtController> logger)
        {
            _logger = logger;
        }

        #endregion Constructors

        /// <summary>
        /// Requests a new JWT Token
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userRepository"></param>
        /// <param name="cryptoService"></param>
        /// <param name="urlHelper"></param>
        /// <returns></returns>
        [HttpPost, Route("requestjwt")]
        [ProducesResponseType(typeof(SecureJwtModel), 200)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetJwt(
            [FromBody] UserAuthenticationModel model,
            [FromServices] IUserRepository userRepository,
            [FromServices] IUrlHelper urlHelper)
        {
            if (ModelState.IsValid)
            {
                var user = await userRepository.GetUserAsync(model.Username);
                if (user == null)
                {
                    user = new Shared.Domain.User { Username = model.Username, Password = model.Password };
                    await userRepository.AddUserAsync(user);
                }
                if (user != null && user.Password == model.Password)
                {
                    var refresh = BuildRefreshJwt(model);
                    refreshTokens[refresh] = model.Username;
                    return Json(new SecureJwtModel
                    {
                        OriginId = 0,
                        TokenModel = new TokenModel
                        {
                            Token = BuildJwt(model),
                            RefreshToken = refresh,
                            RefreshUrl = urlHelper.Action("RefreshJwt"),
                            Expires = DateTime.Now.AddMinutes(1)
                        }
                    });
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return BadRequest(ModelState.ValidationState);
            }
        }

        /// <summary>
        /// Refresh an existing JWT
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cryptoService"></param>
        /// <param name="urlHelper"></param>
        /// <returns></returns>
        [HttpPost, Authorize, Route("refreshjwt", Name = "RefreshJwt")]
        [ProducesResponseType(typeof(SecureJwtModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult RefreshJwt(
            [FromBody] string base64Token,
            [FromServices] IUrlHelper urlHelper)
        {
            if (ModelState.IsValid)
            {
                var refreshToken = Encoding.UTF8.GetString(Convert.FromBase64String(base64Token));

                if (refreshTokens.TryGetValue(refreshToken, out string username))
                {
                    return Json(new SecureJwtModel
                    {
                        OriginId = 0,
                        TokenModel = new TokenModel
                        {
                            Token = BuildJwt(new UserAuthenticationModel { Username = username }),
                            RefreshToken = refreshToken,
                            RefreshUrl = urlHelper.Action("RefreshJwt"),
                            Expires = DateTime.Now.AddMinutes(1)
                        }
                    });
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return BadRequest(ModelState.ValidationState);
            }
        }
    }
}
