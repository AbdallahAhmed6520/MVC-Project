using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
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
	}
}
