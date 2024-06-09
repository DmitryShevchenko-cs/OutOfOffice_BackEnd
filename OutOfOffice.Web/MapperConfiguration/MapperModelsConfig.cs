using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.Web.Models;

namespace OutOfOffice.Web.MapperConfiguration;

public class MapperModelsConfig : AutoMapper.Profile
{
    public MapperModelsConfig()
    {
        CreateMap<ProjectManager, ProjectManagerCreateModel>()
            .ReverseMap();
    }
}