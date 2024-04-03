using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.GetEmployeesByName(SearchValue);
            }
            var MappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(MappedEmployees);

            // 1. ViewData => KeyValuePair[Dictionary Object]
            // Transfer Data From Controller [Action] T Its View
            // .Net FrameWork 3.5
            //ViewData["Message"] = "Hello From View Data";

            //ViewBag.Message = "Hello From View Bag";
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                employeeVM.ImageName = DocumentSettings.UplodaFile(employeeVM.Image, "Images");
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                await _unitOfWork.EmployeeRepository.AddAsync(MappedEmployee);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(employeeVM);
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest(); //status code 400

            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);

            if (employee is null)
                return NotFound();

            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);

            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync((int)employee.DepartmentId);
            ViewBag.DepartmentName = department.Name;

            return View(viewName, MappedEmployee);
        }

        [HttpGet]
        public Task<IActionResult> Edit(int? id)
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAllAsync();
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
        public IActionResult Edit(EmployeeViewModel employeeViewModel, [FromRoute] int id)
        {
            if (id != employeeViewModel.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    employeeViewModel.ImageName = DocumentSettings.UplodaFile(employeeViewModel.Image, "Images");
                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
                    _unitOfWork.EmployeeRepository.Update(MappedEmployee);
                    _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    //Log Exception
                    //Form
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(employeeViewModel);
        }

        [HttpGet]
        public Task<IActionResult> Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeViewModel, [FromRoute] int id)
        {
            if (id != employeeViewModel.Id)
            {
                return BadRequest();
            }   
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
                    _unitOfWork.EmployeeRepository.Delete(MappedEmployee);
                    int result = await _unitOfWork.CompleteAsync();
                    if (result > 0 && employeeViewModel.ImageName is not null)
                    {
                        DocumentSettings.DeleteFile(employeeViewModel.ImageName, "Images");
                    }
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
