using AutoMapper;
using EmployeeManagement.Common.Enum;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces.Repositories;
using EmployeeManagement.Domain.Interfaces.Services;
using EmployeeManagement.Domain.ViewModels.Common;
using EmployeeManagement.Domain.ViewModels.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> CreateEmployee(CreateEmployeeVM createEmployeeVM)
        {
            var employee = _mapper.Map<Employee>(createEmployeeVM);
            await _employeeRepository.AddAsync(employee);
            var request = await _employeeRepository.SaveEntitiesAsync();
            if (!request)
                return new Response<bool>
                {
                    Data = false,
                    ResponseCode = ResponseCodeEnum.InvalidOperation,
                    ErrorMsg = $"Invalid operation"
                };

            return new Response<bool>
            {
                Data = true,
                ResponseCode = ResponseCodeEnum.Success,
                SuccessMsg = $"Created successfully with employee id {employee.Id}"
            };
        }

        public async Task<Response<bool>> EditEmployee(EditEmployeeVM editEmployeeVM)
        {
            var employee = await _employeeRepository.GetByIdAsync(editEmployeeVM.Id);
            if (employee == null)
                return new Response<bool>
                {
                    Data = false,
                    ResponseCode = ResponseCodeEnum.InvalidOperation,
                    ErrorMsg = $"There is no employee with the Id {editEmployeeVM.Id}"
                };
            var updatedEmployee = _mapper.Map<Employee>(editEmployeeVM);
            _employeeRepository.Update(updatedEmployee);
            var request = await _employeeRepository.SaveEntitiesAsync();
            if (!request)
                return new Response<bool>
                {
                    Data = false,
                    ResponseCode = ResponseCodeEnum.InvalidOperation,
                    ErrorMsg = $"Invalid operation"
                };

            return new Response<bool>
            {
                Data = true,
                ResponseCode = ResponseCodeEnum.Success,
                SuccessMsg = $"Updated successfully"
            };
        }

        public async Task<Response<List<GetEmployeeVM>>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllAsync();
            var getEmployeeVMs = _mapper.Map<List<GetEmployeeVM>>(employees);
            return new Response<List<GetEmployeeVM>>
            {
                Data = getEmployeeVMs,
                ResponseCode = ResponseCodeEnum.Success,
            };
        }

        public async Task<Response<bool>> DeleteEmployee(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
                return new Response<bool>
                {
                    Data = false,
                    ResponseCode = ResponseCodeEnum.InvalidOperation,
                    ErrorMsg = $"There is no employee with the Id {employeeId}"
                };
            _employeeRepository.Delete(employee);
            var request = await _employeeRepository.SaveEntitiesAsync();
            if (!request)
                return new Response<bool>
                {
                    Data = false,
                    ResponseCode = ResponseCodeEnum.InvalidOperation,
                    ErrorMsg = $"Invalid operation"
                };

            return new Response<bool>
            {
                Data = true,
                ResponseCode = ResponseCodeEnum.Success,
                SuccessMsg = $"Deleted successfully"
            };
        }

        public async Task<Response<EditEmployeeVM>> GetEmployeeForEdit(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
                return new Response<EditEmployeeVM>
                {
                    Data = null,
                    ResponseCode = ResponseCodeEnum.InvalidOperation,
                    ErrorMsg = $"There is no employee with the Id {employeeId}"
                };
            var editEmployeeVM = _mapper.Map<EditEmployeeVM>(employee);
            return new Response<EditEmployeeVM>
            {
                Data = editEmployeeVM,
                ResponseCode = ResponseCodeEnum.Success,
            };
        }
    }
}
