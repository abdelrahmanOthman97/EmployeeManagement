using AutoMapper;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.ViewModels.Employee;

namespace EmployeeManagement.Services.Mapper
{
    public static class EmployeeMapper
    {
        public static void ConfigureMapping(IMapperConfigurationExpression mapperConfigs)
        {
            mapperConfigs.CreateMap<CreateEmployeeVM, Employee>();

            mapperConfigs.CreateMap<EditEmployeeVM, Employee>().ReverseMap();

            mapperConfigs.CreateMap<Employee, GetEmployeeVM>()
                 .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                 .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.Department.ManagerId == src.Id ? null : src.Department.ManagerId))
                 .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Department.ManagerId == src.Id ? null : src.Department.Manager.Name))
                 .ForMember(dest => dest.IsManager, opt => opt.MapFrom(src => src.Department.ManagerId == src.Id));

        }
    }
}
