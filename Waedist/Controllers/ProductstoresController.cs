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
    public class ProductstoresController : Controller
    {
        private readonly ModelContext _context;

        public ProductstoresController(ModelContext context)
        {
            _context = context;
        }

        // GET: Productstores
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Productstores.Include(p => p.Product).Include(p => p.Store);
            return View(await modelContext.ToListAsync());
        }

        // GET: Productstores/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productstore = await _context.Productstores
                .Include(p => p.Product)
                .Include(p => p.Store)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productstore == null)
            {
                return NotFound();
            }

            return View(productstore);
        }

        // GET: Productstores/Create
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
            ViewBag.stores = _context.Stores.ToList();


            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid");
            ViewData["Storeid"] = new SelectList(_context.Stores, "Storeid", "Storeid");
            return View();
        }

        // POST: Productstores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Productid,Storeid")] Productstore productstore)
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
            if (ModelState.IsValid)
            {
                _context.Add(productstore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid", productstore.Productid);
            ViewData["Storeid"] = new SelectList(_context.Stores, "Storeid", "Storeid", productstore.Storeid);
            return View(productstore);
        }

        // GET: Productstores/Edit/5
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

            var productstore = await _context.Productstores.FindAsync(id);
            if (productstore == null)
            {
                return NotFound();
            }
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid", productstore.Productid);
            ViewData["Storeid"] = new SelectList(_context.Stores, "Storeid", "Storeid", productstore.Storeid);
            return View(productstore);
        }

        // POST: Productstores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Productid,Storeid")] Productstore productstore)
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
            if (id != productstore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productstore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductstoreExists(productstore.Id))
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
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid", productstore.Productid);
            ViewData["Storeid"] = new SelectList(_context.Stores, "Storeid", "Storeid", productstore.Storeid);
            return View(productstore);
        }

        // GET: Productstores/Delete/5
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

            var productstore = await _context.Productstores
                .Include(p => p.Product)
                .Include(p => p.Store)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productstore == null)
            {
                return NotFound();
            }

            return View(productstore);
        }

        // POST: Productstores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
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
            var productstore = await _context.Productstores.FindAsync(id);
            _context.Productstores.Remove(productstore);
            await _context.SaveChangesAsync();
            return RedirectToAction("ProductsStores", "AdminDashboard");
      
        }

        private bool ProductstoreExists(decimal id)
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
            return _context.Productstores.Any(e => e.Id == id);
        }
    }
}
