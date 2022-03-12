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
    public class MemberShipsController : Controller
    {
        private readonly GymContext _context;

        public MemberShipsController(GymContext context)
        {
            _context = context;
        }

        // GET: MemberShips
        public async Task<IActionResult> Index()
        {
            var gymContext = _context.MemberShip.Include(m => m.Offer);
            return View(await gymContext.ToListAsync());
        }

        // GET: MemberShips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberShip = await _context.MemberShip
                .Include(m => m.Offer)
                .FirstOrDefaultAsync(m => m.MemberShipId == id);
            if (memberShip == null)
            {
                return NotFound();
            }

            return View(memberShip);
        }

        // GET: MemberShips/Create
        public IActionResult Create()
        {
            ViewData["OfferId"] = new SelectList(_context.Offers, "OfferId", "OfferId");
            return View();
        }

        // POST: MemberShips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberShipId,MemberShipName,TimePerMonth,TypeOfEvnet,Cost,OfferId,Capacity")] MemberShip memberShip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberShip);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "MemberShips");
            }
            ViewData["OfferId"] = new SelectList(_context.Offers, "OfferId", "OfferId", memberShip.OfferId);
            return View(memberShip);
        }

        // GET: MemberShips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberShip = await _context.MemberShip.FindAsync(id);
            if (memberShip == null)
            {
                return NotFound();
            }
            ViewData["OfferId"] = new SelectList(_context.Offers, "OfferId", "OfferId", memberShip.OfferId);
            return View(memberShip);
        }

        // POST: MemberShips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberShipId,MemberShipName,TimePerMonth,TypeOfEvnet,Cost,OfferId,Capacity")] MemberShip memberShip)
        {
            if (id != memberShip.MemberShipId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberShip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberShipExists(memberShip.MemberShipId))
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
            ViewData["OfferId"] = new SelectList(_context.Offers, "OfferId", "OfferId", memberShip.OfferId);
            return View(memberShip);
        }

        // GET: MemberShips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberShip = await _context.MemberShip
                .Include(m => m.Offer)
                .FirstOrDefaultAsync(m => m.MemberShipId == id);
            if (memberShip == null)
            {
                return NotFound();
            }

            return View(memberShip);
        }

        // POST: MemberShips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberShip = await _context.MemberShip.FindAsync(id);
            _context.MemberShip.Remove(memberShip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberShipExists(int id)
        {
            return _context.MemberShip.Any(e => e.MemberShipId == id);
        }
    }
}
