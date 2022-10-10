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
    public class UseraccountsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _iwebHostEnvirounment;

        public UseraccountsController(ModelContext context, IWebHostEnvironment iwebHostEnvirounment)
        {
            _context = context;
            _iwebHostEnvirounment = iwebHostEnvirounment;
        }

        // GET: Useraccounts
        public async Task<IActionResult> Index()
        {
            ViewBag.Users = _context.Useraccounts.ToList();
            var modelContext = _context.Useraccounts.Include(u => u.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: Useraccounts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var useraccount = await _context.Useraccounts
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (useraccount == null)
            {
                return NotFound();
            }

            return View(useraccount);
        }

        // GET: Useraccounts/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid");
            return View();
        }

        // POST: Useraccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Fullname,Phonenumber,Pic,Email,Password,Roleid")] Useraccount useraccount, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                if (img != null)
                {

                    string wwwroot = _iwebHostEnvirounment.WebRootPath;
                    string filename = Guid.NewGuid() + "_" + img.FileName;
                    string path = Path.Combine(wwwroot + "/Images/", filename);

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {

                        img.CopyTo(filestream);
                        useraccount.Pic = filename;
                        useraccount.Roleid = 2;
                        _context.Add(useraccount);
                        await _context.SaveChangesAsync();

                    }


                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", useraccount.Roleid);
            return View(useraccount);
        }

        // GET: Useraccounts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.flag = HttpContext.Session.GetInt32("IncorrectPass");

            ViewBag.Users = _context.Useraccounts.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Users = _context.Useraccounts.ToList();

            if (id == null)
            {
                return NotFound();
            }

            var useraccount = await _context.Useraccounts.FindAsync(id);
            if (useraccount == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", useraccount.Roleid);
            return View(useraccount);
        }

        // POST: Useraccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Userid,Fullname,Phonenumber,Pic,Email,Password,Roleid")] Useraccount useraccount, IFormFile img, string? Old, string? password)
        {
            if (password != null && Old != null)
            {
                HttpContext.Session.SetInt32("IncorrectPass", 2);

                var user = AdminPass(id, Old, password);
                return RedirectToAction(nameof(Edit));
                return View(user);
            }


            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");

            if (id != useraccount.Userid)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {



                HttpContext.Session.SetInt32("IncorrectPass", 2);



                try
                {
                    HttpContext.Session.SetInt32("IncorrectPass", 2);

                    if (img != null)
                    {
                        string wwwroot = _iwebHostEnvirounment.WebRootPath;
                        string filename = Guid.NewGuid() + "_" + img.FileName;
                        string path = Path.Combine(wwwroot + "/Images/", filename);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            img.CopyTo(filestream);
                            useraccount.Pic = filename;
                        }

               
                        HttpContext.Session.SetInt32("IncorrectPass", 2);

                    }

                    _context.Update(useraccount);
                    await _context.SaveChangesAsync();
                    ViewBag.Users = _context.Useraccounts.ToList();

                    ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
                    ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UseraccountExists(useraccount.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            

                return RedirectToAction(nameof(Edit));



            }

            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", useraccount.Roleid);
            return View(useraccount);

        }
        public async Task<IActionResult> AdminPass(decimal id, string Old, string password)
        {
            ViewBag.Users = _context.Useraccounts.ToList();
            var pass = _context.Useraccounts.Where(x => x.Userid == id).SingleOrDefault();


            if (pass.Password == Old)
            {
                HttpContext.Session.SetInt32("IncorrectPass", 0);
                ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
                ViewBag.Users = _context.Useraccounts.ToList();


                pass.Password = password;
                _context.Update(pass);
                await _context.SaveChangesAsync();


            }
            else if (pass.Password != Old)
            {
                HttpContext.Session.SetInt32("IncorrectPass", 1);
                ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
                ViewBag.Users = _context.Useraccounts.ToList();



            }
            await _context.SaveChangesAsync();




            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", pass.Roleid);
            return View(pass);

        }








        public async Task<IActionResult> UserProfile(decimal? id)
        {
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");
            ViewBag.flag = HttpContext.Session.GetInt32("IncorrectPass");

            ViewBag.Users = _context.Useraccounts.ToList();

            ViewBag.Adminid = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminN = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImg = HttpContext.Session.GetString("Image");
            ViewBag.UserName = HttpContext.Session.GetString("FullName");
            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Users = _context.Useraccounts.ToList();

            if (id == null)
            {
                return NotFound();
            }

            var useraccount = await _context.Useraccounts.FindAsync(id);
            if (useraccount == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", useraccount.Roleid);
            return View(useraccount);
        }

        // POST: Useraccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserProfile(decimal id, [Bind("Userid,Fullname,Phonenumber,Pic,Email,Password,Roleid")] Useraccount useraccount, IFormFile img, string? Old, string? password)
        {
            HttpContext.Session.SetInt32("IncorrectPass",5);

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");

            if (password != null && Old != null)
            {
                HttpContext.Session.SetInt32("IncorrectPass", 2);

                var user = AdminPass(id, Old, password);
                return RedirectToAction(nameof(UserProfile));
                return View(user);
            }


            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
            ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");

            if (id != useraccount.Userid)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {



                HttpContext.Session.SetInt32("IncorrectPass", 2);



                try
                {
                    HttpContext.Session.SetInt32("IncorrectPass", 2);

                    if (img != null)
                    {
                        string wwwroot = _iwebHostEnvirounment.WebRootPath;
                        string filename = Guid.NewGuid() + "_" + img.FileName;
                        string path = Path.Combine(wwwroot + "/Images/", filename);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            img.CopyTo(filestream);
                            useraccount.Pic = filename;
                        }


                        HttpContext.Session.SetInt32("IncorrectPass", 2);

                    }

                    _context.Update(useraccount);
                    await _context.SaveChangesAsync();
                    ViewBag.Users = _context.Useraccounts.ToList();

                    ViewBag.Roleid = HttpContext.Session.GetInt32("Roleid");
                    ViewBag.Logged = HttpContext.Session.GetInt32("AlredyLogged");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UseraccountExists(useraccount.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }


                return RedirectToAction(nameof(UserProfile));



            }

            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", useraccount.Roleid);
            return View(useraccount);

        }
        public async Task<IActionResult> UserPass(decimal id, string Old, string password )
        {
            ViewBag.Users = _context.Useraccounts.ToList();
            var pass = _context.Useraccounts.Where(x => x.Userid == id).SingleOrDefault();
            if (pass.Password == Old)
            {
                HttpContext.Session.SetInt32("IncorrectPass", 0);
                ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
                ViewBag.Users = _context.Useraccounts.ToList();


                pass.Password = password;
                _context.Update(pass);
                await _context.SaveChangesAsync();


            }
            else if (pass.Password != Old)
            {
                HttpContext.Session.SetInt32("IncorrectPass", 1);



            }




            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", pass.Roleid);
            return View(pass);

        } 


        // GET: Useraccounts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var useraccount = await _context.Useraccounts
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (useraccount == null)
            {
                return NotFound();
            }

            return View(useraccount);
        }

        // POST: Useraccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var useraccount = await _context.Useraccounts.FindAsync(id);
            _context.Useraccounts.Remove(useraccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UseraccountExists(decimal id)
        {
            return _context.Useraccounts.Any(e => e.Userid == id);
        }
    }
}
