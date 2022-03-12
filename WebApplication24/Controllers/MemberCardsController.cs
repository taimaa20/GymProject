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
    public class MemberCardsController : Controller
    {
        private readonly GymContext _context;

        public MemberCardsController(GymContext context)
        {
            _context = context;
        }

        // GET: MemberCards
        public async Task<IActionResult> Index()
        {
            var gymContext = _context.MemberCard.Include(m => m.Customer).Include(m => m.Employee).Include(m => m.MemberShip);
            return View(await gymContext.ToListAsync());
        }

        // GET: MemberCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberCard = await _context.MemberCard
                .Include(m => m.Customer)
                .Include(m => m.Employee)
                .Include(m => m.MemberShip)
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (memberCard == null)
            {
                return NotFound();
            }

            return View(memberCard);
        }

        // GET: MemberCards/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Fname");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fname");
            ViewData["MemberShipId"] = new SelectList(_context.MemberShip, "MemberShipId", "MemberShipName");
            return View();
        }

        // POST: MemberCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        string id = "id";
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CardId,CustomerId,MemberShipId,TimeStart,TimeEnd,EmployeeId")] MemberCard memberCard ,string typeofpayment,int NumberOfTimesToPay, DateTime DateOfPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberCard);
                var LastId = _context.MemberCard.OrderByDescending(x => x.CardId).FirstOrDefault().CardId;
                Payment pay = new Payment();
                MemberShip memberShip = new MemberShip();
                Customer customer = new Customer();
                int customerId = (int)HttpContext.Session.GetInt32(id);
                ViewBag.id = HttpContext.Session.GetInt32(id);
                memberCard.CustomerId = customerId;
                pay.CardId = memberCard.CardId;
                pay.TypeOfPayment = typeofpayment;
                pay.NumberOfTimesToPay = NumberOfTimesToPay;
                
           
              
                _context.Add(pay);
              
                return RedirectToAction("Index","Home");
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Fname", memberCard.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fname", memberCard.EmployeeId);
            ViewData["MemberShipId"] = new SelectList(_context.MemberShip, "MemberShipId", "MemberShipName", memberCard.MemberShipId);
            return View(memberCard);
        }

        // GET: MemberCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberCard = await _context.MemberCard.FindAsync(id);
            if (memberCard == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Fname", memberCard.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fname", memberCard.EmployeeId);
            ViewData["MemberShipId"] = new SelectList(_context.MemberShip, "MemberShipId", "MemberShipName", memberCard.MemberShipId);
            return View(memberCard);
        }

        // POST: MemberCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CardId,CustomerId,MemberShipId,TimeStart,TimeEnd,EmployeeId")] MemberCard memberCard)
        {
            if (id != memberCard.CardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberCardExists(memberCard.CardId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Fname", memberCard.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Fname", memberCard.EmployeeId);
            ViewData["MemberShipId"] = new SelectList(_context.MemberShip, "MemberShipId", "MemberShipName", memberCard.MemberShipId);
            return View(memberCard);
        }

        // GET: MemberCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberCard = await _context.MemberCard
                .Include(m => m.Customer)
                .Include(m => m.Employee)
                .Include(m => m.MemberShip)
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (memberCard == null)
            {
                return NotFound();
            }

            return View(memberCard);
        }

        // POST: MemberCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberCard = await _context.MemberCard.FindAsync(id);
            _context.MemberCard.Remove(memberCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberCardExists(int id)
        {
            return _context.MemberCard.Any(e => e.CardId == id);
        }
    }
}
