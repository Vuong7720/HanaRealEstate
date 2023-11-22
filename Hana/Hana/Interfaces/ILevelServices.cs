using Hana.Models.DataModels;

namespace Hana.Interfaces
{
    public interface ILevelServices
    {
        IEnumerable<Level> GetListLevels();
        Level GetDetails(int id);
        void Create(Level level);
        void UpdateLevel(Level level);
        void DeleteLevel(int id);
        bool IsExist(int id);
    }
}
