using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication24.Models;

namespace WebApplication24.Controllers
{
    public class AccountantController : Controller
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<MemberCard> memberCard = new List<MemberCard>();
        List<MemberShip> memberShip = new List<MemberShip>();
        private readonly ILogger<AccountantController> _logger;
        private readonly GymContext _context;
        public IActionResult Index()
        {
            ViewBag.SumOfProduct = _context.MemberCard.Where(x => x.CardId>=1) .Count();
            ViewBag.sumofcapacityofgym = _context.MemberShip.Where(x => x.Capacity>=1).Count();
            ViewBag.sumofsalary = _context.Salary.DefaultIfEmpty().Average(x => x.Salary1);
            return View();
        }
        public AccountantController(ILogger<AccountantController> logger, GymContext context)
        {
            _logger = logger;
            con.ConnectionString = WebApplication24.Properties.Resources.ConnectionString;
            _context = context;

        }
        string id = "id";
        public IActionResult InfoDisplay( int id )
        {
            //FetchData();

            GymContext gym = new GymContext();
            List<Employee> emp = gym.Employee.ToList();
            List<Salary> salary = gym.Salary.ToList();
      
            var multable = from s in salary
                           join emm in emp on s.EmployeeId equals emm.EmployeeId into table1
                           from emm in table1.DefaultIfEmpty()
                        

                           select new joins { employee = emm, salary = s };

            var multable1 = multable.Where(x => x.employee.EmployeeId== id);
            return View(multable1.ToList());

        }
        public IActionResult DisplayEmployee()
        {
            //FetchData();

            GymContext gym = new GymContext();
            List<Employee> emp = gym.Employee.ToList();
            List<Salary> salary = gym.Salary.ToList();

            var multable = from s in salary
                           join emm in emp on s.EmployeeId equals emm.EmployeeId into table1
                           from emm in table1.DefaultIfEmpty()


                           select new joins { employee = emm, salary = s };
           

            return View(multable.ToList());

        }
        public IActionResult CostumerPayemnt()
        {
            //FetchData();
           
            GymContext gym = new GymContext();
            List<Payment> pay = gym.Payment.ToList();
            List<Customer> cust = gym.Customer.ToList();
          
            var multable = from c in pay
                           join emm in cust on c.CutsomerId equals emm.CustomerId into table1
                           from emm in table1.DefaultIfEmpty() 
                           select new joins { customer = emm, payment = c };

         

            return View(multable.ToList());
        

        }
        public IActionResult PaymentDisplay(int id)
        {
            //FetchData();

            GymContext gym = new GymContext();
            List<Payment> pay = gym.Payment.ToList();
            List<Customer> cust = gym.Customer.ToList();

            var multable = from c in cust
                           join emm in pay on c.CustomerId equals emm.CutsomerId into table1
                           from emm in table1.DefaultIfEmpty()
                           select new joins { customer = c, payment = emm };

            var multable1 = multable.Where(x => x.customer.CustomerId == id);
            return View(multable1.ToList());

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
    }
}
