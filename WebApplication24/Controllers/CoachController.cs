using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication24.Models;

using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace WebApplication24.Controllers
{
    public class CoachController : Controller
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<MemberCard> memberCard = new List<MemberCard>();
        List<MemberShip> memberShip = new List<MemberShip>();
        private readonly ILogger<CoachController> _logger;
        private readonly GymContext _context;

        GymContext gym = new GymContext();

        public CoachController(ILogger<CoachController> logger, GymContext context)
        {
            _logger = logger;
            con.ConnectionString = WebApplication24.Properties.Resources.ConnectionString;
            _context = context;

        }
        

        public IActionResult Index()
        {
            // ViewBag.CountOfProduct = _context.MemberCard.DefaultIfEmpty().Count();
            // ViewBag.CountOfCategoreys = _context.Categoreys.DefaultIfEmpty().Count();
            int? employeeid = HttpContext.Session.GetInt32(id);
            ViewBag.id = HttpContext.Session.GetInt32(id);
            List<Employee> emp = gym.Employee.ToList();
            ViewBag.SumOfProduct = _context.Employee.Where(x=>x.EmployeeId==employeeid).Count();
            ViewBag.Avgoftime  = _context.MemberShip.DefaultIfEmpty().Average(x => x.TimePerMonth);



            var Client = _context.Employee.ToList();

            var models = Tuple.Create<IEnumerable<Employee>>(Client);
        
            return View(models );


        
        }

    
   

        public IActionResult Display(  )
        {
                int? employeeid = HttpContext.Session.GetInt32(id);
            ViewBag.id = HttpContext.Session.GetInt32(id);
           
            //FetchData();

            GymContext gym = new GymContext();
            List<Customer> cust = gym.Customer.ToList();
            List<MemberCard> card = gym.MemberCard.ToList();
            List<MemberShip> ship = gym.MemberShip.ToList();
            List<Employee> emp = gym.Employee.ToList();
            
            var multable = from c in card
                           join sh in ship on c.MemberShipId equals sh.MemberShipId into table1
                           from sh in table1.DefaultIfEmpty()
                           join emm in emp on c.EmployeeId equals emm.EmployeeId into table2
                           from emm in table2.DefaultIfEmpty()
                           join cus in cust on c.CustomerId equals cus.CustomerId into table3
                           from cus in table3.DefaultIfEmpty()
                           select new joins { memberCard = c, memberShip = sh ,employee=emm ,customer=cus};


            var multable1 = multable.Where(x => x.employee.EmployeeId == employeeid);

            return View(multable1.ToList());

        }

        public IActionResult AccountantDisplay()
        {
            //FetchData();

            GymContext gym = new GymContext();

            int? employeeid = HttpContext.Session.GetInt32(id);
            ViewBag.id = HttpContext.Session.GetInt32(id);
         
            List<Employee> emp = gym.Employee.ToList();
            List<Salary> salary = gym.Salary.ToList();
            var multable = from s in salary
                           join emm in emp on s.EmployeeId equals emm.EmployeeId into table1
                           from emm in table1.DefaultIfEmpty()
                           
                           select new joins {employee=emm ,salary=s};
            var multable1 = multable.Where(x => x.employee.EmployeeId == employeeid);

            return View(multable1.ToList());

        }

        public IActionResult ParticipantSearch(string searching)
        {
            
            List<Employee> emp = gym.Employee.ToList();
            List<Customer> customer = gym.Customer.ToList();
            List<MemberCard> card = gym.MemberCard.ToList();
            var multable = from c in card
                           join cust in customer on c.CustomerId equals cust.CustomerId into table1
                           from cust in table1.DefaultIfEmpty()
                           join emm in emp on c.EmployeeId equals emm.EmployeeId into table2
                           from emm in table2.DefaultIfEmpty()
                           select new joins { memberCard = c, employee = emm, customer=cust};

           
               var multable1 = multable.Where(x => x.customer.Fname==searching || x.customer.Lname == searching);
              
           
            
                return View( multable1.ToList());


        }
        string id = "id";
   
       
        public IActionResult InfoDisplay( )
        {
            //FetchData();
            int? employeeid = HttpContext.Session.GetInt32(id);
            ViewBag.id = HttpContext.Session.GetInt32(id);


            GymContext gym = new GymContext();
            List<Employee> emp = gym.Employee.ToList();
            List<Salary> salary = gym.Salary.ToList();
            List<Login> logins = gym.Login.ToList();
            var multable = from s in salary
                           join emm in emp on s.EmployeeId equals emm.EmployeeId into table1
                           from emm in table1.DefaultIfEmpty()
                           join logs in logins on emm.EmployeeId equals logs.EmploeeId into table2
                           from logs in table2.DefaultIfEmpty()
                         
                           select new joins { employee = emm, salary = s ,logins=logs};
            var multable1 = multable.Where(x => x.employee.EmployeeId == employeeid);
        

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
        //private void FetchData()
        //{

        //    if (memberCard.Count > 0)
        //    {
        //        memberCard.Clear();
        //    }
        //    try
        //    {
        //        con.Open();
        //        com.Connection = con;
        //        com.CommandText = "SELECT [CardID],[TimeStart],[TimeEnd],[MemberShipName],MemberShip.MemberShipID,[TimePerMonth],[TypeOfEvnet],[Capacity],EmployeeID FROM MemberCard left JOIN MemberShip ON MemberCard.MemberShipID = MemberShip.MemberShipID" ;
        //        dr = com.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            memberCard.Add(new MemberCard()
        //            {
        //           CardId = (int)dr["CardID"],
        //                TimeStart = (DateTime)dr["TimeStart"],
        //                TimeEnd = (DateTime)dr["TimeEnd"],

        //            });
        //            memberShip.Add(new MemberShip()
        //            {
        //                MemberShipId = (int)dr["MemberShipId"],
        //                MemberShipName = dr["MemberShipName"].ToString(),
        //                TimePerMonth = (int)dr["TimePerMonth"],
        //                TypeOfEvnet = dr["TypeOfEvnet"].ToString(),
        //                Capacity = (int)dr["Capacity"],

        //            });
        //        }

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
