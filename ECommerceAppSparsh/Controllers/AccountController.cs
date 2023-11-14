using ECommerceAppSparsh.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace ECommerceAppSparsh.Controllers
{
    public class AccountController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7289/api");
        private readonly HttpClient _client;
  
        public AccountController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegistrationModel userModel)
        {
            string data = JsonConvert.SerializeObject(userModel);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Account/Register", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }
            string res = response.Content.ReadAsStringAsync().Result;

            TempData["EmailExists"] = res;
            return View(userModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                string data = JsonConvert.SerializeObject(userModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Account/Login", content);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    var accessToken = JsonConvert.DeserializeObject<TokenConfig>(token);

                    HttpContext.Session.SetString("AccessToken", accessToken.Token);

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(accessToken.Token);
                    var claims = securityToken.Claims.ToList();
                    var claimValue = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                    if (claimValue == "User")
                    {
                        return RedirectToAction("Index", "User");
                    }
                    return RedirectToAction("Index", "Admin");

                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", error);
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Internal Server error. Please try later!!");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Modal()
        {
            return View();
        }
    }
}
