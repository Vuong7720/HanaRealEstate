using Hana.Models.DataModels;

namespace Hana.Interfaces
{
    public interface ICommonServices
    {
        Task<IEnumerable<District>> GetDistrictsByCity(int? cityId);
        int? GetDistrictByAddress(string address);
        int? GetWardByAddress(string address);
    }
}
