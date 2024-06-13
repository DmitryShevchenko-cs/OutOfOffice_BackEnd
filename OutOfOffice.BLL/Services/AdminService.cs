using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.BLL.Services.Interfaces;

namespace OutOfOffice.BLL.Services;

public class AdminService : IAdminService
{
    public Task<AdminModel> CreateAdmin(int adminId, AdminModel adminModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<AdminModel> UpdateAdmin(int adminId, AdminModel adminModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<AdminModel> DeleteAdmin(int adminId, AdminModel adminModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}