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
    public class TransportServicesController : Controller
    {
        private readonly ApplicationContext _context;

        public TransportServicesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: TransportServices
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.TransportService.Include(t => t.Employees).Include(t => t.Routes).Include(t => t.TypeOfTransport);
            return View(await applicationContext.ToListAsync());
        }

        // GET: TransportServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transportService = await _context.TransportService
                .Include(t => t.Employees)
                .Include(t => t.Routes)
                .Include(t => t.TypeOfTransport)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (transportService == null)
            {
                return NotFound();
            }

            return View(transportService);
        }

        // GET: TransportServices/Create
        public IActionResult Create()
        {
            ViewData["EmployeesId"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["RoutesId"] = new SelectList(_context.Routes, "Id", "Id");
            ViewData["TypeOfTransportId"] = new SelectList(_context.TypeOfTransport, "Id", "Id");
            return View();
        }

        // POST: TransportServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeOfTransportId,RoutesId,EmployeesId,Date,Change")] TransportService transportService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transportService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeesId"] = new SelectList(_context.Employees, "Id", "Id", transportService.EmployeesId);
            ViewData["RoutesId"] = new SelectList(_context.Routes, "Id", "Id", transportService.RoutesId);
            ViewData["TypeOfTransportId"] = new SelectList(_context.TypeOfTransport, "Id", "Id", transportService.TypeOfTransportId);
            return View(transportService);
        }

        // GET: TransportServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transportService = await _context.TransportService.SingleOrDefaultAsync(m => m.Id == id);
            if (transportService == null)
            {
                return NotFound();
            }
            ViewData["EmployeesId"] = new SelectList(_context.Employees, "Id", "Id", transportService.EmployeesId);
            ViewData["RoutesId"] = new SelectList(_context.Routes, "Id", "Id", transportService.RoutesId);
            ViewData["TypeOfTransportId"] = new SelectList(_context.TypeOfTransport, "Id", "Id", transportService.TypeOfTransportId);
            return View(transportService);
        }

        // POST: TransportServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeOfTransportId,RoutesId,EmployeesId,Date,Change")] TransportService transportService)
        {
            if (id != transportService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transportService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransportServiceExists(transportService.Id))
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
            ViewData["EmployeesId"] = new SelectList(_context.Employees, "Id", "Id", transportService.EmployeesId);
            ViewData["RoutesId"] = new SelectList(_context.Routes, "Id", "Id", transportService.RoutesId);
            ViewData["TypeOfTransportId"] = new SelectList(_context.TypeOfTransport, "Id", "Id", transportService.TypeOfTransportId);
            return View(transportService);
        }

        // GET: TransportServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transportService = await _context.TransportService
                .Include(t => t.Employees)
                .Include(t => t.Routes)
                .Include(t => t.TypeOfTransport)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (transportService == null)
            {
                return NotFound();
            }

            return View(transportService);
        }

        // POST: TransportServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transportService = await _context.TransportService.SingleOrDefaultAsync(m => m.Id == id);
            _context.TransportService.Remove(transportService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransportServiceExists(int id)
        {
            return _context.TransportService.Any(e => e.Id == id);
        }
    }
}
