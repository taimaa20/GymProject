using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication24.Models;

namespace WebApplication24.Controllers
{
    public class EmplyeeJobsController : Controller
    {
        private readonly GymContext _context;

        public EmplyeeJobsController(GymContext context)
        {
            _context = context;
        }

        // GET: EmplyeeJobs
        public async Task<IActionResult> Index()
        {
            var gymContext = _context.EmplyeeJob.Include(e => e.Employee).Include(e => e.Task);
            return View(await gymContext.ToListAsync());
        }

        // GET: EmplyeeJobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emplyeeJob = await _context.EmplyeeJob
                .Include(e => e.Employee)
                .Include(e => e.Task)
                .FirstOrDefaultAsync(m => m.EmplyeeJobId == id);
            if (emplyeeJob == null)
            {
                return NotFound();
            }

            return View(emplyeeJob);
        }

        // GET: EmplyeeJobs/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fname");
            ViewData["TaskId"] = new SelectList(_context.Task, "TaskId", "NameOfTask");
            return View();
        }

        // POST: EmplyeeJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmplyeeJobId,EmployeeId,TaskId")] EmplyeeJob emplyeeJob)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emplyeeJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fname", emplyeeJob.EmployeeId);
            ViewData["TaskId"] = new SelectList(_context.Task, "TaskId", "NameOfTask", emplyeeJob.TaskId);
            return View(emplyeeJob);
        }

        // GET: EmplyeeJobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emplyeeJob = await _context.EmplyeeJob.FindAsync(id);
            if (emplyeeJob == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fname", emplyeeJob.EmployeeId);
            ViewData["TaskId"] = new SelectList(_context.Task, "TaskId", "NameOfTask", emplyeeJob.TaskId);
            return View(emplyeeJob);
        }

        // POST: EmplyeeJobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmplyeeJobId,EmployeeId,TaskId")] EmplyeeJob emplyeeJob)
        {
            if (id != emplyeeJob.EmplyeeJobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emplyeeJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmplyeeJobExists(emplyeeJob.EmplyeeJobId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fname", emplyeeJob.EmployeeId);
            ViewData["TaskId"] = new SelectList(_context.Task, "TaskId", "NameOfTask", emplyeeJob.TaskId);
            return View(emplyeeJob);
        }

        // GET: EmplyeeJobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emplyeeJob = await _context.EmplyeeJob
                .Include(e => e.Employee)
                .Include(e => e.Task)
                .FirstOrDefaultAsync(m => m.EmplyeeJobId == id);
            if (emplyeeJob == null)
            {
                return NotFound();
            }

            return View(emplyeeJob);
        }

        // POST: EmplyeeJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emplyeeJob = await _context.EmplyeeJob.FindAsync(id);
            _context.EmplyeeJob.Remove(emplyeeJob);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmplyeeJobExists(int id)
        {
            return _context.EmplyeeJob.Any(e => e.EmplyeeJobId == id);
        }
    }
}
