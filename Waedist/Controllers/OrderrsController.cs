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
    public class OrderrsController : Controller
    {
        private readonly ModelContext _context;

        public OrderrsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Orderrs
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Orderrs.Include(o => o.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Orderrs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderr = await _context.Orderrs
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (orderr == null)
            {
                return NotFound();
            }

            return View(orderr);
        }

        // GET: Orderrs/Create
  

        // GET: Orderrs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderr = await _context.Orderrs.FindAsync(id);
            if (orderr == null)
            {
                return NotFound();
            }
            ViewData["Userid"] = new SelectList(_context.Useraccounts, "Userid", "Userid", orderr.Userid);
            return View(orderr);
        }

        // POST: Orderrs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Orderid,Userid,Dte,Status")] Orderr orderr)
        {
            if (id != orderr.Orderid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderrExists(orderr.Orderid))
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
            ViewData["Userid"] = new SelectList(_context.Useraccounts, "Userid", "Userid", orderr.Userid);
            return View(orderr);
        }

        // GET: Orderrs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderr = await _context.Orderrs
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (orderr == null)
            {
                return NotFound();
            }

            return View(orderr);
        }

        // POST: Orderrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var orderr = await _context.Orderrs.FindAsync(id);
            _context.Orderrs.Remove(orderr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderrExists(decimal id)
        {
            return _context.Orderrs.Any(e => e.Orderid == id);
        }
    }
}
