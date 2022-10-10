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
    public class TestmoninalsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _iwebHostEnvirounment;

        public TestmoninalsController(ModelContext context, IWebHostEnvironment iwebHostEnvirounment)
        {
            _context = context;
            _iwebHostEnvirounment = iwebHostEnvirounment;
        }

        // GET: Testmoninals
        public async Task<IActionResult> Index()
        {
            ViewBag.Users = _context.Useraccounts.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");


            var modelContext = _context.Testmoninals.Include(t => t.User);
            return View(await modelContext.ToListAsync());
        }

        public async Task<IActionResult> EditStatus()
        {
            ViewBag.Users = _context.Useraccounts.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            var modelContext = _context.Testmoninals.Include(t => t.User);
            return View(await modelContext.ToListAsync());
        }
        // GET: Testmoninals/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
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

            var testmoninal = await _context.Testmoninals
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Testmoninalid == id);
            if (testmoninal == null)
            {
                return NotFound();
            }

            return View(testmoninal);
        }

        // GET: Testmoninals/Create
        public IActionResult Create()
        {
            ViewBag.Users = _context.Useraccounts.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewData["Userid"] = new SelectList(_context.Useraccounts, "Userid", "Userid");
            return View();
        }

        // POST: Testmoninals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Testmoninalid,Message,Name,Testimage,Status,Userid")] Testmoninal testmoninal , IFormFile img)
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
                if (img != null)
                {

                    string wwwroot = _iwebHostEnvirounment.WebRootPath;
                    string filename = Guid.NewGuid() + "_" + img.FileName;
                    string path = Path.Combine(wwwroot + "/Images/Testimonial/", filename);

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {


                        img.CopyTo(filestream);
                        testmoninal.Testimage = filename;
                        testmoninal.Status = "R";

                        _context.Add(testmoninal);
                        await _context.SaveChangesAsync();
                    }


                }

                return RedirectToAction("Testimonials","UserDashboard");
            }
            ViewData["Userid"] = new SelectList(_context.Useraccounts, "Userid", "Userid", testmoninal.Userid);
            return View(testmoninal);
        }

        // GET: Testmoninals/Edit/5
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
            ViewBag.Testimonials = _context.Testmoninals.ToList();
            ViewBag.TestID = id;
            if (id == null)
            {
                return NotFound();
            }

            var testmoninal = await _context.Testmoninals.FindAsync(id);
            if (testmoninal == null)
            {
                return NotFound();
            }
            ViewData["Userid"] = new SelectList(_context.Useraccounts, "Userid", "Userid", testmoninal.Userid);
            return View(testmoninal);
        }

        // POST: Testmoninals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Testmoninalid,Message,Name,Testimage,Status,Userid")] Testmoninal testmoninal)
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
            if (id != testmoninal.Testmoninalid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(testmoninal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestmoninalExists(testmoninal.Testmoninalid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Testimonials","AdminDashboard");
            }
            ViewData["Userid"] = new SelectList(_context.Useraccounts, "Userid", "Userid", testmoninal.Userid);
            return View(testmoninal);
        }

        // GET: Testmoninals/Delete/5
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

            var testmoninal = await _context.Testmoninals
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Testmoninalid == id);
            if (testmoninal == null)
            {
                return NotFound();
            }

            return View(testmoninal);
        }


        public async Task<IActionResult> Remove(decimal? id)
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
            var testmoninal = await _context.Testmoninals.FindAsync(id);
            _context.Testmoninals.Remove(testmoninal);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","AdminDashboard");

        }
        public async Task<IActionResult> Remov(decimal? id)
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
            var testmoninal = await _context.Testmoninals.FindAsync(id);
            _context.Testmoninals.Remove(testmoninal);
            await _context.SaveChangesAsync();
            return RedirectToAction("Testimonials", "AdminDashboard");

        }
        // POST: Testmoninals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            ViewBag.Users = _context.Useraccounts.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            var testmoninal = await _context.Testmoninals.FindAsync(id);
            _context.Testmoninals.Remove(testmoninal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestmoninalExists(decimal id)
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
            return _context.Testmoninals.Any(e => e.Testmoninalid == id);
        }


        public async Task<IActionResult> _EditStatus(decimal? id)
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

            var testmoninal = await _context.Testmoninals.FindAsync(id);
            if (testmoninal == null)
            {
                return NotFound();
            }
            ViewData["Userid"] = new SelectList(_context.Useraccounts, "Userid", "Userid", testmoninal.Userid);
            return View(testmoninal);
        }

        // POST: Testmoninals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _EditStatus(decimal id, [Bind("Testmoninalid,Message,Name,Testimage,Status,Userid")] Testmoninal testmoninal)
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
            if (id != testmoninal.Testmoninalid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    testmoninal.Status = "A";

                    _context.Update(testmoninal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestmoninalExists(testmoninal.Testmoninalid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Testimonials","AdminDashboard");
            }
            ViewData["Userid"] = new SelectList(_context.Useraccounts, "Userid", "Userid", testmoninal.Userid);
            return View(testmoninal);
        }


    }

}
