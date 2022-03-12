using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication24.Models;

namespace WebApplication24.Controllers
{
    public class AdminController : Controller
    {
        private readonly GymContext _context;

        public AdminController(GymContext context)
        {
            _context = context;
        }


        GymContext db = new GymContext();
        public interface IAdminController
        {
            int creat(Employee employee);
            Employee FindByName(string Fname);
            Employee ReadById(int EmployeeID);
            IEnumerable<Employee> ReadAll();

        }
        public int CreateEmployee(Employee employee)
        {
            var existEmployee = FindByName(employee.Fname);
            if (existEmployee != null)
            {
                return -2;
            }
            db.Employee.Add(employee);
            return db.SaveChanges();

       //     return db.Employee.Where(t => t.Fname == Fname).FirstOrDefault();
        }
        public IActionResult Index()
        {
            ViewBag.countOFCustomer = _context.Customer.Count();
            ViewBag.countOFEmployee = _context.Employee.Count();
            ViewBag.countOFCoaches = _context.Employee.Where(x => x.TypeId == 3).Count();


            return View();
        }
        public Employee FindByName(string Fname)
        {
            return db.Employee.Where(t => t.Fname == Fname).FirstOrDefault();
        }
      public IActionResult MembershipAdd()
        {

           return RedirectToAction("Create", "MemberShips");
      
        }
        string id = "id";
        public IActionResult AddEmployee()
        {
            return RedirectToAction("Create", "Employees");
        }

        public IActionResult Reservationfrom11(DateTime startDate, DateTime endDate)
        {
            int? employeeid = HttpContext.Session.GetInt32(id);
            ViewBag.id = HttpContext.Session.GetInt32(id);

            var gymContext = _context.MemberShip.Include(m => m.MemberCard).Include(m => m.Offer);
            if (startDate != null)
            {
                GymContext gym = new GymContext();
                List<MemberCard> memberCards = gym.MemberCard.ToList();
                List<MemberShip> memberShips = gym.MemberShip.ToList();
                List<Offers> off = gym.Offers.ToList();


                var multable = from c in memberCards
                               join emm in memberShips on c.MemberShipId equals emm.MemberShipId into table2
                               from emm in table2.DefaultIfEmpty()

                               select new joins { memberCard = c, memberShip = emm };


                var multable1 = multable.Where(x => x.memberCard.TimeStart >= startDate && x.memberCard.TimeEnd <= endDate);
                return View(multable1);
            }
            return View(gymContext.ToList());

        }
        public IActionResult Reservationfrom12(DateTime startDate, DateTime endDate)
        {
            int? employeeid = HttpContext.Session.GetInt32(id);
            ViewBag.id = HttpContext.Session.GetInt32(id);
            var gymContext = _context.MemberShip.Include(m => m.MemberCard).Include(m => m.Offer);
            if (startDate != null)
            {
                GymContext gym = new GymContext();
                List<MemberCard> memberCards = gym.MemberCard.ToList();
                List<MemberShip> memberShips = gym.MemberShip.ToList();
                List<Offers> off = gym.Offers.ToList();


                var multable = from c in memberCards
                               join emm in memberShips on c.MemberShipId equals emm.MemberShipId into table2
                               from emm in table2.DefaultIfEmpty()

                               select new joins { memberCard = c, memberShip = emm };


                var multable1 = multable.Where(x => x.memberCard.TimeStart >= startDate && x.memberCard.TimeEnd <= endDate);
                return View(multable1);
            }
            return View(gymContext.ToList());

        }
        //public async Task<IActionResult> Reservationfrom1(int ?startDate, int? endDate) { 
        //    var gymContext = _context.MemberCard.Include(m => m.Customer).Include(m => m.Employee).Include(m => m.MemberShip);



        //        var res = (from x in _context.MemberCard
        //                   where (x.TimeStart.Year >= startDate) && (x.TimeEnd.Year <= endDate)
        //                   select x).ToListAsync();
        //        return View(await res);

        //}
        //public async Task<IActionResult> FinanacialReport(DateTime? startDate = null)
        //{
        //    var gymContext = _context.Salary.Include(m => m.Employee);
        //    if (startDate != null)
        //    {
        //        GymContext gym = new GymContext();
        //        List<Employee> emp = gym.Employee.ToList();
        //        List<Salary> salary = gym.Salary.ToList();




        //        var res = (from x in _context.Salary
        //                   where (x.MonthOfSalary == startDate)
        //                   select x).ToListAsync();
        //        return View(await res);

        //    }
        //    return View(gymContext);
        //}
        public IActionResult FinanacialReport(DateTime startDate, DateTime endDate )
        {

            var gymContext = _context.Salary.Include(m => m.Employee);
            if (startDate != null)
            {
                GymContext gym = new GymContext();
                List<Employee> emp = gym.Employee.ToList();
                List<Salary> salary = gym.Salary.ToList();

                var multable = from c in salary
                               join emm in emp on c.EmployeeId equals emm.EmployeeId into table2
                               from emm in table2.DefaultIfEmpty()
                               select new joins { salary = c,  employee = emm };


                var multable1 = multable.Where(x => x.salary.MonthOfSalary >= startDate  &&  x.salary.MonthOfSalary  <= endDate);
                return View( multable1);
            }
            return View(gymContext.ToList());

        }
        public IActionResult Reservationfull1(DateTime startDate, DateTime endDate)
        {

            var gymContext = _context.MemberShip.Include(m => m.MemberCard).Include(m => m.Offer);
            if (startDate != null)
            {
                GymContext gym = new GymContext();
                List<MemberCard> memberCards = gym.MemberCard.ToList();
                List<MemberShip> memberShips = gym.MemberShip.ToList();
                List<Offers> off = gym.Offers.ToList();


                var multable = from c in memberCards
                               join emm in memberShips on c.MemberShipId equals emm.MemberShipId into table2
                               from emm in table2.DefaultIfEmpty()
                           
                               select new joins { memberCard = c, memberShip = emm };


                var multable1 = multable.Where(x => x.memberCard.TimeStart >= startDate && x.memberCard.TimeEnd <= endDate);
                return View(multable1);
            }
            return View(gymContext.ToList());

        }
    }
}

