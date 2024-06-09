using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.Web.Models;

namespace OutOfOffice.Web.MapperConfiguration;

public class MapperModelsConfig : AutoMapper.Profile
{
    public MapperModelsConfig()
    {
        CreateMap<ProjectManager, EmployeeCreateModel>()
            .ReverseMap();
        
        CreateMap<GeneralEmployeeModel, EmployeeCreateModel>()
            .ReverseMap();
        CreateMap<GeneralEmployeeModel, GeneralEmployee>()
            .ReverseMap();
        CreateMap<GeneralEmployee, BaseEmployeeModel>()
            .ReverseMap();
    }
}