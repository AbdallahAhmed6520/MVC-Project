using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)//Ask CLR for createing Object from Class Implement Interface IDepartmentRepository
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.DepartmentRepository.AddAsync(department);
                int result = await _unitOfWork.CompleteAsync();
                // 3. Temp Data => Dictionary Object
                // Transfer Data From Action To Action
                if (result > 0)
                    TempData["Message"] = "Department Is Created Sucessfully";
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
            {
                return BadRequest(); //status code 400
            }
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);
            if (department is null)
            {
                return NotFound();
            }
            return View(viewName, department);
        }

        [HttpGet]
        public Task<IActionResult> Edit(int? id)
        {
            //if (id is null)
            //{
            //    return BadRequest(); //status code 400
            //}
            //var department = _unitOfWork.DepartmentRepository.GetById(id.Value);
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
                    _unitOfWork.DepartmentRepository.Update(department);
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

            return View(department);
        }

        [HttpGet]
        public Task<IActionResult> Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Delete(department);
                    _unitOfWork.CompleteAsync();
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
