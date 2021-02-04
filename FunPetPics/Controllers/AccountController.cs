using FunPetPics.Data;
using FunPetPics.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FunPetPics.Controllers
{
    [Route("Account")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger _logger;

        public AccountController(FunPetPicsContext context, IConfiguration configuration, ILogger<AccountController> logger) : base(context)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

            if (user != null && user.Password == password)
            {
                HttpContext.Session.SetString("username", user.DisplayName);
                HttpContext.Session.SetInt32("Id", user.Id);
                return View("Success");
            }
            else
            {
                ViewBag.error = "An acount with this email address does not exist please try again or create an account with the link below";
                return View("Index");
            }
        }

        [Route("Logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }

        public IActionResult CreateAccount()
        {
            ViewData["ReCaptchaKey"] = _configuration.GetSection("GoogleReCaptcha:key").Value;
            return View();
        }

        [Route("ConfirmCreateAccount")]
        [HttpPost]
        public async Task<IActionResult> ConfirmCreateAccount(string email, string displayName, string password, string confirmPassword)
        {
            if (!ReCaptchaPassed(
           Request.Form["g-recaptcha-response"], // that's how you get it from the Request object
           _configuration.GetSection("GoogleReCaptcha:secret").Value,
           _logger
           ))
            {
                ModelState.AddModelError("Capcha", "Please confirm the CAPTCHA to create an account");
                ViewData["ReCaptchaKey"] = _configuration.GetSection("GoogleReCaptcha:key").Value;
                return View("CreateAccount");
            }

            var users = _context.Users.ToList();

            string errormessage = "";

            if (password != confirmPassword)
                errormessage += "Passwords do not match \n";
            if (users.FirstOrDefault(u => u.Email == email) != null)
                errormessage += "An account with this email already exists \n";
            else
                errormessage += validateEmail(email);
            if (users.FirstOrDefault(u => u.DisplayName == displayName) != null)
                errormessage += "This display name already exists \n";

            if (errormessage.Any())
            {
                ViewBag.error = errormessage;
                return CreateAccount();
            }

            var user = new UserModel
            {
                Email = email,
                DisplayName = displayName,
                Password = password,
            };
            _context.Add(user);
            await _context.SaveChangesAsync();
            HttpContext.Session.SetString("username", user.DisplayName);
            return View("Success");
        }

        [Route("ManageAccount")]
        public IActionResult ManageAccount()
        {
            return View();
        }

        public static bool ReCaptchaPassed(string gRecaptchaResponse, string secret, ILogger logger)
        {
            HttpClient httpClient = new HttpClient();
            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={gRecaptchaResponse}").Result;
            if (res.StatusCode != HttpStatusCode.OK)
            {
                logger.LogError("Error while sending request to ReCaptcha");
                return false;
            }

            string JSONres = res.Content.ReadAsStringAsync().Result;
            dynamic JSONdata = JObject.Parse(JSONres);
            if (JSONdata.success != "true")
            {
                return false;
            }

            return true;
        }

        private string validateEmail(string email)
        {
            if (!Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                return "Invalid Email address";
            else
                return "";
        }
    }
}