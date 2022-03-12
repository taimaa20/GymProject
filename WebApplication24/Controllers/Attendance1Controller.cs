using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication24.Models;

namespace WebApplication24.Controllers
{
    public class Attendance1Controller : Controller
    {
        private readonly GymContext _context;

        public Attendance1Controller(GymContext context)
        {
            _context = context;
        }

        // GET: Attendance1
        public async Task<IActionResult> Index()
        {
            var gymContext = _context.Attendance1.Include(a => a.Employee);
            return View(await gymContext.ToListAsync());
        }

        // GET: Attendance1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance1 = await _context.Attendance1
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance1 == null)
            {
                return NotFound();
            }

            return View(attendance1);
        }

        // GET: Attendance1/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fname");
            return View();
        }

        // POST: Attendance1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        string id = "id";
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttendanceId,EmployeeId,CheckIn,CheckOut")] Attendance1 attendance1 ,DateTime ? CheckOut, DateTime ? CheckIn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendance1);
                await _context.SaveChangesAsync();

                Attendance1 att = new Attendance1();
                //int employeeid = (int)HttpContext.Session.GetInt32(id);
                //ViewBag.id = HttpContext.Session.GetInt32(id);
                att.EmployeeId = 13;
                att.CheckIn = CheckIn;
                att.CheckOut = CheckOut;

                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fname", attendance1.EmployeeId);
            return View(attendance1);
        }

        // GET: Attendance1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance1 = await _context.Attendance1.FindAsync(id);
            if (attendance1 == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fname", attendance1.EmployeeId);
            return View(attendance1);
        }

        // POST: Attendance1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttendanceId,EmployeeId,CheckIn,CheckOut")] Attendance1 attendance1)
        {
            if (id != attendance1.AttendanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Attendance1Exists(attendance1.AttendanceId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fname", attendance1.EmployeeId);
            return View(attendance1);
        }

        // GET: Attendance1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance1 = await _context.Attendance1
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance1 == null)
            {
                return NotFound();
            }

            return View(attendance1);
        }

        // POST: Attendance1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendance1 = await _context.Attendance1.FindAsync(id);
            _context.Attendance1.Remove(attendance1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Attendance1Exists(int id)
        {
            return _context.Attendance1.Any(e => e.AttendanceId == id);
        }
    }
}
