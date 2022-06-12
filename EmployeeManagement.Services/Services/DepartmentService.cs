using AutoMapper;
using EmployeeManagement.Common.Enum;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces.DomainEvents;
using EmployeeManagement.Domain.Interfaces.Repositories;
using EmployeeManagement.Domain.Interfaces.Repositories.Common;
using EmployeeManagement.Domain.Interfaces.Services;
using EmployeeManagement.Domain.ViewModels.Common;
using EmployeeManagement.Domain.ViewModels.Department;
using EmployeeManagement.Services.DomainEvents.Department;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Services.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainEventManager _domainEventManager;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, IUnitOfWork unitOfWork, IDomainEventManager domainEventManager)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _domainEventManager = domainEventManager;
        }

        public async Task<Response<bool>> CreateDepartment(CreateDepartmentVM createDepartmentVM)
        {
            var department = _mapper.Map<Department>(createDepartmentVM);
            _unitOfWork.BeginTransaction();
            await _departmentRepository.AddAsync(department);

            var (res, error) = await FireDomainEventsForCreateDepartment(department);
            if (!res)
                return new Response<bool>
                {
                    Data = false,
                    ResponseCode = ResponseCodeEnum.InvalidOperation,
                    ErrorMsg = string.IsNullOrEmpty(error) ? "Invalid operation" : error,
                };

            var saved = _unitOfWork.CommitTransaction();
            if (!saved)
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
                SuccessMsg = $"Created successfully with department id {department.Id}"
            };
        }

        public async Task<Response<bool>> EditDepartment(EditDepartmentVM editDepartmentVM)
        {
            var department = await _departmentRepository.GetByIdAsync(editDepartmentVM.Id);
            if (department == null)
                return new Response<bool>
                {
                    Data = false,
                    ResponseCode = ResponseCodeEnum.InvalidOperation,
                    ErrorMsg = $"There is no department with the Id {department.Id}"
                };
            var updatedDepartment = _mapper.Map<Department>(editDepartmentVM);
            _unitOfWork.BeginTransaction();
            _departmentRepository.Update(updatedDepartment);
            var (res, error) = await FireDomainEventsForEditDepartment(department, updatedDepartment);
            if (!res)
                return new Response<bool>
                {
                    Data = false,
                    ResponseCode = ResponseCodeEnum.InvalidOperation,
                    ErrorMsg = string.IsNullOrEmpty(error) ? "Invalid operation" : error,
                };

            var saved = _unitOfWork.CommitTransaction();
            if (!saved)
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

        public async Task<Response<List<GetDepartmentVM>>> GetAllDepartments()
        {
            var departments = await _departmentRepository.GetAllAsync();
            var getDepartmentVMs = _mapper.Map<List<GetDepartmentVM>>(departments);
            return new Response<List<GetDepartmentVM>>
            {
                Data = getDepartmentVMs,
                ResponseCode = ResponseCodeEnum.Success,
            };
        }

        public async Task<Response<bool>> DeleteDepartment(int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId);
            if (department == null)
                return new Response<bool>
                {
                    Data = false,
                    ResponseCode = ResponseCodeEnum.InvalidOperation,
                    ErrorMsg = $"There is no department with the Id {department.Id}"
                };
            _departmentRepository.Delete(department);
            var request = await _departmentRepository.SaveEntitiesAsync();
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

        public async Task<Response<EditDepartmentVM>> GetDepartmentForEdit(int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId);
            if (department == null)
                return new Response<EditDepartmentVM>
                {
                    Data = null,
                    ResponseCode = ResponseCodeEnum.InvalidOperation,
                    ErrorMsg = $"There is no department with the Id {departmentId}"
                };
            var editEmployeeVM = _mapper.Map<EditDepartmentVM>(department);
            return new Response<EditDepartmentVM>
            {
                Data = editEmployeeVM,
                ResponseCode = ResponseCodeEnum.Success,
            };
        }

        #region DomainEvents
        private async Task<(bool, string)> FireDomainEventsForCreateDepartment(Department department)
        {
            _domainEventManager.AddDomainEvent(new DepartmentCreatedDomainEvent
            {
                Department = department,
            });
            return await _domainEventManager.DispatchDomainEvents();
        }
        private async Task<(bool, string)> FireDomainEventsForEditDepartment(Department oldDepartment, Department updatedDepartment)
        {
            _domainEventManager.AddDomainEvent(new DepartmentEditedDomainEvent
            {
                OldDepartment = oldDepartment,
                UpdatedDepartment = updatedDepartment
            });
            return await _domainEventManager.DispatchDomainEvents();
        }

        #endregion

    }
}
