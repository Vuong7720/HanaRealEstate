using Hana.Models.DataModels;
using Hana.Models.ViewModels;

namespace Hana.Interfaces
{
    public interface IAccountServices
    {
        Agent GetUser(VM_Login login);
        Task<bool> RegisterUser(VM_Register registerUser);
        bool CheckExist(string loginName);
        bool IsValidUser(int userId, int realEstateId);
    }
}
