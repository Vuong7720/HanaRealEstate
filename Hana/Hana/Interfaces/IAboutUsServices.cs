using Hana.Models.DataModels;

namespace Hana.Interfaces
{
    public interface IAboutUsServices
    {
        IEnumerable<AboutUs> GetListAboutUs();
        IQueryable<AboutUs> GetData();
        AboutUs GetDetails(int id);
        void Create(AboutUs about);
        void UpdateAboutUs(AboutUs about);
        void DeleteAboutUs(int id);

        bool IsExist(int id);
    }
}
