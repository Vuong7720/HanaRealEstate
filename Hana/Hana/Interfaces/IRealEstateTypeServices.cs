using Hana.Models.DataModels;

namespace Hana.Interfaces
{
    public interface IRealEstateTypeServices
    {
        IEnumerable<RealEstateType> GetList();
        RealEstateType GetDetails(int id);
        void Create(RealEstateType type);
        void UpdateType(RealEstateType type);
        void DeleteType(int id);
        bool IsExist(int id);
    }
}
