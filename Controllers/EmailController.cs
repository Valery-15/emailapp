using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmailApp.Controllers
{
    public class EmailController : Controller
    {
        public EmailController()
        {
        }

        public IActionResult Index(string UserName)
        {
            ViewData["username"] = UserName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OpenProfile(string username)
        {
            if (username == null)
            {
                return RedirectToAction("Index", "Home");
            }
            using emaildbContext db = new emaildbContext();
            Users foundUser = await db.Users.FindAsync(username);
            if (foundUser == null)
            {
                await db.Users.AddAsync(new Users() { Username = username });
                await db.SaveChangesAsync();
            }
            var myLetters = await db.Letters
                                    .Where(p => p.Recipient.Equals(username))
                                    .ToListAsync();
            var allUsers = db.Users.ToList();
            List<string> allUsernames = new List<string>();
            foreach (var user in allUsers)
            {
                allUsernames.Add(user.Username);
            }
            ViewBag.allUsernames = allUsernames;
            return RedirectToAction("Index","Email",new { @UserName = username});
        }

        [HttpPost]
        public async Task<IActionResult> SendLetter(string username, string Recipient, string Theme, string Message)
        {

            if(Recipient==null)
            {
                ViewData["username"] = username;
                ViewData["Message"] = "Recipient is required.";
                return View("~/Views/Email/Index.cshtml");
            }
            if (Theme == null)
            {
                ViewData["username"] = username;
                ViewData["Message"] = "Letter theme is required.";
                return View("~/Views/Email/Index.cshtml");
            }
            if (Message == null)
            {
                ViewData["username"] = username;
                ViewData["Message"] = "Message is required.";
                return View("~/Views/Email/Index.cshtml");
            }
            Letters letter = new Letters
            {
                Sender = username,
                Recipient = Recipient,
                SendDateTime = DateTime.UtcNow,
                Theme = Theme,
                Body = Message
            };
            using emaildbContext db = new emaildbContext();

            var allUsers = db.Users.ToList();
            List<string> allUsernames = new List<string>();
            foreach (var user in allUsers)
            {
                allUsernames.Add(user.Username);
            }
            if (!allUsernames.Contains(Recipient))
            {
                ViewData["username"] = username;
                ViewData["Message"] = "The recipient is unknown";
                return View("~/Views/Email/Index.cshtml");
            } else
            {
                await db.Letters.AddAsync(letter);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Email", new { @UserName = username});
            }
        }

        [HttpGet]
        public JsonResult UpdateLetters(string username)
        {
            using emaildbContext db = new emaildbContext();
            var myLetters = db.Letters.Where(p => p.Recipient.Equals(username)).ToList();
            myLetters.Reverse();
            return Json(myLetters);
        }


        [HttpGet]
        public JsonResult UpdateAutocompleteList()
        {
            using emaildbContext db = new emaildbContext();
            var usersList = db.Users.ToList();
            return Json(usersList);
        }
    }
}
