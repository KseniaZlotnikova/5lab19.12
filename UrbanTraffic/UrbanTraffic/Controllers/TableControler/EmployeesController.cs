using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UrbanTraffic;
using UrbanTraffic.Models;
using UrbanTraffic.ViewModel;
using UrbanTraffic.ViewModel.Filter;

namespace UrbanTraffic.Controllers.TableControler
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationContext _context;

        public EmployeesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Employees
        public IActionResult Index(string Name, string Surname, string MiddleName,int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 10;

            IQueryable<Employees> source = _context.Employees;

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["MiddleNameSort"] = sortOrder == SortState.MiddleNameAsc ? SortState.MiddleNameDesc : SortState.MiddleNameAsc;
            ViewData["SurnameSort"] = sortOrder == SortState.SurnameAsc ? SortState.SurnameDesc : SortState.SurnameAsc;

            if (Name != null)
            {
                source = source.Where(x => x.Name == Name);
            }
            if (Surname != null)
            {
                source = source.Where(x => x.Surname == Surname);
            }
            if (MiddleName != null)
            {
                source = source.Where(x => x.MiddleName == MiddleName);
            }

            switch (sortOrder)
            {
                case SortState.NameAsc:
                    source = source.OrderBy(x => x.Name);
                    break;
                case SortState.NameDesc:
                    source = source.OrderByDescending(x => x.Name);
                    break;
                case SortState.MiddleNameAsc:
                    source = source.OrderBy(x => x.MiddleName);
                    break;
                case SortState.MiddleNameDesc:
                    source = source.OrderByDescending(x => x.MiddleName);
                    break;
                case SortState.SurnameAsc:
                    source = source.OrderBy(x => x.Surname);
                    break;
                case SortState.SurnameDesc:
                    source = source.OrderByDescending(x => x.Surname);
                    break;
            }


            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);

            IndexViewModel ivm = new IndexViewModel
            {
                PageViewModel = pageView,
                FilterEmployeesViewModel = new FilterEmployeesViewModel(Name,Surname,MiddleName),
                Employees = items
            };
            return View(ivm);

        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .SingleOrDefaultAsync(m => m.Id == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Surname,Name,MiddleName,Position,WorkExperience")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employees);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees.SingleOrDefaultAsync(m => m.Id == id);
            if (employees == null)
            {
                return NotFound();
            }
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Surname,Name,MiddleName,Position,WorkExperience")] Employees employees)
        {
            if (id != employees.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.Id))
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
            return View(employees);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .SingleOrDefaultAsync(m => m.Id == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employees = await _context.Employees.SingleOrDefaultAsync(m => m.Id == id);
            _context.Employees.Remove(employees);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
