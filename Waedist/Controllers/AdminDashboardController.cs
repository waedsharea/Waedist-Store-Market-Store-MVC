using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waedist.Models;

namespace Waedist.Controllers
{
    public class AdminDashboardController : Controller
    {
        private const int V = 3;
        private readonly ModelContext _context;

        public AdminDashboardController(ModelContext context)
        {
            _context = context;
        }
        public IActionResult ProductsStores()
        {

            ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");


            var product = _context.Products.ToList();
            var store = _context.Stores.ToList();
            var productstore = _context.Productstores.ToList();
            var pr = _context.Products.ToList();
            var sr = _context.Stores.ToList();
            var Ct = _context.Categories.ToList();
            var prs = _context.Productstores.ToList();

            var PSCJoin = from p in product
                          join pros in productstore
                          on p.Productid equals pros.Productid
                          join s in sr
                          on pros.Storeid equals s.Storeid
                          join c in Ct
                          on s.Categoryid equals c.Categoryid
                          select new PSCJoincs { product = p, productstore = pros, store = s, category = c };

            var PS = Tuple.Create<IEnumerable<Product>, IEnumerable<Store>, IEnumerable<Productstore>, IEnumerable<PSCJoincs>>(product, store, productstore, PSCJoin);
    
            return View(PS);

        }
        public IActionResult Index()
        {
            ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            var orderproduct = _context.Orderproducts.ToList();
            var useraccount = _context.Useraccounts.ToList();
            var order = _context.Orderrs.ToList();
            var product = _context.Products.ToList();
            var productstore = _context.Productstores.ToList();
            var Store = _context.Stores.ToList();

            var join = from u in useraccount
                       join o in order
                       on u.Userid equals o.Userid
                       join op in orderproduct
                       on o.Orderid equals op.Orderid
                       join p in product
                       on op.Productid equals p.Productid
                
                   
                       select new JoinUserOrder { useraccount = u, order = o, orderproduct = op, product = p};
             var Last =  (from u in @join
                                            orderby u.order.Orderid descending
                                            select u).Take(10);
            var TotalSales = _context.Payments.Sum(a => a.Amount);

            ViewBag.Totalsales   = TotalSales;

            ViewBag.UsersC       = _context.Useraccounts.Count();
            ViewBag.ProductsC    = _context.Products.Count();
            ViewBag.CategoriesC  = _context.Categories.Count();
            ViewBag.StoresC      = _context.Stores.Count();
            ViewBag.OrdersC      = _context.Orderrs.Count();
            ViewBag.MassegesC    = _context.Contactus.Count();
            ViewBag.TestimonialsC= _context.Testmoninals.Count();


           
            var category = _context.Categories.ToList();
            var Users = _context.Useraccounts.ToList();
            var Orders = _context.Orderrs.ToList();
            var Masseges = _context.Contactus.ToList();
            var testimonial = _context.Testmoninals.ToList();
            var lastFiveUsers = (from u in Users
                                 orderby u.Userid descending
                                    select u).Take(10);
            var lastFiveOrders = (from u in Orders
                                  orderby u.Orderid descending
                                 select u).Take(5);
            var testimonials = (from u in testimonial
                              orderby u.Testmoninalid descending
                              select u).Take(10);
            var topsales = (from u in Store orderby u.Totalsales descending select u).Take(3);
            ViewBag.top= topsales.ToArray();

            var Dashboard = Tuple.Create<IEnumerable<Useraccount>, IEnumerable<Contactu>, IEnumerable<Testmoninal>, IEnumerable<JoinUserOrder>, IEnumerable<Orderr>, IEnumerable<Store>>(lastFiveUsers,  Masseges, testimonials , Last, lastFiveOrders, Store);










            return View(Dashboard);
        }
        [HttpPost]
        public async Task<IActionResult> Search(string name)
        {
            ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            var modelContext = _context.Useraccounts.Include(u => u.Role);

            if (name != null)
            {
                var user = await modelContext.Where(x => x.Fullname == name).ToListAsync();
                return View(user);
            }
            else
            {

                return View(modelContext);
            }
            }


