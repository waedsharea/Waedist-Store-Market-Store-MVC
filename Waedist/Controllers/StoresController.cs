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
    public class StoresController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _iwebHostEnvirounment;

        public StoresController(ModelContext context, IWebHostEnvironment iwebHostEnvirounment)
        {
            _context = context;
            _iwebHostEnvirounment = iwebHostEnvirounment;
        }

        // GET: Stores
        public async Task<IActionResult> Index()
        {
            ViewBag.Users = _context.Useraccounts.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            return View(await _context.Stores.ToListAsync());

        }
        [HttpPost]
        public async Task<IActionResult> Index(string? store)
        {
            ViewBag.Users = _context.Useraccounts.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            var S = await _context.Stores.Include(s => s.Category).ToListAsync();
            if (store != null)
            {
                var result = S.Where(x => x.Storename.ToUpper().Contains(store.ToUpper()));
                return View(result);
            }
           
            return View(S);
                        
        }

        public async Task<IActionResult> UserIndex(int? id)

        {

            var store = _context.Stores.Where(x => x.Categoryid == id);
            ViewBag.Users = _context.Useraccounts.ToList();
            var S = await _context.Stores.ToListAsync();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            if (id != null)
                return View(store);
            else
                return View(S);
        }
        [HttpPost]
        public async Task<IActionResult> UserIndex(string? store)
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

            var S = await _context.Stores.Include(s => s.Category).ToListAsync();
            if (store != null)
            {
                var result = S.Where(x => x.Storename.ToUpper().Contains(store.ToUpper()));
                return View(result);
            }
            return View(S);
        }
        public async Task<IActionResult> ViewStores()
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
            return View(await _context.Stores.ToListAsync());

        }
      
        public async Task<IActionResult> ManageStores()
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
            return View(await _context.Stores.ToListAsync());
        }

        // GET: Stores/Details/5
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

            var store = await _context.Stores
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Storeid == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Stores/Create
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
            ViewBag.CategoriesName = _context.Categories.ToList();

            ViewData["Categoryid"] = new SelectList(_context.Categories, "Categoryid", "Categoryid" );
            ViewData["Revid"] = new SelectList(_context.Reviews, "Reviewid", "Reviewid");
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Storeid,Storename,Categoryid,Smallpic,Headpic,Text,Revid,Totalsales,Monthlyrent")] Store store , IFormFile img ) 
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

                if (ModelState.IsValid)
                {
                    if (img != null)
                    {

                        string wwwroot = _iwebHostEnvirounment.WebRootPath;
                        string filename = Guid.NewGuid() + "_" + img.FileName;
                        string path = Path.Combine(wwwroot + "/Images/Store/", filename);

                        using (var filestream = new FileStream(path, FileMode.Create))
                        {


                            img.CopyTo(filestream);
                            store.Headpic = filename;
                            store.Totalsales = 0;

                            _context.Add(store);
                            await _context.SaveChangesAsync();
                        }

                        return RedirectToAction(nameof(Index));

                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["Categoryid"] = new SelectList(_context.Categories, "Categoryid", "Categoryid", store.Categoryid);
            return View(store);
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
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
            ViewBag.StoreId = id;
            ViewBag.Store = _context.Stores.ToList();
            ViewBag.CategoriesName = _context.Categories.ToList();

            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            ViewData["Categoryid"] = new SelectList(_context.Categories, "Categoryid", "Categoryid", store.Categoryid);
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Storeid,Storename,Categoryid,Smallpic,Headpic,Text,Revid,Totalsales,Monthlyrent")] Store store, IFormFile img)
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


            if (id != store.Storeid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (img != null )
                    {

                        string wwwroot = _iwebHostEnvirounment.WebRootPath;
                        string filenamel = Guid.NewGuid() + "_" + img.FileName;

                        string path = Path.Combine(wwwroot + "/Images/Store", filenamel);

                        using (var filestream = new FileStream(path, FileMode.Create))
                        {


                            img.CopyTo(filestream);

                            store.Headpic = filenamel;

                        }
                    }
                        _context.Update(store);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.Storeid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Stores");
            }
            ViewData["Categoryid"] = new SelectList(_context.Categories, "Categoryid", "Categoryid", store.Categoryid);
            return View(store);
        }

        // GET: Stores/Delete/5
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

            var store = await _context.Stores
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Storeid == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Stores/Delete/5
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
            var store = await _context.Stores.FindAsync(id);
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(decimal id)
        {
            return _context.Stores.Any(e => e.Storeid == id);
        }
    }
}
