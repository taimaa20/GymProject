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
    public class LoginsController : Controller
    {
        private readonly GymContext _context;

        public LoginsController(GymContext context)
        {
            _context = context;
        }

        // GET: Logins
        public async Task<IActionResult> Index()
        {
            var gymContext = _context.Login.Include(l => l.Customer).Include(l => l.Emploee).Include(l => l.Type);
            return View(await gymContext.ToListAsync());
        }

        // GET: Logins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login
                .Include(l => l.Customer)
                .Include(l => l.Emploee)
                .Include(l => l.Type)
                .FirstOrDefaultAsync(m => m.LoginId == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // GET: Logins/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Fname");
            ViewData["EmploeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fname");
            ViewData["TypeId"] = new SelectList(_context.LoginTypes, "TypeId", "LoginType");
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoginId,Email,Password,TypeId,CustomerId,EmploeeId")] Login login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Fname", login.CustomerId);
            ViewData["EmploeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fname", login.EmploeeId);
            ViewData["TypeId"] = new SelectList(_context.LoginTypes, "TypeId", "LoginType", login.TypeId);
            return View(login);
        }

        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Fname", login.CustomerId);
            ViewData["EmploeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fname", login.EmploeeId);
            ViewData["TypeId"] = new SelectList(_context.LoginTypes, "TypeId", "LoginType", login.TypeId);
            return View(login);
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoginId,Email,Password,TypeId,CustomerId,EmploeeId")] Login login)
        {
            if (id != login.LoginId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.LoginId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Fname", login.CustomerId);
            ViewData["EmploeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fname", login.EmploeeId);
            ViewData["TypeId"] = new SelectList(_context.LoginTypes, "TypeId", "LoginType", login.TypeId);
            return View(login);
        }

        // GET: Logins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login
                .Include(l => l.Customer)
                .Include(l => l.Emploee)
                .Include(l => l.Type)
                .FirstOrDefaultAsync(m => m.LoginId == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var login = await _context.Login.FindAsync(id);
            _context.Login.Remove(login);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginExists(int id)
        {
            return _context.Login.Any(e => e.LoginId == id);
        }





     
    }
}
