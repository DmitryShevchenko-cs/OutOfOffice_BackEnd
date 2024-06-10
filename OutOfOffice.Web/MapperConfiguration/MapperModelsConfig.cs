using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.DAL.Entity;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.Web.Models;
using AuthorizationInfo = OutOfOffice.DAL.Entity.AuthorizationInfo;

namespace OutOfOffice.Web.MapperConfiguration;

public class MapperModelsConfig : AutoMapper.Profile
{
    public MapperModelsConfig()
    {
        CreateMap<BaseEmployeeEntity, BaseEmployeeModel>()
            .ReverseMap();
        
        CreateMap<Admin, AdminModel>()
            .ReverseMap();
        
        CreateMap<BaseManagerEntity, BaseManagerModel>()
            .ReverseMap();
        
        CreateMap<GeneralEmployee, GeneralEmployeeModel>()
            .ReverseMap();
        
        CreateMap<HrManager, HrManagerModel>()
            .ReverseMap();
        
        CreateMap<ProjectManager, ProjectManagerModel>()
            .ReverseMap();
        
        CreateMap<AuthorizationInfoModel, AuthorizationInfo>()
            .ForMember(m => m.EmployeeId, o => o.Ignore())
            .ForMember(m => m.Employee, o => o.Ignore())
            .ReverseMap();
        
        CreateMap<ApprovalRequest, ApprovalRequestModel>()
            .ReverseMap();
        
        CreateMap<LeaveRequest, LeaveRequestModel>()
            .ReverseMap();
        
        CreateMap<Project, ProjectModel>()
            .ReverseMap();
        
        CreateMap<GeneralEmployee, BaseEmployeeModel>()
            .ReverseMap();
        
        CreateMap<BaseManagerModel, EmployeeCreateModel>()
            .ReverseMap();
        CreateMap<ProjectManagerModel, EmployeeCreateModel>()
            .ReverseMap();
        CreateMap<HrManagerModel, EmployeeCreateModel>()
            .ReverseMap();
        
        CreateMap<BaseManagerModel, ProjectManager>()
            .ReverseMap();
        
        CreateMap<BaseManagerModel, HrManager>()
            .ReverseMap();
        
        CreateMap<GeneralEmployee, BaseEmployeeEntity>()
            .ReverseMap();
        
        CreateMap<BaseManagerEntity, ProjectManager>()
            .ReverseMap();
        
        CreateMap<BaseManagerModel, EmployeeUpdateModel>()
            .ReverseMap();
        

    }
}