using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Waedist.Models;

namespace Waedist.Controllers
{
    public class OrderdelivariesController : Controller
    {
        private readonly ModelContext _context;

        public OrderdelivariesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Orderdelivaries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orderdelivaries.ToListAsync());
        }

        // GET: Orderdelivaries/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdelivary = await _context.Orderdelivaries
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (orderdelivary == null)
            {
                return NotFound();
            }

            return View(orderdelivary);
        }

        // GET: Orderdelivaries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orderdelivaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Orderid,Userid,Username,Useraddress,Useremail,Userphone,Shippingmethod,Postnumber")] Orderdelivary orderdelivary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderdelivary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderdelivary);
        }

        // GET: Orderdelivaries/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdelivary = await _context.Orderdelivaries.FindAsync(id);
            if (orderdelivary == null)
            {
                return NotFound();
            }
            return View(orderdelivary);
        }

        // POST: Orderdelivaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Orderid,Userid,Username,Useraddress,Useremail,Userphone,Shippingmethod,Postnumber")] Orderdelivary orderdelivary)
        {
            if (id != orderdelivary.Orderid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    _context.Update(orderdelivary);
                    await _context.SaveChangesAsync();
                
           
                return RedirectToAction(nameof(Index));
            }
            return View(orderdelivary);
        }

        // GET: Orderdelivaries/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdelivary = await _context.Orderdelivaries
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (orderdelivary == null)
            {
                return NotFound();
            }

            return View(orderdelivary);
        }

        // POST: Orderdelivaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var orderdelivary = await _context.Orderdelivaries.FindAsync(id);
            _context.Orderdelivaries.Remove(orderdelivary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderdelivaryExists(decimal id)
        {
            return _context.Orderdelivaries.Any(e => e.Orderid == id);
        }
    }
}
