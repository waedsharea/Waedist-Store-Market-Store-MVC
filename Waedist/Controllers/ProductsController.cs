using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Waedist.Models;

namespace Waedist.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _iwebHostEnvirounment;

        public ProductsController(ModelContext context, IWebHostEnvironment iwebHostEnvirounment)
        {
            _context = context;
            _iwebHostEnvirounment = iwebHostEnvirounment;
        }


        public IActionResult JStoreProduct(int? id)
        {
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.filter = id;
            ViewBag.Users = _context.Useraccounts.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

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

            return View();
 
        }


        [HttpPost]
        public async Task<IActionResult> JStoreProduct(string? pro, int? id)
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



            if (pro != null)
            {
                var result = join.Where(x => x.product.Productname.ToUpper().Contains(pro.ToUpper()));
                return View(result);
            }
            return View(join);

        }





        public async Task<IActionResult> Index()
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



            return View(await _context.Products.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string? product)
        {
            ViewBag.Users = _context.Useraccounts.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            var prod = await _context.Products.ToListAsync();
            if (product != null)
            {
                var result = prod.Where(x => x.Productname.ToUpper().Contains(product.ToUpper()));
                return View(result);
            }
            return View(prod);

        }

            // GET: Products/Details/5
            public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Productid == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.StoresName = _context.Stores.ToList();
            ViewBag.Users = _context.Useraccounts.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            ViewData["Storeid"] = new SelectList(_context.Stores, "StoreName", "Storeid");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Productid,Productname,Price,Productvalue,Pic,Storeid,Productsize")] Product product ,IFormFile img  )
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {

                        string wwwroot = _iwebHostEnvirounment.WebRootPath;
                        string filename = Guid.NewGuid() + "_" + img.FileName;
                        string path = Path.Combine(wwwroot + "/Images/Products/", filename);

                        using (var filestream = new FileStream(path, FileMode.Create))
                        {


                            img.CopyTo(filestream);
                            product.Pic = filename;
                            _context.Add(product);
                            await _context.SaveChangesAsync();
                        }

                        return RedirectToAction(nameof(Index));

                    }

                }

            }
            return View(product);
        }

        // GET: Products/Edit/5
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
            ViewBag.productid = id;
            ViewBag.products = _context.Products.ToList();
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Productid,Productname,Price,Productvalue,Pic,Storeid,Productsize")] Product product , IFormFile img)
        {
            if (id != product.Productid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (img != null)
                    {

                        string wwwroot = _iwebHostEnvirounment.WebRootPath;
                        string filename = Guid.NewGuid() + "_" + img.FileName;

                        string path = Path.Combine(wwwroot + "/Images/Products", filename);

                        using (var filestream = new FileStream(path, FileMode.Create))
                        {


                            img.CopyTo(filestream);

                            product.Pic = filename;

                        }
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Productid))
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
            return View(product);
        }

        // GET: Products/Delete/5
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

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Productid == id);
            var products = _context.Productstores.Where(x => x.Productid == id).ToList();
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.Users = _context.Useraccounts.ToList();
            var orderproducts = _context.Orderproducts.ToList();
            var productstores = _context.Productstores.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            var product = await _context.Products.FindAsync(id);
            if (product!=null)
            _context.Products.Remove(product);
             await _context.SaveChangesAsync();
            var products = _context.Productstores.Where(x => x.Productid == id).ToList();

            foreach (var item in products)
            {
                _context.Productstores.Remove(item);
                await _context.SaveChangesAsync();

            }


            return RedirectToAction(nameof(Index));
        }


        private bool ProductExists(decimal id)
        {
            return _context.Products.Any(e => e.Productid == id);
        }

      
    }
  
}

