using Hana.Data;
using Hana.Interfaces;
using Hana.Models.DataModels;

namespace Hana.Services
{
    public class CommentService : ICommentService
    {
        private readonly HanaContext _context;

        public CommentService(HanaContext context)
        {
            _context = context;
        }

        public async Task AddComment(Comment comment)
        {
            var newComment = new Comment
            {
                AgentId = comment.AgentId, // Đảm bảo set AgentId nếu cần
                RealEstateId = comment.RealEstateId, // Đảm bảo set RealEstateId
                Content = comment.Content,
                Ngaytao = comment.Ngaytao // Đảm bảo set Ngaytao theo CreatedAt của CommentModel
            };

            _context.Comment.Add(newComment);
            await _context.SaveChangesAsync();
        }

        public List<Comment> GetCommentsForRealEstate(int realEstateId)
        {
            var comments = _context.Comment
       .Where(c => c.RealEstateId == realEstateId)
       .OrderByDescending(c => c.Ngaytao)
       .Select(c => new Comment
       {
           Id = c.Id,
           AgentId = c.AgentId,
           Content = c.Content,
           Ngaytao = c.Ngaytao,
           RealEstateId = c.RealEstateId
       })
       .ToList();

            return comments;
        }
    }
}
