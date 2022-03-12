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
    public class PaymentsController : Controller
    {
        private readonly GymContext _context;

        public PaymentsController(GymContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var gymContext = _context.Payment.Include(p => p.Card).Include(p => p.Cutsomer).Include(p => p.MemberShip);
            return View(await gymContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Card)
                .Include(p => p.Cutsomer)
                .Include(p => p.MemberShip)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["CardId"] = new SelectList(_context.MemberCard, "CardId", "CardId");
            ViewData["CutsomerId"] = new SelectList(_context.Customer, "CustomerId", "Fname");
            ViewData["MemberShipId"] = new SelectList(_context.MemberShip, "MemberShipId", "MemberShipName");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeOfPayment,NumberOfTimesToPay,MemberShipId,PaymentId,MountOfPayment,DateOfPayment,TheRestOfTheAmount,CardId,CutsomerId")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardId"] = new SelectList(_context.MemberCard, "CardId", "CardId", payment.CardId);
            ViewData["CutsomerId"] = new SelectList(_context.Customer, "CustomerId", "Fname", payment.CutsomerId);
            ViewData["MemberShipId"] = new SelectList(_context.MemberShip, "MemberShipId", "MemberShipName", payment.MemberShipId);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["CardId"] = new SelectList(_context.MemberCard, "CardId", "CardId", payment.CardId);
            ViewData["CutsomerId"] = new SelectList(_context.Customer, "CustomerId", "Fname", payment.CutsomerId);
            ViewData["MemberShipId"] = new SelectList(_context.MemberShip, "MemberShipId", "MemberShipName", payment.MemberShipId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeOfPayment,NumberOfTimesToPay,MemberShipId,PaymentId,MountOfPayment,DateOfPayment,TheRestOfTheAmount,CardId,CutsomerId")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "Payments");
            }
            ViewData["CardId"] = new SelectList(_context.MemberCard, "CardId", "CardId", payment.CardId);
            ViewData["CutsomerId"] = new SelectList(_context.Customer, "CustomerId", "Fname", payment.CutsomerId);
            ViewData["MemberShipId"] = new SelectList(_context.MemberShip, "MemberShipId", "MemberShipName", payment.MemberShipId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Card)
                .Include(p => p.Cutsomer)
                .Include(p => p.MemberShip)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.PaymentId == id);
        }
    }
}
