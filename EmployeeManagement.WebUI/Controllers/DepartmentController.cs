using EmployeeManagement.Domain.Interfaces.Services;
using EmployeeManagement.Domain.Interfaces.Services.Common;
using EmployeeManagement.Domain.ViewModels.Department;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DepartmentManagement.WebUI.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IDropdownListService _dropdownListService;

        public DepartmentController(IDepartmentService DepartmentService, IDropdownListService dropdownListService)
        {
            _departmentService = DepartmentService;
            _dropdownListService = dropdownListService;
        }
        [HttpGet("Get-Departments")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _departmentService.GetAllDepartments();
            if (!response.IsSucceeded)
            {
                ViewBag.Error = response.ErrorMsg;
                return View();
            }
            ViewBag.Success = TempData["Success"];
            ViewBag.Error = TempData["Error"];
            return View(response.Data);
        }

        [HttpGet("Create-Department")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Employees = await _dropdownListService.GetEmployess(includeManagers: false);
            return View();
        }

        [HttpPost("Create-Department")]
        public async Task<IActionResult> Create(CreateDepartmentVM createDepartmentVM)
        {
            ViewBag.Employees = await _dropdownListService.GetEmployess(includeManagers: false);
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Invalid Data";
                return View(createDepartmentVM);
            }
            var response = await _departmentService.CreateDepartment(createDepartmentVM);
            if (!response.IsSucceeded)
            {
                ViewBag.Error = response.ErrorMsg;
                return View(createDepartmentVM);
            }
            TempData["Success"] = response.SuccessMsg;
            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet("Edit-Department")]
        public async Task<IActionResult> Edit(int departmentId)
        {
            var response = await _departmentService.GetDepartmentForEdit(departmentId);
            if (!response.IsSucceeded)
            {
                TempData["Error"] = response.ErrorMsg;
                return RedirectToAction(nameof(GetAll));
            }
            ViewBag.Employees = await _dropdownListService.GetEmployess(includeManagers: false, includeManger: response.Data.ManagerId);
            return View(response.Data);
        }

        [HttpPost("Edit-Department")]
        public async Task<IActionResult> Edit(EditDepartmentVM editDepartmentVM)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid Data";
                ViewBag.Employees = await _dropdownListService.GetEmployess(includeManagers: false, includeManger: editDepartmentVM.ManagerId);
                return View(editDepartmentVM);
            }
            var response = await _departmentService.EditDepartment(editDepartmentVM);
            if (!response.IsSucceeded)
            {
                TempData["Error"] = response.ErrorMsg;
                ViewBag.Employees = await _dropdownListService.GetEmployess(includeManagers: false, includeManger: editDepartmentVM.ManagerId);
                return View(editDepartmentVM);
            }
            TempData["Success"] = response.SuccessMsg;
            return RedirectToAction(nameof(GetAll));
        }
    }
}
