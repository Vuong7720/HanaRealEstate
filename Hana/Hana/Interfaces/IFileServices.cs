using Hana.Models.DataModels;

namespace Hana.Interfaces
{
    public interface IFileServices
    {
        Dictionary<string, string> UploadFiles(List<IFormFile> files);
        int AddPicture(int realEstateId, List<IFormFile> files);
        Task<IEnumerable<Picture>> GetPicturesForRealEstate(int id);
        Task<bool> RemovePictureFromRealEstate(int pictureId);
        int? GetRealEstateId(int pictureId);
    }
}
