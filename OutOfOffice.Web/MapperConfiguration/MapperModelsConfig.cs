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
        
        CreateMap<Employee, EmployeeModel>()
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
        CreateMap<ApprovalRequestViewModel, ApprovalRequestModel>()
            .ReverseMap();  
        CreateMap<ApprovalRequestCreateModel, ApprovalRequestModel>()
            .ReverseMap();  
        CreateMap<ApprovalRequestUpdateModel, ApprovalRequestModel>()
            .ReverseMap();  
        CreateMap<AbsenceReasonViewModel, AbsenceReason>()
            .ReverseMap();
        
        CreateMap<LeaveRequest, LeaveRequestModel>()
            .ReverseMap();
        CreateMap<LeaveRequestModel, LeaveRequestViewModel>()
            .ReverseMap();
        CreateMap<LeaveRequestFullViewModel, LeaveRequestModel>()
            .ReverseMap();
        CreateMap<LeaveRequestCreateModel, LeaveRequestModel>()
            .ReverseMap();
        CreateMap<LeaveRequestUpdateModel, LeaveRequestModel>()
            .ReverseMap();
        
        CreateMap<Project, ProjectModel>()
            .ReverseMap();
        
        CreateMap<Employee, BaseEmployeeModel>()
            .ReverseMap();
        
        CreateMap<BaseManagerModel, ManagerCreateModel>()
            .ReverseMap();
        CreateMap<ProjectManagerModel, ManagerCreateModel>()
            .ReverseMap();
        CreateMap<HrManagerModel, ManagerCreateModel>()
            .ReverseMap();
        
        CreateMap<Employee, BaseEmployeeEntity>()
            .ReverseMap();
        
        CreateMap<EmployeeModel, BaseEmployeeEntity>()
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
        CreateMap<BaseManagerEntity, ManagerViewModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.GetType().Name))
            .ReverseMap();


        
        CreateMap<HrManagerModel, ManagerViewModel>()
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
        
        CreateMap<SelectionViewModel, SelectionModel>()
            .ReverseMap();
        CreateMap<SelectionViewModel, SelectionModel>()
            .ReverseMap();
        CreateMap<SelectionViewModel, SelectionModel>()
            .ReverseMap();
        
        CreateMap<SelectionModel, Position>()
            .ForMember(m => m.Employees, o => o.Ignore())
            .ReverseMap();
        CreateMap<SelectionModel, Subdivision>()
            .ForMember(m => m.Employees, o => o.Ignore())
            .ReverseMap();
        CreateMap<SelectionModel, ProjectType>()
            .ForMember(m => m.Projects, o => o.Ignore())
            .ReverseMap();
        
        
        CreateMap<EmployeeViewModel, EmployeeModel>()
            .ReverseMap();
        CreateMap<BaseEmployeeViewModel, EmployeeModel>()
            .ReverseMap();
        CreateMap<EmployeeCreateModel, EmployeeModel>()
            .ReverseMap();
        
        CreateMap<ProjectViewModel, ProjectModel>()
            .ReverseMap();
        CreateMap<ProjectCreateModel, ProjectModel>()
            .ReverseMap();
        CreateMap<ProjectUpdateModel, ProjectModel>()
            .ReverseMap();

        CreateMap<BaseEmployeeModel, CurrentUserViewModel>()
            .ForMember(dest => dest.UserType, opt => opt.Ignore())
            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo != null ? Convert.ToBase64String(src.Photo) : null))
            .ReverseMap();

        CreateMap<BaseEmployeeEntity, CurrentUserViewModel>()
            .ForMember(dest => dest.UserType, opt => opt.Ignore())
            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo != null ? Convert.ToBase64String(src.Photo) : null))
            .ReverseMap();


    }
}