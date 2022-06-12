using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagement.Services.Mapper
{
    public class MapperConfigurator
    {
        public static MapperConfiguration ConfigureMappings(IConfiguration configs)
        {
            var mapperConfiguration = new MapperConfiguration(mapperConfigs =>
            {
                EmployeeMapper.ConfigureMapping(mapperConfigs);
                DepartmentMapper.ConfigureMapping(mapperConfigs);
            });

            return mapperConfiguration;
        }
    }
}
