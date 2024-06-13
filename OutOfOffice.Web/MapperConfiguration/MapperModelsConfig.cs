using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.DAL.Entity;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Entity.Selections;
using OutOfOffice.Web.Models;
using AuthorizationInfo = OutOfOffice.DAL.Entity.AuthorizationInfo;

namespace OutOfOffice.Web.MapperConfiguration;

public class MapperModelsConfig : AutoMapper.Profile
{
    public MapperModelsConfig()
    {
        CreateMap<BaseEmployeeEntity, BaseEmployeeModel>()
            .ReverseMap();
        
        CreateMap<EmployeeModel, BaseEmployeeModel>()
            .ReverseMap();
        
        CreateMap<Admin, AdminModel>()
            .ReverseMap();
        
        CreateMap<BaseManagerEntity, BaseManagerModel>()
            .ReverseMap();
        
        CreateMap<DAL.Entity.Employees.Employee, EmployeeModel>()
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
        CreateMap<AbsenceReasonViewModel, AbsenceReason>()
            .ReverseMap();
        
        CreateMap<LeaveRequest, LeaveRequestModel>()
            .ReverseMap();
        CreateMap<LeaveRequestViewModel, LeaveRequestModel>()
            .ReverseMap();
        CreateMap<LeaveRequestFullViewModel, LeaveRequestModel>()
            .ReverseMap();
        CreateMap<LeaveRequestCreateModel, LeaveRequestModel>()
            .ReverseMap();
        CreateMap<LeaveRequestUpdateModel, LeaveRequestModel>()
            .ReverseMap();
        
        CreateMap<Project, ProjectModel>()
            .ReverseMap();
        
        CreateMap<DAL.Entity.Employees.Employee, BaseEmployeeModel>()
            .ReverseMap();
        
        CreateMap<BaseManagerModel, ManagerCreateModel>()
            .ReverseMap();
        CreateMap<ProjectManagerModel, ManagerCreateModel>()
            .ReverseMap();
        CreateMap<HrManagerModel, ManagerCreateModel>()
            .ReverseMap();
        
        CreateMap<DAL.Entity.Employees.Employee, BaseEmployeeEntity>()
            .ReverseMap();
        
        CreateMap<BaseManagerModel, ProjectManager>()
            .ReverseMap();
        CreateMap<BaseManagerModel, HrManager>()
            .ReverseMap();
        CreateMap<BaseManagerEntity, ProjectManager>()
            .ReverseMap();
        CreateMap<BaseManagerModel, ManagerUpdateModel>()
            .ReverseMap();
        CreateMap<BaseManagerModel, ManagerViewModel>()
            .ReverseMap();
        
        CreateMap<SelectionViewModel, Position>()
            .ForMember(m => m.Employees, o => o.Ignore())
            .ReverseMap();
        CreateMap<SelectionViewModel, Subdivision>()
            .ForMember(m => m.Employees, o => o.Ignore())
            .ReverseMap();
        CreateMap<SelectionViewModel, ProjectType>()
            .ForMember(m => m.Projects, o => o.Ignore())
            .ReverseMap();
        
        CreateMap<EmployeeViewModel, EmployeeModel>()
            .ReverseMap();
        CreateMap<EmployeeCreateModel, EmployeeModel>()
            .ReverseMap();
        
        CreateMap<ProjectViewModel, ProjectModel>()
            .ReverseMap();
        CreateMap<ProjectCreateModel, ProjectModel>()
            .ReverseMap();
        CreateMap<ProjectUpdateModel, ProjectModel>()
            .ReverseMap();
        

    }
}