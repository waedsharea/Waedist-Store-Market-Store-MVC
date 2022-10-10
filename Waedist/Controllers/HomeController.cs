using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Waedist.Models;

namespace Waedist.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _iwebHostEnvirounment;
        private readonly ModelContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ModelContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;

        }

        public IActionResult Index()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");

            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Home = _context.Homes.ToList();
            ViewBag.UsersC = _context.Useraccounts.Count();
            ViewBag.ProductsC = _context.Products.Count();
            ViewBag.CategoriesC = _context.Categories.Count();
            ViewBag.StoresC = _context.Stores.Count();
            var category = _context.Categories.ToList();
            var product = _context.Products.ToList();

            var store = _context.Stores.ToList();
            var aboutus = _context.Aboutus.ToList();
            var testimonial = _context.Testmoninals.ToList();
            var homePage = _context.Homes.ToList();
            var lastFiveProducts = (from u in product
                                    orderby u.Productid descending
                                    select u).Take(10);
            var home = Tuple.Create<IEnumerable<Home>, IEnumerable<Aboutu>, IEnumerable<Category>, IEnumerable<Store>, IEnumerable<Testmoninal>, IEnumerable<Product>>(homePage, aboutus, category, store, testimonial, lastFiveProducts);

            return View(home);
        }
   

        public IActionResult Privacy()
        {
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Home = _context.Homes.ToList();

            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");

            return View();
        }
        public IActionResult Aboutus()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Home = _context.Homes.ToList();
            ViewBag.About = _context.Aboutus.ToList();

            return View();
        }
        public IActionResult Blog()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Home = _context.Homes.ToList();

            return View();
        }
        public IActionResult Contactus()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Home = _context.Homes.ToList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contactus( Contactu Contact )
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Home = _context.Homes.ToList();

            if (ModelState.IsValid)
            {

                Contact.Tittle = "ContactUs";
                Contact.Tittlepic = "None";
                _context.Add(Contact);
                await _context.SaveChangesAsync();

                return View();

            }
            return View();

        }


        [HttpPost]
        public IActionResult SendEmail(string Email)
        {   ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            string Name = ViewBag.UserName;

            MimeMessage obj = new MimeMessage();
            MailboxAddress emailfrom = new MailboxAddress("Waedit.Com", "wshareaa@gmail.com");
            MailboxAddress emailto = new MailboxAddress("Waedit.Com", Email);
            obj.From.Add(emailfrom);
            obj.To.Add(emailto);
            obj.Subject = "Waedist Stylist Stores Subscribtion";
            BodyBuilder Body = new BodyBuilder();
            // bb.TextBody = body;
            Body.HtmlBody = "<html>" + "<h1>" + " Dear, "  + "</h1>" + "</br>" + "Thank you for your subscribtion" +   "</br>" + "</html>";
            obj.Body = Body.ToMessageBody();
            MailKit.Net.Smtp.SmtpClient emailclient = new MailKit.Net.Smtp.SmtpClient();
            emailclient.Connect("smtp.gmail.com", 465, true);
            emailclient.Authenticate("wshareaa@gmail.com", "raaplatpmlacyfis");
            emailclient.Send(obj);
            emailclient.Disconnect(true);
            emailclient.Dispose();
            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Home = _context.Homes.ToList();

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