        public IActionResult Report()
        {
            var Store = _context.Stores.ToList();


     




            ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");

            ViewBag.Users = _context.Useraccounts.ToList();
            var orderproduct = _context.Orderproducts.ToList();
            var useraccount = _context.Useraccounts.ToList();
            var order = _context.Orderrs.ToList();
            var product = _context.Products.ToList();
            var productstore = _context.Productstores.ToList();
            var payment = _context.Payments.ToList();
            var user = _context.Useraccounts.ToList();
            var Cat = _context.Categories.ToList();

            var pr= _context.Products.ToList();
            var sr = _context.Stores.ToList();
            var Ct = _context.Categories.ToList();
            var prs = _context.Productstores.ToList();

            var PSCJoin = from p in product
                       join pros in productstore
                       on p.Productid equals pros.Productid
                       join s in sr
                       on pros.Storeid equals s.Storeid
                       join c in Ct 
                       on s.Categoryid equals c.Categoryid
                       select new PSCJoincs { product = p, productstore = pros, store = s,category=c };



            var join = from u in useraccount
                       join o in order
                       on u.Userid equals o.Userid
                       join op in orderproduct
                       on o.Orderid equals op.Orderid
                       join p in product
                       on op.Productid equals p.Productid
                     
                    
                       select new JoinUserOrder { useraccount = u, order = o, orderproduct = op, product = p};
            var Last = (from u in @join
                        orderby u.order.Orderid descending
                        select u);
            var TotalSales = _context.Payments.Sum(a => a.Amount);

            ViewBag.Totalsales = TotalSales;

            ViewBag.UsersC = _context.Useraccounts.Count();
            ViewBag.ProductsC = _context.Products.Count();
            ViewBag.CategoriesC = _context.Categories.Count();
            ViewBag.StoresC = _context.Stores.Count();
            ViewBag.OrdersC = _context.Orderrs.Count();
            ViewBag.MassegesC = _context.Contactus.Count();
            ViewBag.TestimonialsC = _context.Testmoninals.Count();



            var category = _context.Categories.ToList();
            var Users = _context.Useraccounts.ToList();
            var Orders = _context.Orderrs.ToList();
            var Masseges = _context.Contactus.ToList();
            var testimonial = _context.Testmoninals.ToList();
            var lastFiveUsers = (from u in Users
                                 orderby u.Userid descending
                                 select u).Take(10);
            var lastFiveOrders = (from u in Orders
                                  orderby u.Orderid descending
                                  select u).Take(10);
            var testimonials = (from u in testimonial
                                orderby u.Testmoninalid descending
                                select u).Take(10);
            ViewBag.TesC = testimonials.Count();
            var stores = _context.Stores.ToList();
            var pay = from p in payment
                      join u in user on p.Userid equals u.Userid
                      select new PaymentUser { payment = p, user = u };
            var Lastpay = (from u in pay
                           orderby u.payment.Payid descending
                           select u);
            
            var Report = Tuple.Create<IEnumerable<Useraccount>, IEnumerable<PaymentUser>, IEnumerable<Store>, IEnumerable<JoinUserOrder>, IEnumerable<Orderr>,  IEnumerable<PSCJoincs>, IEnumerable<Product>>(lastFiveUsers, Lastpay, stores, Last, lastFiveOrders, PSCJoin, product);




            return View(Report);
        }
        public IActionResult _SearchAdmin()
        {
            ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");


            return View();
        }
        public  IActionResult Testimonials()
        {
            ViewBag.Testimonials = _context.Testmoninals.ToList();

            ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");

            var modelContext = _context.Testmoninals.Include(t => t.User);
            return View();
        }


        public IActionResult Logout()
        {



            ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult MyOrders()
        {
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Users = _context.Useraccounts.ToList();

            ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");



            var orderproduct = _context.Orderproducts.ToList();
            var useraccount = _context.Useraccounts.ToList();
            var order = _context.Orderrs.ToList();
            var product = _context.Products.ToList();
            var productstore = _context.Productstores.ToList();
            var Store = _context.Stores.ToList();

            var join = from u in useraccount
                       join o in order
                       on u.Userid equals o.Userid
                       join op in orderproduct
                       on o.Orderid equals op.Orderid
                       join p in product
                       on op.Productid equals p.Productid
                       join ps in productstore
                       on p.Productid equals ps.Productid
                       join s in Store
                       on ps.Storeid equals s.Storeid
                       select new JoinUserOrder { useraccount = u, order = o, orderproduct = op, product = p, productshop = ps, store = s };
            return View(join);
        }


        public IActionResult _Orders()
        {
            ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");


            return View();
        }


        public IActionResult Profile()
        {
            ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.AdminEmail = HttpContext.Session.GetInt32("AdminEmail");
            ViewBag.AdminPhone = HttpContext.Session.GetString("AdminPhone");




            return View();
        }
        public IActionResult ManageHome()
        {
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Users = _context.Useraccounts.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");


            return View();
        }

        public async Task<IActionResult> ManageAboutAsync(decimal? id)
        {
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Users = _context.Useraccounts.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            if (id == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus.FindAsync(id);
            if (aboutu == null)
            {
                return NotFound();
            }

            return View();
        }
        public IActionResult ManageBlog()
        {
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Users = _context.Useraccounts.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            return View();
        }

        public async Task<IActionResult> ManageUsers()

        {
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.Testimonials = _context.Testmoninals.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            var modelContext = _context.Useraccounts.Include(u => u.Role);

      


            return View(await modelContext.ToListAsync());
        }
        public async Task<IActionResult> Messages()
        {
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Users = _context.Useraccounts.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            return View(await _context.Contactus.ToListAsync());
        }


        public IActionResult Orders(DateTime? startDate, DateTime? endDate)
        {       ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Users = _context.Useraccounts.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            var TotalSales = _context.Payments.Sum(a => a.Amount);

            ViewBag.Totalsales = TotalSales;
            ViewBag.OrdersC = _context.Orderrs.Count();

            ViewBag.DelivaryDetailes = _context.Orderdelivaries.ToList();

            var orderproduct = _context.Orderproducts.ToList();
            var useraccount = _context.Useraccounts.ToList();
            var order = _context.Orderrs.ToList();
            var product = _context.Products.ToList();
            var productstore = _context.Productstores.ToList();
            var Store = _context.Stores.ToList();

            var join = from u in useraccount
                       join o in order
                       on u.Userid equals o.Userid
                       join op in orderproduct
                       on o.Orderid equals op.Orderid
                       join p in product
                       on op.Productid equals p.Productid
                  
                 
                       select new JoinUserOrder { useraccount = u, order = o, orderproduct = op, product = p };

            if (startDate == null && endDate == null)
                return View(join);
            else if (startDate != null && endDate == null)
            {
                var result1 = join.Where(x => x.order.Dte.Value.Date >= startDate).ToList();
                return View(result1);
            }
            else if (startDate == null && endDate != null)
            {
                var result = join.Where(x => x.order.Dte.Value.Date <= endDate).ToList();
                return View(join);
            }
            else
            {
                var result = join.Where(x => x.order.Dte.Value.Date <= endDate && x.order.Dte.Value.Date >= startDate).ToList();
                return View(result);
            }
        }





        }






    }

