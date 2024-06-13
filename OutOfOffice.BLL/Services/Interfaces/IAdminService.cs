using OutOfOffice.BLL.Models.Employees;

namespace OutOfOffice.BLL.Services.Interfaces;

public interface IAdminService
{
    Task<AdminModel> CreateAdmin(int adminId, AdminModel adminModel, CancellationToken cancellationToken = default);
    Task<AdminModel> UpdateAdmin(int adminId, AdminModel adminModel, CancellationToken cancellationToken = default);
    Task<AdminModel> DeleteAdmin(int adminId, AdminModel adminModel, CancellationToken cancellationToken = default);
    
}