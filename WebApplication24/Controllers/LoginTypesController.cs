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
    public class LoginTypesController : Controller
    {
        private readonly GymContext _context;

        public LoginTypesController(GymContext context)
        {
            _context = context;
        }

        // GET: LoginTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoginTypes.ToListAsync());
        }

        // GET: LoginTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginTypes = await _context.LoginTypes
                .FirstOrDefaultAsync(m => m.TypeId == id);
            if (loginTypes == null)
            {
                return NotFound();
            }

            return View(loginTypes);
        }

        // GET: LoginTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoginTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeId,LoginType,TypeOfLoginType")] LoginTypes loginTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loginTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loginTypes);
        }

        // GET: LoginTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginTypes = await _context.LoginTypes.FindAsync(id);
            if (loginTypes == null)
            {
                return NotFound();
            }
            return View(loginTypes);
        }

        // POST: LoginTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeId,LoginType,TypeOfLoginType")] LoginTypes loginTypes)
        {
            if (id != loginTypes.TypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loginTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginTypesExists(loginTypes.TypeId))
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
            return View(loginTypes);
        }

        // GET: LoginTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginTypes = await _context.LoginTypes
                .FirstOrDefaultAsync(m => m.TypeId == id);
            if (loginTypes == null)
            {
                return NotFound();
            }

            return View(loginTypes);
        }

        // POST: LoginTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loginTypes = await _context.LoginTypes.FindAsync(id);
            _context.LoginTypes.Remove(loginTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginTypesExists(int id)
        {
            return _context.LoginTypes.Any(e => e.TypeId == id);
        }
    }
}
