using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Waedist.Models;

namespace Waedist.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IWebHostEnvironment _iwebHostEnvirounment;
        private readonly ModelContext _context;
        public decimal flag = 1;
        public AuthenticationController(ModelContext context, IWebHostEnvironment iwebHostEnvirounment)
        {
            _context = context;
            _iwebHostEnvirounment = iwebHostEnvirounment;
        }
        public IActionResult Index()
        {
            ViewBag.flag = HttpContext.Session.GetInt32("flag");

            return View();
        }

        public IActionResult Register()
        {
            ViewBag.flag = HttpContext.Session.GetInt32("flag");
            ViewBag.img = HttpContext.Session.GetInt32("img");

            ViewBag.Register = HttpContext.Session.GetInt32("Register");
            ViewBag.pass = HttpContext.Session.GetInt32("pass");


            HttpContext.Session.SetInt32("flag", 3);

            return View();
        }
        public IActionResult IncorrectEmail()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Userid, Fullname, Phonenumber, Pic, Email, Password, Roleid")] Useraccount useraccount, IFormFile upload, string Email , string pass1 , string pass2)
        {
            HttpContext.Session.SetInt32("flag", 3);

            if (ModelState.IsValid)
            {
                if (pass1 != pass2)
                {
                    HttpContext.Session.SetInt32("pass", 0);

                }
                else {
                    HttpContext.Session.SetInt32("pass", 1);

                }
                var exists = _context.Useraccounts.Where(data => data.Email == useraccount.Email).SingleOrDefault();
                if (exists == null)
                {
                    HttpContext.Session.SetInt32("Register", 1);


                    if (upload != null)
                    {
                        HttpContext.Session.SetInt32("Img", 1);


                        string wwwroot = _iwebHostEnvirounment.WebRootPath;
                        string filename = Guid.NewGuid() + "_" + upload.FileName;
                        string path = Path.Combine(wwwroot + "/Images/", filename);

                        using (var filestream = new FileStream(path, FileMode.Create))
                        {

                            upload.CopyTo(filestream);
                            useraccount.Pic = filename;
                            useraccount.Roleid = 2;

                            _context.Add(useraccount);
                            await _context.SaveChangesAsync();
                        }


                    }
                    HttpContext.Session.SetInt32("Img", 0);
                    return RedirectToAction("Login", "Authentication");

                }
                else
                {
                    HttpContext.Session.SetInt32("Register", 0);
                }

            }


            return RedirectToAction("Register", "Authentication");
        }
        public IActionResult Login()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("Roleid");
            ViewBag.flag = HttpContext.Session.GetInt32("flag");


            if (ViewBag.RoleId == 1)
            {
                return RedirectToAction("Index", "AdminDashboard");


            }
            else if (ViewBag.RoleId == 2)
            {
                return RedirectToAction("Index", "UserDashboard");
            }
            else
            return View();

        }
  
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login([Bind("Email,Password")] Useraccount useraccount)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.RoleId = HttpContext.Session.GetInt32("Roleid");



            var auth = _context.Useraccounts.Where(data => data.Email == useraccount.Email && data.Password == useraccount.Password).SingleOrDefault();

            if (auth != null)
            {
                HttpContext.Session.SetInt32("AlreadyLogged", 1);
                HttpContext.Session.SetInt32("Roleid", (int)auth.Roleid);

                // Login
                // 1- Admin
                // 2- Customer
                // Checking by the RoleId
                switch (auth.Roleid)
                {
                    case 1:
                        // Customer

                        HttpContext.Session.SetInt32("flag", 2);

                        HttpContext.Session.SetInt32("AdminId", (int)auth.Userid);
                        HttpContext.Session.SetString("Image", auth.Pic);

                        HttpContext.Session.SetString("AdminName", auth.Fullname);
                        HttpContext.Session.SetString("AdminEmail", auth.Email);

                        return RedirectToAction("Index", "AdminDashboard");
                    case 2:
                        {
                            HttpContext.Session.SetInt32("flag", 1);

                            HttpContext.Session.SetInt32("UserId", (int)auth.Userid);
                            ViewBag.Userid = HttpContext.Session.GetInt32("UserId");
                            HttpContext.Session.SetString("UserEmail", auth.Email);
                            HttpContext.Session.SetString("FullName", auth.Fullname);
                            await StartOrderAsync();

                            return RedirectToAction("Index", "UserDashboard");
                        }

                }

            }
            else
            {
                HttpContext.Session.SetInt32("flag", 0);
                return RedirectToAction("Login", "Authentication");
            }
            await _context.SaveChangesAsync();

            return View(useraccount);
        }

        public async Task<IActionResult> StartOrderAsync()
        {
            
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            var id = ViewBag.UserId;
            DateTime now = DateTime.Now;
            Orderr order = new Orderr();
            order.Userid = id;
            order.Status = "0";
            order.Display = "0";
            order.Dte = now;
            _context.Add(order);
            await _context.SaveChangesAsync();

            return View();
        }
        public async Task<IActionResult> EndOrderAsync(int? id)
        {

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
             id = ViewBag.UserId;
            Orderr order = _context.Orderrs.Where(x => x.Userid == id && x.Status == "0").SingleOrDefault(); 
            order.Status = "1";
            _context.Update(order);
            await _context.SaveChangesAsync();

            return View();
        }

    }
}
