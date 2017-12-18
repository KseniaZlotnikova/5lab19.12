using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UrbanTraffic;
using UrbanTraffic.Models;

namespace UrbanTraffic.Controllers
{
    public class TypeOfTransportsController : Controller
    {
        private readonly ApplicationContext _context;

        public TypeOfTransportsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: TypeOfTransports
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeOfTransport.ToListAsync());
        }

        // GET: TypeOfTransports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfTransport = await _context.TypeOfTransport
                .SingleOrDefaultAsync(m => m.Id == id);
            if (typeOfTransport == null)
            {
                return NotFound();
            }

            return View(typeOfTransport);
        }

        // GET: TypeOfTransports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeOfTransports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TypeOfTransport typeOfTransport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeOfTransport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfTransport);
        }

        // GET: TypeOfTransports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfTransport = await _context.TypeOfTransport.SingleOrDefaultAsync(m => m.Id == id);
            if (typeOfTransport == null)
            {
                return NotFound();
            }
            return View(typeOfTransport);
        }

        // POST: TypeOfTransports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TypeOfTransport typeOfTransport)
        {
            if (id != typeOfTransport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeOfTransport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeOfTransportExists(typeOfTransport.Id))
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
            return View(typeOfTransport);
        }

        // GET: TypeOfTransports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfTransport = await _context.TypeOfTransport
                .SingleOrDefaultAsync(m => m.Id == id);
            if (typeOfTransport == null)
            {
                return NotFound();
            }

            return View(typeOfTransport);
        }

        // POST: TypeOfTransports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeOfTransport = await _context.TypeOfTransport.SingleOrDefaultAsync(m => m.Id == id);
            _context.TypeOfTransport.Remove(typeOfTransport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeOfTransportExists(int id)
        {
            return _context.TypeOfTransport.Any(e => e.Id == id);
        }
    }
}
