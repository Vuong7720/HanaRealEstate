using Hana.Models.DataModels;

namespace Hana.Interfaces
{
    public interface IFAQServices
    {
        IEnumerable<Faq> GetListFAQs();
        Faq GetDetails(int id);
        void Create(Faq faq);
        void UpdateFAQ(Faq faq);
        void DeleteFAQ(int id);
        bool IsExist(int id);
    }
}
