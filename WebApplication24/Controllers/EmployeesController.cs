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
    public class EmployeesController : Controller
    {
        private readonly GymContext _context;

        public EmployeesController(GymContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var gymContext = _context.Employee.Include(e => e.Type);
            return View(await gymContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Type)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.LoginTypes, "TypeId", "LoginType");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Fname,Lname,Phone,TypeId,Evaluation")] Employee employee, string username, string password,int TypeId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);

                var LastId = _context.Employee.OrderByDescending(x => x.EmployeeId).FirstOrDefault().EmployeeId;

                Login login = new Login();
                Customer customers = new Customer();
                LoginTypes logintyp = new LoginTypes();
                login.EmploeeId = LastId;
               
                login.TypeId = TypeId;
                login.Email = username;
                login.Password = password;


                _context.Add(login);

                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Employees");
            }
            ViewData["TypeId"] = new SelectList(_context.LoginTypes, "TypeId", "LoginType", employee.TypeId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.LoginTypes, "TypeId", "LoginType", employee.TypeId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,Fname,Lname,Phone,TypeId,Evaluation")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "Employees");
            }
        
            return View(employee);
        }
        public async Task<IActionResult> EditEmployee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
       
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee(int id, [Bind("EmployeeId,Fname,Lname,Phone")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
          
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Type)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeId == id);
        }
        string id = "id";
        public IActionResult EmployeeTask ()
        {
            //FetchData();

            GymContext gym = new GymContext();

            int? employeeid = HttpContext.Session.GetInt32(id);
            ViewBag.id = HttpContext.Session.GetInt32(id);

            List<Employee> emp = gym.Employee.ToList();
            List<Models.Task> tasks = gym.Task.ToList();
            List<EmplyeeJob> empjob = gym.EmplyeeJob.ToList();
            var multable = from s in empjob
                           join emm in emp on s.EmployeeId equals emm.EmployeeId into table1
                           from emm in table1.DefaultIfEmpty()
                           join task in tasks on s.TaskId equals task.TaskId into table2
                           from task in table2.DefaultIfEmpty()

                           select new joins { emplyeeJob = s,tasks1=task,employee=emm};
            var multable1 = multable.Where(x => x.employee.EmployeeId == employeeid);

            return View(multable1.ToList());

        }
        public IActionResult SalaySlip()
        {
            GymContext gym = new GymContext();
            int? employeeid = HttpContext.Session.GetInt32(id);
            ViewBag.id = HttpContext.Session.GetInt32(id);

            List<Employee> emp = gym.Employee.ToList();
            List<Salary> salary = gym.Salary.ToList();
            List<Login> login = gym.Login.ToList();
            ViewBag.SumOfProduct = _context.Salary.DefaultIfEmpty().Sum(x => x.Salary1);

            var multable = from emm in emp
                           join s in salary on emm.EmployeeId equals s.EmployeeId into table1
                           from s in table1.DefaultIfEmpty()
                           join log in login on emm.EmployeeId equals log.EmploeeId into table2
                           from log in table2.DefaultIfEmpty()


                           select new joins { salary = s, employee = emm ,logins=log };
            var multable1 = multable.Where(x => x.employee.EmployeeId == employeeid);
            

            return View(multable1.ToList());

    
        }
    }
}
