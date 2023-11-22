using Hana.Models.DataModels;
using Hana.Models.ViewModels;

namespace Hana.Interfaces
{
    public interface IRealEstateServices
    {
        List<VM_RealEstate> GetList();
        List<VM_Location> GetAllActiveLocation();

        IQueryable<RealEstateViewModel> GetRealEstates(string searchKey);

        List<RealEstateViewModel> GetUserAllPosts(int? userId);

        List<RealEstateViewModel> GetCustomerConFirmList();
        List<RealEstateViewModel> CustomerExpireList();

        IQueryable<Result> Filter(Condition condition);
        Task<VM_RealEstateDetails> GetRealEstateDetails(int? id);
        int AddNewRealEstate(RealEstate realEstate);
        bool AddRealEstateDetails(RealEstateDetail details);
        bool AddMapForRealEstate(Map map);
        int AddCompleteRealEstate(VM_RealEstateDetails details, int agentId);
        bool UpdateRealEstate(VM_RealEstateDetails details);
        int DeleteRealEstate(int id, int userId);
        int DeleteMultipleRealEstates(List<int> ids);


        bool DisableRealEstate(int id);
        bool EnableRealEstate(int id);
        bool BookedRealEstate(int id);
        void ActiveRealEstate(int id);
        bool ConfirmRealEsate(int id, int confirmType);
        IEnumerable<RealEstateType> GetRealEstateTypeList();
        IEnumerable<City> GetCityList();
        bool IsExistRealEstate(int id);

        Tuple<int?, int?, int> GetLocation(string address);
        List<Result> GetRecommendList(int? id);
        List<District> GetDistrictList();
    }
}
