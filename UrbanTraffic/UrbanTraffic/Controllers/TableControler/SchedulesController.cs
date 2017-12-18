using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UrbanTraffic.Models;
using UrbanTraffic.ViewModel.Filter;
using UrbanTraffic.ViewModel;
using UrbanTraffic;

namespace UrbanTraffic.Controllers.TableControler
{
    public class SchedulesController : Controller
    {
        private readonly ApplicationContext _context;

        public SchedulesController(ApplicationContext context)
        {
            _context = context;
        }


        public IActionResult Index(string Name, float ArrivaTime, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 10;

            IQueryable<Schedule> source = _context.Schedule.Include(s => s.TypeOfTransport);

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["ArrivaTimeSort"] = sortOrder == SortState.ArrivaTimeAsc ? SortState.ArrivaTimeDesc : SortState.ArrivaTimeAsc;


            if (Name != null)
            {
                source = source.Where(x => x.TypeOfTransport.Name == Name);
            }
            if (Name != null)
            {

                source = source.Where(x => x.ArrivaTime == ArrivaTime);
            }

            switch (sortOrder)
            {
                case SortState.NameAsc:
                    source = source.OrderBy(x => x.TypeOfTransport.Name);
                    break;
                case SortState.NameDesc:
                    source = source.OrderByDescending(x => x.TypeOfTransport.Name);
                    break;
                case SortState.ArrivaTimeAsc:
                    source = source.OrderBy(x => x.ArrivaTime);
                    break;
                case SortState.ArrivaTimeDesc:
                    source = source.OrderByDescending(x => x.ArrivaTime);
                    break;
            }


            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);

            IndexViewModel ivm = new IndexViewModel
            {
                PageViewModel = pageView,
                FilterSchedulesViewModel = new FilterSchedulesViewModel(Name,ArrivaTime),
                Schedule = items
            };
            return View(ivm);

        }



        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .Include(s => s.TypeOfTransport)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewData["TypeOfTransportId"] = new SelectList(_context.TypeOfTransport, "Id", "Name");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeOfTransportId,ArrivaTime,DayOfTheWeek,Year")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeOfTransportId"] = new SelectList(_context.TypeOfTransport, "Id", "Name", schedule.TypeOfTransportId);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule.SingleOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["TypeOfTransportId"] = new SelectList(_context.TypeOfTransport, "Id", "Name", schedule.TypeOfTransportId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeOfTransportId,ArrivaTime,DayOfTheWeek,Year")] Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
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
            ViewData["TypeOfTransportId"] = new SelectList(_context.TypeOfTransport, "Id", "Name", schedule.TypeOfTransportId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .Include(s => s.TypeOfTransport)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedule.SingleOrDefaultAsync(m => m.Id == id);
            _context.Schedule.Remove(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedule.Any(e => e.Id == id);
        }
    }
}
