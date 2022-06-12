using AutoMapper;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.ViewModels.Department;

namespace EmployeeManagement.Services.Mapper
{
    public class DepartmentMapper
    {
        public static void ConfigureMapping(IMapperConfigurationExpression mapperConfigs)
        {
            mapperConfigs.CreateMap<CreateDepartmentVM, Department>(); 

            mapperConfigs.CreateMap<EditDepartmentVM, Department>().ReverseMap();

            mapperConfigs.CreateMap<Department, GetDepartmentVM>()
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager.Name));
        }
    }
}
