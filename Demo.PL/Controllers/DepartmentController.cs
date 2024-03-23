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

        public IActionResult Details(int? id)
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
            return View(department);
        }
    }
}