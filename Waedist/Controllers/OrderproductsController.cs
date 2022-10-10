using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Waedist.Models;

namespace Waedist.Controllers
{
    public class OrderproductsController : Controller
    {
        private readonly ModelContext _context;

        public OrderproductsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Orderproducts
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
            var modelContext = _context.Orderproducts.Include(o => o.Order).Include(o => o.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: Orderproducts/Details/5
        public async Task<IActionResult> Details(decimal? id)
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
            if (id == null)
            {
                return NotFound();
            }

            var orderproduct = await _context.Orderproducts
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderproduct == null)
            {
                return NotFound();
            }

            return View(orderproduct);
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
        // GET: Orderproducts/Create
        public IActionResult Create()

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
            ViewBag.products = _context.Products.ToList();

            ViewData["Orderid"] = new SelectList(_context.Orderrs, "Orderid", "Orderid");
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid");
            return View();
        }


        // POST: Orderproducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind()] Orderproduct orderproduct ,Orderr order ,  decimal price)
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
            ViewBag.products = _context.Products.ToList();
            ViewBag.Userid = HttpContext.Session.GetInt32("UserId");

            DateTime Date = DateTime.Now;
            ViewBag.OrderDate = Date;
        
            if (ModelState.IsValid)
            {



                order.Dte = Date;
                order.Userid = ViewBag.Userid;
                order.Status = "0";
                _context.Add(order);
                await _context.SaveChangesAsync();
                decimal oid = (from r in _context.Orderrs orderby r.Orderid select r.Orderid).Max();
        ViewBag.OrderId = oid;
                orderproduct.Orderid = oid;
                orderproduct.Status = "0";
                orderproduct.Totalamount = price * orderproduct.Qty;               
                _context.Add(orderproduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
    
            return View(orderproduct);
        }




        //public async Task<IActionResult> Register([Bind("Id,Fname,Lname,Imagepath, ImageFile")] Customer customer, string pass, string username)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (customer.ImageFile != null)
        //        {
        //            //1- get w3rootpath
        //            string w3rootpath = webhostEnvironment.WebRootPath;
        //            //Guid.NewGuid : generate unique string before image name ;
        //            ////2- generate image name and add unique string
        //            string fileName = Guid.NewGuid().ToString() + "_" + customer.ImageFile.FileName; //ex : affscdw5635edvcywydfew_Aseel.jpg
        //                                                                                             //3- path
        //            string path = Path.Combine(w3rootpath + "/Images/" + fileName);
        //            //4-create Image inside image file in w3root folder
        //            using (var fileStream = new FileStream(path, FileMode.Create))
        //            {
        //                await customer.ImageFile.CopyToAsync(fileStream);
        //            }

        //            customer.Imagepath = fileName; //ex : affscdw5635edvcywydfew_Aseel.jpg stor in DB
        //            _context.Add(customer);
        //            await _context.SaveChangesAsync();
        //            //to store username+ password inside UserLogin table
        //            Userlogin userlogin = new Userlogin();
        //            userlogin.Password = pass;
        //            userlogin.Username = username;

        //            userlogin.Customerid = customer.Customerid;
        //            userlogin.Roleid = 1;
        //            _context.Add(userlogin);
        //            await _context.SaveChangesAsync();
        //        }

        //    }
        //    return View(customer);
        //}
















        // GET: Orderproducts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
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
            if (id == null)
            {
                return NotFound();
            }

            var orderproduct = await _context.Orderproducts.FindAsync(id);
            if (orderproduct == null)
            {
                return NotFound();
            }
            ViewData["Orderid"] = new SelectList(_context.Orderrs, "Orderid", "Orderid", orderproduct.Orderid);
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid", orderproduct.Productid);
            return View(orderproduct);
        }

        // POST: Orderproducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Orderid,Qty,Totalamount,Status,Productid")] Orderproduct orderproduct)
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
            if (id != orderproduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderproduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderproductExists(orderproduct.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Orderid"] = new SelectList(_context.Orderrs, "Orderid", "Orderid", orderproduct.Orderid);
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid", orderproduct.Productid);
            return View(orderproduct);
        }

        // GET: Orderproducts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
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
            if (id == null)
            {
                return NotFound();
            }

            var orderproduct = await _context.Orderproducts
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderproduct == null)
            {
                return NotFound();
            }

            return View(orderproduct);
        }

    
        // POST: Orderproducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
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
            return RedirectToAction(nameof(Index));
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
            return RedirectToAction("ShoppingCart","UserDashborad");
        }
        private bool OrderproductExists(decimal id)
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
            return _context.Orderproducts.Any(e => e.Id == id);
        }
    }
}
