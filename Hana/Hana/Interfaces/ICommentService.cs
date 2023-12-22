using Hana.Models.DataModels;

namespace Hana.Interfaces
{
    public interface ICommentService
    {
        Task AddComment(Comment comment);
        List<Comment> GetCommentsForRealEstate(int realEstateId);
    }
}
