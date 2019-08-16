using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Login.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Login.Controllers
{
    public class HomeController : Controller
    {
        private LoginContext dbContext;
        public HomeController(LoginContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }
         [HttpGet("success")]
        public IActionResult Success()
        {
            return View();
        }



    [HttpPost("/register")]
    public IActionResult register(User userFromForm)
    {
        System.Console.WriteLine("Reached Register route!!!!!!!!! *****************************");
        // Check initial ModelState
        if(ModelState.IsValid)
        {
            System.Console.WriteLine("Model state is valid");
            if(dbContext.Users.Any(u => u.Email == userFromForm.Email))
            {
                System.Console.WriteLine("Email is not unique");
                ModelState.AddModelError("Email", "Email already in use!");
                return View("Index");
            }
            else
            {
                System.Console.WriteLine("Everything is valid!");
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                userFromForm.Password = Hasher.HashPassword(userFromForm, userFromForm.Password);
                dbContext.Add(userFromForm);
                dbContext.SaveChanges();
                return RedirectToAction("Success");
            }
        }
        else
        {
            return View("Index");
        }
    } 


[HttpPost("Login")]
    public IActionResult Login(LoginUser userSubmission)
    {
        System.Console.WriteLine("Reached the Login route*************");
        if(ModelState.IsValid)
        {
            System.Console.WriteLine("Model state is valid*************");
            // If inital ModelState is valid, query for a user with provided email
            var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == userSubmission.Email);
            // If no user exists with provided email
            if(userInDb == null)
            {
                // Add an error to ModelState and return to View!
                ModelState.AddModelError("Email", "Invalid Email/Password");
                return View();
            }
            
            // Initialize hasher object
            var hasher = new PasswordHasher<LoginUser>();
            
            // verify provided password against hash stored in db
            var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);
            
            // result can be compared to 0 for failure
            if(result == 0)
            {
                ModelState.AddModelError("Email", "Invalid Email/Password");
                return View();
            }
            return RedirectToAction("success");
        }
        return View();
    }
    }
}
