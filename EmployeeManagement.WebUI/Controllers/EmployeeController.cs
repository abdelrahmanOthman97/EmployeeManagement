using EmployeeManagement.Domain.Interfaces.Services;
using EmployeeManagement.Domain.Interfaces.Services.Common;
using EmployeeManagement.Domain.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManagement.WebUI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDropdownListService _dropdownListService;

        public EmployeeController(IEmployeeService employeeService, IDropdownListService dropdownListService)
        {
            _employeeService = employeeService;
            _dropdownListService = dropdownListService;
        }

        [HttpGet("Get-Employees")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _employeeService.GetAllEmployees();
            if (!response.IsSucceeded)
            {
                ViewBag.Error = response.ErrorMsg;
                return View();
            }
            ViewBag.Success = TempData["Success"];
            ViewBag.Error = TempData["Error"];
            return View(response.Data);
        }

        [HttpGet("Create-Employee")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _dropdownListService.GetDepartments();
            return View();
        }

        [HttpPost("Create-Employee")]
        public async Task<IActionResult> Create(CreateEmployeeVM createEmployeeVM)
        {
            ViewBag.Departments = await _dropdownListService.GetDepartments();
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Invalid Data";
                return View(createEmployeeVM);
            }
            var response = await _employeeService.CreateEmployee(createEmployeeVM);
            if (!response.IsSucceeded)
            {
                ViewBag.Error = response.ErrorMsg;
                return View(createEmployeeVM);
            }
            TempData["Success"] = response.SuccessMsg;
            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet("Edit-Employee")]
        public async Task<IActionResult> Edit(int employeeId)
        {
            var response = await _employeeService.GetEmployeeForEdit(employeeId);
            if (!response.IsSucceeded)
            {
                TempData["Error"] = response.ErrorMsg;
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.Departments = await _dropdownListService.GetDepartments();
            return View(response.Data);
        }

        [HttpPost("Edit-Employee")]
        public async Task<IActionResult> Edit(EditEmployeeVM editEmployeeVM)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid Data";
                ViewBag.Departments = await _dropdownListService.GetDepartments();
                return View(editEmployeeVM);
            }
            var response = await _employeeService.EditEmployee(editEmployeeVM);
            if (!response.IsSucceeded)
            {
                TempData["Error"] = response.ErrorMsg;
                ViewBag.Departments = await _dropdownListService.GetDepartments();
                return View(editEmployeeVM);
            }
            TempData["Success"] = response.SuccessMsg;
            return RedirectToAction(nameof(GetAll));
        }
    }
}
