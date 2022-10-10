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
    public class CategoriesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _iwebHostEnvirounment;
    
        public CategoriesController(ModelContext context , IWebHostEnvironment iwebHostEnvirounment)
        {
            _context = context;
            _iwebHostEnvirounment = iwebHostEnvirounment;
        }

        // GET: Categories
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
            return View(await _context.Categories.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string? category)
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

            var cat = await _context.Categories.ToListAsync();
            if (category != null)
            {
                var result = cat.Where(x => x.Categoryname.ToUpper().Contains(category.ToUpper()));
                return View(result);
            }
            return View(cat);
        }
        public async Task<IActionResult> UserIndex(  )
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
            return View(await _context.Categories.ToListAsync());
        }




    
        [HttpPost]
        public async Task<IActionResult> UserIndex(string? category)
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

            var cat = await _context.Categories.ToListAsync();
            if (category != null)
            {
                var result = cat.Where(x => x.Categoryname.ToUpper().Contains(category.ToUpper()));
                return View(result);
            }
            return View(cat);
        }










        public async Task<IActionResult> AdminIndex()
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
            return View(await _context.Categories.ToListAsync());
        }

        public async Task<IActionResult> ManageCategories()
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
            return View(await _context.Categories.ToListAsync());
        }
    

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(decimal? id)
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

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Categoryid == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
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
            ViewBag.Categries = _context.Categories.ToList();
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Categoryid,Categoryname,Pic")] Category category , IFormFile img)
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
                if (img != null)
                {

                    string wwwroot = _iwebHostEnvirounment.WebRootPath;
                    string filename = Guid.NewGuid() + "_" + img.FileName;
                    string path = Path.Combine(wwwroot + "/Images/Category/", filename);

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {


                        img.CopyTo(filestream);
                        category.Pic = filename;
                        _context.Add(category);
                        await _context.SaveChangesAsync();
                    }
                 
                    return RedirectToAction(nameof(Index));

                }

            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(decimal? id  )
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
            ViewBag.CatId = id;
            ViewBag.Categries = _context.Categories.ToList();
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Categoryid,Categoryname,Pic")] Category category,IFormFile img)
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
            if (id != category.Categoryid)
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
                        string path = Path.Combine(wwwroot + "/Images/Category/", filename);

                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            img.CopyTo(filestream);
                            category.Pic = filename;
                        }
                        category.Pic = filename;


                    }

                        _context.Update(category);
                        await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Categoryid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Categories");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
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
            ViewBag.CatId = id;
            ViewBag.Categries = _context.Categories.ToList();
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Categoryid == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
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
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(decimal id)
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
            return _context.Categories.Any(e => e.Categoryid == id);
        }


       

    }
}
