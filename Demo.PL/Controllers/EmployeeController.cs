using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            // 1. ViewData => KeyValuePair[Dictionary Object]
            // Transfer Data From Controller [Action] T Its View
            // .Net FrameWork 3.5
            //ViewData["Message"] = "Hello From View Data";

            //ViewBag.Message = "Hello From View Bag";
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Add(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null)
            {
                return BadRequest(); //status code 400
            }
            var employee = _employeeRepository.GetById(id.Value);
            if (employee is null)
            {
                return NotFound();
            }
            return View(viewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null)
            //{
            //    return BadRequest(); //status code 400
            //}
            //var employee = _employeeRepository.GetById(id.Value);
            //if (employee is null)
            //{
            //    return NotFound();
            //}
            //return View(employee);
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee, [FromRoute] int id)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepository.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    //Log Exception
                    //Form
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(employee);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee employee, [FromRoute] int id)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepository.Delete(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
