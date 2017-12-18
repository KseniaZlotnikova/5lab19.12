using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UrbanTraffic;
using UrbanTraffic.Models;

namespace UrbanTraffic.Controllers.TableControler
{
    public class StoppingsController : Controller
    {
        private readonly ApplicationContext _context;

        public StoppingsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Stoppings
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Stopping.Include(s => s.TypeOfTransport);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Stoppings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stopping = await _context.Stopping
                .Include(s => s.TypeOfTransport)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stopping == null)
            {
                return NotFound();
            }

            return View(stopping);
        }

        // GET: Stoppings/Create
        public IActionResult Create()
        {
            ViewData["TypeOfTransportId"] = new SelectList(_context.TypeOfTransport, "Id", "Id");
            return View();
        }

        // POST: Stoppings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TypeOfTransportId,EndingStation,ControlRoom")] Stopping stopping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stopping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeOfTransportId"] = new SelectList(_context.TypeOfTransport, "Id", "Id", stopping.TypeOfTransportId);
            return View(stopping);
        }

        // GET: Stoppings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stopping = await _context.Stopping.SingleOrDefaultAsync(m => m.Id == id);
            if (stopping == null)
            {
                return NotFound();
            }
            ViewData["TypeOfTransportId"] = new SelectList(_context.TypeOfTransport, "Id", "Id", stopping.TypeOfTransportId);
            return View(stopping);
        }

        // POST: Stoppings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TypeOfTransportId,EndingStation,ControlRoom")] Stopping stopping)
        {
            if (id != stopping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stopping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoppingExists(stopping.Id))
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
            ViewData["TypeOfTransportId"] = new SelectList(_context.TypeOfTransport, "Id", "Id", stopping.TypeOfTransportId);
            return View(stopping);
        }

        // GET: Stoppings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stopping = await _context.Stopping
                .Include(s => s.TypeOfTransport)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stopping == null)
            {
                return NotFound();
            }

            return View(stopping);
        }

        // POST: Stoppings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stopping = await _context.Stopping.SingleOrDefaultAsync(m => m.Id == id);
            _context.Stopping.Remove(stopping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoppingExists(int id)
        {
            return _context.Stopping.Any(e => e.Id == id);
        }
    }
}
