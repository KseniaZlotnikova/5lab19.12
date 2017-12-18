using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UrbanTraffic;
using UrbanTraffic.Models;
using UrbanTraffic.ViewModel.Account;
using UrbanTraffic.ViewModel;

namespace UrbanTraffic.Controllers
{
    public class RoutesController : Controller
    {
        private readonly ApplicationContext _context;

        public RoutesController(ApplicationContext context)
        {
            _context = context;
        }


        // GET: Employees
        public IActionResult Index(string Name, float TravelTime, float Distance, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 10;

            IQueryable<Routes> source = _context.Routes.Include(r => r.Stopping);



            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["TravelTimeSort"] = sortOrder == SortState.TravelTimeAsc ? SortState.TravelTimeDesc : SortState.TravelTimeAsc;
            ViewData["DistanceSort"] = sortOrder == SortState.DistanceAsc ? SortState.DistanceDesc : SortState.DistanceAsc;

            if (Name != null)
            {
                source = source.Where(x => x.Stopping.Name == Name);
            }
            if (Name != null)
            {
                source = source.Where(x => x.TravelTime == TravelTime);
            }
            if (Name != null)
            {
                source = source.Where(x => x.Distance == Distance);
            }

            switch (sortOrder)
            {
                case SortState.NameAsc:
                    source = source.OrderBy(x => x.Stopping.Name);
                    break;
                case SortState.NameDesc:
                    source = source.OrderByDescending(x => x.Stopping.Name);
                    break;
                case SortState.TravelTimeAsc:
                    source = source.OrderBy(x => x.TravelTime);
                    break;
                case SortState.TravelTimeDesc:
                    source = source.OrderByDescending(x => x.TravelTime);
                    break;
                case SortState.DistanceAsc:
                    source = source.OrderBy(x => x.Distance);
                    break;
                case SortState.DistanceDesc:
                    source = source.OrderByDescending(x => x.Distance);
                    break;
            }


            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);

            IndexViewModel ivm = new IndexViewModel
            {
                PageViewModel = pageView,
                FilterRoutesViewModel = new FilterRoutesViewModel(Name,TravelTime, Distance),
                Routes = items
            };
            return View(ivm);

        }


        // GET: Routes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routes = await _context.Routes
                .Include(r => r.Stopping)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (routes == null)
            {
                return NotFound();
            }

            return View(routes);
        }

        // GET: Routes/Create
        public IActionResult Create()
        {
            ViewData["StoppingId"] = new SelectList(_context.Stopping, "Id", "Name");
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StoppingId,TravelTime,Distance,ExpressRoute")] Routes routes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(routes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoppingId"] = new SelectList(_context.Stopping, "Id", "Name", routes.StoppingId);
            return View(routes);
        }

        // GET: Routes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routes = await _context.Routes.SingleOrDefaultAsync(m => m.Id == id);
            if (routes == null)
            {
                return NotFound();
            }
            ViewData["StoppingId"] = new SelectList(_context.Stopping, "Id", "Name", routes.StoppingId);
            return View(routes);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StoppingId,TravelTime,Distance,ExpressRoute")] Routes routes)
        {
            if (id != routes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoutesExists(routes.Id))
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
            ViewData["StoppingId"] = new SelectList(_context.Stopping, "Id", "Name", routes.StoppingId);
            return View(routes);
        }

        // GET: Routes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routes = await _context.Routes
                .Include(r => r.Stopping)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (routes == null)
            {
                return NotFound();
            }

            return View(routes);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var routes = await _context.Routes.SingleOrDefaultAsync(m => m.Id == id);
            _context.Routes.Remove(routes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoutesExists(int id)
        {
            return _context.Routes.Any(e => e.Id == id);
        }
    }
}
