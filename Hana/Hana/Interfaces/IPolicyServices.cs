using Hana.Models.DataModels;

namespace Hana.Interfaces
{
    public interface IPolicyServices
    {
        IEnumerable<Policy> GetListPolicies();
        Policy GetDetails(int id);
        void Create(Policy policy);
        void UpdatePolicy(Policy policy);
        void DeletePolicy(int id);
        bool IsExist(int id);
    }
}
