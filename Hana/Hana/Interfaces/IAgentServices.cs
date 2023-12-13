using Hana.Models.DataModels;
using Hana.Models.ViewModels;

namespace Hana.Interfaces
{
    public interface IAgentServices
    {
        Task<IEnumerable<Agent>> GetListAgent();
        Task<VM_Agent> GetDetails(int id);
        Task<bool> Disable(int id);
        Task<bool> Delete(int id);
        VM_Agent GetUserInfo(int id);
        bool UpdateProfile(VM_Agent updateProfile);
        int UpdatePassword(VM_ChangePassword data);
        Task<bool> Enable(int id);
        Task<bool> UpdateLevel(int id, int newLevelId);
        Task<IEnumerable<Level>> GetLevelList();
    }
}
