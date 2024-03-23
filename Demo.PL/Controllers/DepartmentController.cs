﻿using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)//Ask CLR for createing Object from Class Implement Interface IDepartmentRepository
        {
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.Add(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null)
            {
                return BadRequest(); //status code 400
            }
            var department = _departmentRepository.GetById(id.Value);
            if (department is null)
            {
                return NotFound();
            }
            return View(viewName, department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null)
            //{
            //    return BadRequest(); //status code 400
            //}
            //var department = _departmentRepository.GetById(id.Value);
            //if (department is null)
            //{
            //    return NotFound();
            //}
            //return View(department);
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _departmentRepository.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    //Log Exception
                    //Form
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(department);
        }
    }
}
