﻿using Api.Models.DTO.Request.Authorization.Login;
using Api.Models.DTO.Request.Authorization.ResetPassword;
using Api.Models.Entities.Application;
using Api.Models.Entities.Identity;
using Api.Services.Security;
using Api.Services.Smtp;
using Api.Services.Smtp.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers.Authorization
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AuthOptions _authOptions;
        private readonly IMailService _mailService;

        public LoginController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<AuthOptions> authOptions,
            IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authOptions = authOptions.Value;
            _mailService = mailService;
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.UserNameOrEmail) ??
               await _userManager.FindByNameAsync(request.UserNameOrEmail);


            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return Unauthorized();
            }

            var tokenClaims = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
                });

            var refreshTokenClaims = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                });

            var token = GetJwt(tokenClaims, _authOptions.GetAccessTokenExpirationTimeSpan());
            var refreshToken = GetJwt(refreshTokenClaims, _authOptions.GetRefreshTokenExpirationTimeSpan());

            return Ok(new { UserId = user.Id, user.Email, token, refreshToken });
        }

        private string GetJwt(ClaimsIdentity claimsIdentity, TimeSpan expirence)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_authOptions.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.Now.Add(expirence),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            // Generate an email verification token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Build the verification URL
            var callbackUrl = Url.Action("ResetPassword", "Login", new { userId = user.Id, token }, Request.Scheme);

            var message = new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Confirm your email",
                Body = $"Please confirm your account by clicking this <a href='{callbackUrl}'>link</a>."
            };

            await _mailService.SendEmailAsync(message);

            return Ok(new { UserId = user.Id, Token = token, CallbackUrl = callbackUrl });
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (result.Succeeded)
            {
                var message = new MailRequest
                {
                    ToEmail = user.Email,
                    Subject = "Password changed",
                    Body = $"Password changed!"
                };

                await _mailService.SendEmailAsync(message);

                return Ok(new { msg = "OK", user.Id, user.Email });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
