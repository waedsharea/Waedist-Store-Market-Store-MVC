using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Waedist.Models;

namespace Waedist.Controllers
{
    public class UserDashboardController : Controller
    {
        public bool flag = true;
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _iwebHostEnvirounment;

        public UserDashboardController(ModelContext context, IWebHostEnvironment iwebHostEnvirounment)
        {
            _context = context;
            _iwebHostEnvirounment = iwebHostEnvirounment;
        }
        public IActionResult UserIndex()
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
            ViewBag.UserId = HttpContext.Session.GetInt32("UserName");

            return View();
        }
        public IActionResult PaymentConfirmation()
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
            ViewBag.OrederId = HttpContext.Session.GetInt32("OrderIdd");




            return View();
        }
        public async Task<IActionResult> Index()
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



            return View(await _context.Categories.ToListAsync());
        }
        public async Task<IActionResult> Products()
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
            return View(await _context.Products.ToListAsync());
        }

        public async Task<IActionResult> StartShopping(Orderr order)
        {
            ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId"); var id = ViewBag.Userid;
            DateTime Date = DateTime.Now;

            order.Dte = Date;
            order.Userid = id;
            order.Status = "0";
            _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AddToCart));
        }

        public IActionResult CreateOrder()
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
            ViewData["Userid"] = new SelectList(_context.Useraccounts, "Userid", "Userid");
            return View();
        }

        // POST: Orderrs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder([Bind("Orderid,Userid,Dte,Status")] Orderr order)
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
            if (ModelState.IsValid)
            {

                ViewBag.Userid = HttpContext.Session.GetInt32("UserId");
                var id = ViewBag.Userid;
                DateTime Date = DateTime.Now;

                order.Dte = Date;
                order.Userid = id;
                order.Status = "0";
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userid"] = new SelectList(_context.Useraccounts, "Userid", "Userid", order.Userid);
            return View(order);
        }
        //// GET: Orderproducts/Create
        //public IActionResult AddToCart()

        //{
        //    ViewBag.Users = _context.Useraccounts.ToList();

        //    ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
        //    ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
        //    ViewBag.AdminImg = HttpContext.Session.GetString("Image");
        //    ViewBag.UserName = HttpContext.Session.GetString("FullName");
        //    ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
        //    ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
        //    ViewBag.products = _context.Products.ToList();

        //    ViewData["Orderid"] = new SelectList(_context.Orderrs, "Orderid", "Orderid");
        //    ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid");
        //    return View();
        //}


        //// POST: Orderproducts/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddToCart([Bind()] Orderproduct orderproduct,  decimal price , Orderr order)
        //{
        //    ViewBag.Users = _context.Useraccounts.ToList();

        //    ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
        //    ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
        //    ViewBag.AdminImg = HttpContext.Session.GetString("Image");
        //    ViewBag.UserName = HttpContext.Session.GetString("FullName");
        //    ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
        //    ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
        //    ViewBag.products = _context.Products.ToList();
        //    ViewBag.Userid = HttpContext.Session.GetInt32("UserId");

        //    DateTime Date = DateTime.Now;
        //    ViewBag.OrderDate = Date;
        //    if (ModelState.IsValid)
        //    {

        //        decimal oid = (from r in _context.Orderrs orderby r.Orderid select r.Orderid).Max();

        //        order.Dte = Date;
        //            order.Userid = ViewBag.Userid;
        //            order.Status = "0";
        //            _context.Add(order);
        //            await _context.SaveChangesAsync();



        //        ViewBag.OrderId = oid;
        //        orderproduct.Orderid = oid;
        //        orderproduct.Status = "0";
        //        orderproduct.Totalamount = price * orderproduct.Qty;
        //        _context.Add(orderproduct);

        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("AddToCart","UserDashboard");
        //    }

        //    return View(orderproduct);
        //}


        public IActionResult AddToCart(int? id)
        {
            var storenamee = "";
            ViewBag.filter = id;
            ViewBag.Stores = _context.Stores.ToList();
            foreach (var s in ViewBag.Stores)
                if (s.Storeid == ViewBag.filter)
                    storenamee = s.Storename;
            if (id != null)
            {
                HttpContext.Session.SetInt32("STOREID", (int)id);
                HttpContext.Session.SetString("StoreName", storenamee);

            }
            ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            if (id != null)
            {
                var product = _context.Products.ToList();
                var orderproducts = _context.Orderproducts.ToList();
                var productstore = _context.Productstores.Where(x => x.Storeid == id);
                var store = _context.Stores.ToList();
                var join = from p in product
                           join ps in productstore
                           on p.Productid equals ps.Productid
                           join s in store
                           on ps.Storeid equals s.Storeid
                           select new JoinStoreProduct { product = p, productstore = ps, store = s };

                var pro = _context.Products.Where(x => x.Productid == id);
                ViewBag.Products = join;
            }
            else {

                var product = _context.Products.ToList();
                var orderproducts = _context.Orderproducts.ToList();
                var productstore = _context.Productstores.ToList();
                var store = _context.Stores.ToList();
                var join = from p in product
                           join ps in productstore
                           on p.Productid equals ps.Productid
                           join s in store
                           on ps.Storeid equals s.Storeid
                           select new JoinStoreProduct { product = p, productstore = ps, store = s };

                var pro = _context.Products.Where(x => x.Productid == id);
                ViewBag.Products = join;

            }

            return View();
        }
      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart([Bind()] int? id, decimal price, decimal Pid, int Qnn)
        {

            ViewBag.Storeiid = HttpContext.Session.GetInt32("STOREID");
            ViewBag.Storenamee = HttpContext.Session.GetString("StoreName");
            string Storename = ViewBag.Storenamee;
            decimal Storeiid = ViewBag.Storeiid;
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            id = ViewBag.UserId;
            Orderr order = _context.Orderrs.Where(x => x.Status == "0" && x.Userid == id).FirstOrDefault();

            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Email = HttpContext.Session.GetString("Email");

            order.Userid = ViewBag.UserId;
            DateTime now = DateTime.Now;
            order.Dte = now;
            _context.Update(order);
            await _context.SaveChangesAsync();

            decimal oid = (from r in _context.Orderrs orderby r.Orderid select r.Orderid).Max();
            Orderproduct orderproduct = new Orderproduct();
            orderproduct.Qty = Qnn;
            orderproduct.Status = "0";
            orderproduct.Totalamount = Qnn * price;
            orderproduct.Orderid = order.Orderid;
            HttpContext.Session.SetInt32("OrderIdd", (int)order.Orderid);

            orderproduct.Totalamount = price * orderproduct.Qty;
            orderproduct.Productid = Pid;
            orderproduct.Stoid = Storeiid;
            if (Storename != null)
            orderproduct.Storename = Storename;
            else
                orderproduct.Storename = "Stores";
            _context.Add(orderproduct);
            await _context.SaveChangesAsync();


            //orderproduct.Qty = Numberofpieces;
            //orderproduct.Totalamount = Numberofpieces * product.Price;
            //orderproduct.Productid = product.Productid;
            return RedirectToAction("AddToCart", "UserDashboard");
        }








        public IActionResult DelivaryDetailes()
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
            return View();
        }

        // POST: Orderrs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DelivaryDetailes([Bind("Orderid,Userid,Username,Useraddress,Useremail,Userphone,Shippingmethod,Postnumber")] Orderdelivary orderdelivary)
        {
            ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.OrederId = HttpContext.Session.GetInt32("OrderIdd");

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            decimal id = ViewBag.Userid;
            if (ModelState.IsValid)
            {
               


                ViewBag.Userid = HttpContext.Session.GetInt32("UserId");
                    DateTime Date = DateTime.Now;

                    orderdelivary.Userid = ViewBag.Userid;
                if (ViewBag.orderid != null)
                {
                    orderdelivary.Orderid = ViewBag.OrederId;
                }
                else 
                {
                    Orderr order = _context.Orderrs.Where(x => x.Status == "0" && x.Userid == id).FirstOrDefault();
                    orderdelivary.Orderid = order.Orderid;
                }
                    orderdelivary.Shippingmethod = "Via Post Office";
                    _context.Add(orderdelivary);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(PaymentConfirmation));
                
            }
            return View(orderdelivary);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMethod(decimal id, string dm)
        {
            var orderdelivary = _context.Orderdelivaries.Where(x => x.Odlivaryid == id).SingleOrDefault();
            if (ModelState.IsValid)
            {
                orderdelivary.Shippingmethod = dm;
                    _context.Update(orderdelivary);
                    await _context.SaveChangesAsync();
                
             
                return RedirectToAction("Orders","Admindashboard");
            }
            return View(orderdelivary);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditMethod(decimal id, string dm)
        //{

        //  var o = _context.Orderdelivaries.Where(x=>x.)

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {

        //            _context.Update(orderdelivary);
        //            await _context.SaveChangesAsync();
        //        }

        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(orderdelivary);
        //}







        /// <summary>
        /// ///////////////////////////////
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        //public IActionResult AddToCart(int id)
        //{
        //    ViewBag.filter = id;

        //    ViewBag.Users = _context.Useraccounts.ToList();

        //    ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
        //    ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
        //    ViewBag.AdminImg = HttpContext.Session.GetString("Image");
        //    ViewBag.UserName = HttpContext.Session.GetString("FullName");
        //    ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
        //    ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

        //    var product = _context.Products.ToList();
        //    var orderproducts = _context.Orderproducts.ToList();
        //    var productstore = _context.Productstores.Where(x => x.Storeid == id);
        //    var store = _context.Stores.ToList();
        //    var join = from p in product
        //               join ps in productstore
        //               on p.Productid equals ps.Productid
        //               join s in store
        //               on ps.Storeid equals s.Storeid
        //               select new JoinStoreProduct { product = p, productstore = ps, store = s };

        //    var pro = _context.Products.Where(x => x.Productid == id);
        //    ViewBag.Products = join;
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddToCart([Bind()] int id, decimal price, Orderr order, decimal Pid, int Qnn)
        //{
        //    ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
        //    ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
        //    ViewBag.Email = HttpContext.Session.GetString("Email");
        //    var product = _context.Products.Where(x => x.Productid == id).FirstOrDefault();
        //    order.Userid = ViewBag.Userid;
        //    DateTime now = DateTime.Now;
        //    order.Dte = now;
        //    order.Status = "0";
        //    _context.Add(order);
        //    await _context.SaveChangesAsync();

        //    decimal oid = (from r in _context.Orderrs orderby r.Orderid select r.Orderid).Max();

        //    Orderproduct orderproduct = new Orderproduct();
        //    orderproduct.Qty = Qnn;
        //    orderproduct.Status = "0";
        //    orderproduct.Totalamount = Qnn * price;
        //    orderproduct.Orderid = oid;
        //    orderproduct.Totalamount = price * orderproduct.Qty;
        //    orderproduct.Productid = Pid;
        //    _context.Add(orderproduct);
        //    await _context.SaveChangesAsync();


        //    //orderproduct.Qty = Numberofpieces;
        //    //orderproduct.Totalamount = Numberofpieces * product.Price;
        //    //orderproduct.Productid = product.Productid;
        //    return RedirectToAction("AddToCart", "UserDashboard");
        //}



        /// <summary>
        /// /////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>









        public IActionResult ShoppingCart()
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
            var Id = ViewBag.UserId;
            decimal oid = (from r in _context.Orderrs orderby r.Orderid select r.Orderid).Max();
            var Order = _context.Orderrs.ToList();
            var OrderProduct = _context.Orderproducts.ToList();
            List<decimal> orderid = new List<decimal>();
            decimal total = 0;
            foreach (var item in Order)
            {
                if (Id == item.Userid)
                {
                    if (item.Status == "1" && item.Display == "0")
                    {
                        orderid.Add(item.Orderid);



                    }
                }
            }
            ViewBag.Orderid = orderid;


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
            return View(join);
        }
        public IActionResult PaidCart()
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
            return View(join);
        }

        public IActionResult Invoice()
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
            var Id = ViewBag.UserId;
            decimal oid = (from r in _context.Orderrs orderby r.Orderid select r.Orderid).Max();
            var Order = _context.Orderrs.ToList();
            var OrderProduct = _context.Orderproducts.ToList();
            List<decimal> orderid = new List<decimal>();
            decimal total = 0;
            foreach (var item in Order)
            {
                if (Id == item.Userid)
                {
                    if (item.Status == "1" && item.Display == "0")
                    {
                        orderid.Add(item.Orderid);
                 


                    }
                }
            }
            ViewBag.Orderid = orderid;

            ViewBag.UserPhone = HttpContext.Session.GetInt32("PhoneNumber");

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
            return View(join);
        }
        [HttpPost]
        public async Task<IActionResult> Display()
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
            ViewBag.UserId = HttpContext.Session.GetInt32("UserName");

            var Id = ViewBag.UserId;
            decimal oid = (from r in _context.Orderrs orderby r.Orderid select r.Orderid).Max();
            var Order = _context.Orderrs.ToList();
            var OrderProduct = _context.Orderproducts.ToList();
            List<decimal> orderid = new List<decimal>();
            decimal total = 0;
            foreach (var item in Order)
            {
                
                    if (item.Userid == Id && item.Status == "0" && item.Display == "0")
                    {
                        item.Display = "1";
                        _context.Update(item);
                        await _context.SaveChangesAsync();



                    }
                
            }
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
            return RedirectToAction(nameof(Index));
        }


   












        public IActionResult SendEmail()
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

            return View();
        }


        [HttpPost]
        public IActionResult SendEmail(string to, string subject, decimal amount)
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
            string Name = ViewBag.UserName;
            to = ViewBag.UserEmail;

            MimeMessage obj = new MimeMessage();
            MailboxAddress emailfrom = new MailboxAddress("Waedit.Com", "aasss1223331@gmail.com");
            MailboxAddress emailto = new MailboxAddress("Waedit.Com", to);
            obj.From.Add(emailfrom);
            obj.To.Add(emailto);
            obj.Subject = subject;
            BodyBuilder Body = new BodyBuilder();
            // bb.TextBody = body;
            Body.HtmlBody = "<html>" + "<h1>" + " Hi " + ViewBag.UserName + "</h1>" + "</br>" + "Your payment have been done Successfully " + "</br>" + " " + " Total : " + amount + "$" + "</br>" + "</html>";
            obj.Body = Body.ToMessageBody();
            MailKit.Net.Smtp.SmtpClient emailclient = new MailKit.Net.Smtp.SmtpClient();
            emailclient.Connect("smtp.gmail.com", 465, true);
            emailclient.Authenticate("wshareaa@gmail.com", "raaplatpmlacyfis");
            emailclient.Send(obj);
            emailclient.Disconnect(true);
            emailclient.Dispose();
            return View();
        }

        //[HttpPost]
        // public IActionResult SendEmail(string to, string subject, decimal amount)
        // {
        //     ViewBag.Users = _context.Useraccounts.ToList();
        //     ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
        //     ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
        //     ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
        //     ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
        //     ViewBag.AdminImg = HttpContext.Session.GetString("Image");
        //     ViewBag.UserName = HttpContext.Session.GetString("FullName");
        //     ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
        //     ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

        //     string T = ViewBag.UserEmail;


        //     using System.Net.Mail.SmtpClient mySmtpClient = new System.Net.Mail.SmtpClient("smtp.outlook.com", 587);
        //     mySmtpClient.EnableSsl = true;

        //     // set smtp-client with basicAuthentication
        //     mySmtpClient.UseDefaultCredentials = false;
        //     NetworkCredential basicAuthenticationInfo = new
        //    NetworkCredential("WaedistStore@outlook.com", "W@edW@ed12");


        //     mySmtpClient.Credentials = basicAuthenticationInfo;
        //     MailMessage message = new MailMessage("WaedistStore@outlook.com", T);
        //     string body = " Hi " + ViewBag.UserName + "Your payment have been done Successfully "  + " Total : " + amount + "$" ;
        //     message.Subject = "Waedist Stores - Invoice";
        //     message.Body = body;



        //     mySmtpClient.Send(message);









        //     return View();
        // }

















        public IActionResult Testimonials()
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

            ViewBag.Userid = HttpContext.Session.GetInt32("UserId");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Testmoninalid,Message,Name,Testimage,Status,Userid")] Testmoninal testmoninal, IFormFile img)
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
            ViewBag.Userid = HttpContext.Session.GetInt32("UserId");

            if (ModelState.IsValid)
            {
                if (img != null)
                {

                    string wwwroot = _iwebHostEnvirounment.WebRootPath;
                    string filename = Guid.NewGuid() + "_" + img.FileName;
                    string path = Path.Combine(wwwroot + "/Images/Testimonial/", filename);

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {


                        img.CopyTo(filestream);
                        testmoninal.Testimage = filename;
                        testmoninal.Userid = ViewBag.Userid;
                        testmoninal.Status = "R";
                        _context.Add(testmoninal);
                        await _context.SaveChangesAsync();
                    }

                    return RedirectToAction("Testimonials", "UserDashboard");

                }


            }
            ViewData["Userid"] = new SelectList(_context.Useraccounts, "Userid", "Userid", testmoninal.Userid);
            return View(testmoninal);
        }




        public IActionResult _Payment()
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


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> StroeSale(List<Orderproduct> orderproduct )
        {
            decimal total = 0;
            
            var stores = _context.Stores.ToList();
            foreach (var s in stores)
            {
                foreach (var op in orderproduct)
                {
                    if (s.Storeid == op.Stoid)
                    {
                        total = total + (decimal)op.Totalamount;

                    }
                  
                }
                s.Totalsales =s.Totalsales + total;
                _context.Stores.Update(s);
                total = 0;

            }
            await _context.SaveChangesAsync();



            return RedirectToAction("Invoice", "UserDashboard");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> _Payment([Bind()] Bank bank, Payment payment)
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


            var userid = ViewBag.UserId;
            var auth = _context.Banks.Where(data => data.Cardnumber == bank.Cardnumber && data.Cvv == bank.Cvv).SingleOrDefault();
            var Order = _context.Orderrs.ToList();
            var OrderProduct = _context.Orderproducts.ToList();
            List<decimal> orderid = new List<decimal>();
            decimal total = 0;
            foreach (var item in Order)
            {
                if (userid == item.Userid)
                {
                    if (item.Status == "0" && item.Display == "0")
                    {
                        orderid.Add(item.Orderid);
                        item.Status = "1";
                        _context.Orderrs.Update(item);


                    }
                }
            }
            List<Orderproduct> orderproduct = new List<Orderproduct>();
            foreach (var item in _context.Orderproducts)
            {
                foreach (var o in orderid)
                {
                    if (item.Orderid == o)
                    {
                        var order = item;
                        orderproduct.Add(order);
                        item.Status = "1";
                        _context.Update(item);
                    }
                }
            }
            foreach (var p in orderproduct)
            {
                total = (decimal)(total + p.Totalamount);
                _context.Orderproducts.Update(p);
            }


            if (auth.Amount >= total)
            {
                auth.Amount = auth.Amount - total;
            }
            DateTime now = DateTime.Now;
            payment.Datee = now;
            payment.Cardnumber = auth.Cardnumber;
            payment.Amount = total;
            payment.Userid = userid;

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            SendEmail(ViewBag.UserEmail, "success checkout", total);
            await StroeSale(orderproduct);
            return RedirectToAction("Invoice", "UserDashboard");
        }
        public IActionResult Logout()
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
            decimal id = ViewBag.UserId;
            HttpContext.Session.Clear();
            EndOrder();
            return RedirectToAction("Index", "Home");
        }

            public async Task<IActionResult> Remove(decimal id)
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
            var orderproduct = await _context.Orderproducts.FindAsync(id);
            _context.Orderproducts.Remove(orderproduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ShoppingCart));
        }
        //public IActionResult _SinglePayment( decimal orderId)
        //{
        //    ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
        //    ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
        //    ViewBag.Email = HttpContext.Session.GetString("Email");
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public async Task<IActionResult> _SinglePayment([Bind()] Bank bank, Payment payment , decimal orderId)
        //{
        //    ViewBag.UserName = HttpContext.Session.GetString("FullName");
        //    ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
        //    ViewBag.UserId = HttpContext.Session.GetInt32("UserId");


        //    var userid = ViewBag.UserId;
        //    var auth = _context.Banks.Where(data => data.Cardnumber == bank.Cardnumber && data.Cvv == bank.Cvv).SingleOrDefault();
        //    var Order = _context.Orderrs.ToList();
        //    var OrderProduct = _context.Orderproducts.ToList();
        //    List<decimal> orderid = new List<decimal>();
        //    decimal total = 0;

        //    foreach (var item in Order)
        //    {
        //        if (userid == item.Userid && item.Status == "0")
        //        {
        //            orderid.Add(item.Orderid);
        //            item.Status = "1";
        //            _context.Orderrs.Update(item);


        //        }
        //    }
        //    List<Orderproduct> orderproduct = new List<Orderproduct>();
        //    foreach (var item in _context.Orderproducts)
        //    {

        //            if (item.Orderid == orderId)
        //            {
        //                var order = item;
        //                orderproduct.Add(order);
        //                item.Status = "1";
        //                _context.Update(item);
        //            }

        //    }
        //    foreach (var p in orderproduct)
        //    {
        //        total = (decimal)(total + p.Totalamount);
        //        _context.Orderproducts.Update(p);
        //    }


        //    if (auth.Amount >= total)
        //    {
        //        auth.Amount = auth.Amount - total;
        //    }
        //    DateTime now = DateTime.Now;
        //    payment.Datee = now;
        //    payment.Cardnumber = auth.Cardnumber;
        //    payment.Amount = total;
        //    payment.Userid = userid;

        //    _context.Payments.Add(payment);
        //    await _context.SaveChangesAsync();
        //    SendEmail(ViewBag.UserEmail, "success checkout", total);
        //    return View();
        //}




        [HttpPost]
        public async Task<IActionResult> EndOrder()
        {


            HttpContext.Session.SetInt32("AlreadyLogged", 0);

            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Users = _context.Useraccounts.ToList();
                ViewBag.UserName = HttpContext.Session.GetString("FullName");
                ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
                ViewBag.UserId = HttpContext.Session.GetInt32("UserId");


                var userid = ViewBag.UserId;
                var Order = _context.Orderrs.ToList();
                var OrderProduct = _context.Orderproducts.ToList();
                List<decimal> orderid = new List<decimal>();
                foreach (var item in Order)
                   {
                    if (item.Userid == userid  && item.Status == "0")
                    {
                        orderid.Add(item.Orderid);
                    //    item.Userid = userid;
                    //    item.Status = "1";
                    //    item.Display = "0";
                    //_context.Update(item);

                    _context.Orderrs.Remove(item);
                    await _context.SaveChangesAsync();

                }
            }
                //List<Orderproduct> orderproduct = new List<Orderproduct>();
      
            foreach (var item in OrderProduct)
                {
                    foreach (var o in orderid)
                    {
                        if (item.Orderid == o)
                    { 
                        _context.Remove(item);
                        await _context.SaveChangesAsync();


                    }
                }
                }
            await _context.SaveChangesAsync();
            await DeOP();
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
          

        }

        [HttpPost]
        public async Task<IActionResult> DeOP()
        {
            foreach (var item in _context.Orderproducts)
            {
                if (item.Orderid == null)
                {
                    _context.Remove(item);
                    await _context.SaveChangesAsync();
                }

            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]

            public async Task<IActionResult> End()
        {

            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Users = _context.Useraccounts.ToList();
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");


            var userid = ViewBag.UserId;
            var Order = _context.Orderrs.ToList();
            var OrderProduct = _context.Orderproducts.ToList();
            List<decimal> orderid = new List<decimal>();
            foreach (var item in Order)
            {
                if (item.Userid == userid && item.Status == "1")
                {
                    orderid.Add(item.Orderid);
                    item.Userid = userid;
                    item.Display = "1";

                    _context.Update(item);

                }

            }
           await  _context.SaveChangesAsync();

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            var id = ViewBag.UserId;
            DateTime now = DateTime.Now;
            Orderr orderr = new Orderr();
            orderr.Userid = id;
            orderr.Status = "0";
            orderr.Display = "0";
            orderr.Dte = now;
            _context.Add(orderr);
     
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "UserDashboard");


        }












    }































}





