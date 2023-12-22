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
                AgentId = comment.AgentId, 
                RealEstateId = comment.RealEstateId, 
                Content = comment.Content,
                Ngaytao = comment.Ngaytao 
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
        public async Task AddReply(Comment reply)
        {
            _context.Comment.Add(reply);
            await _context.SaveChangesAsync();
        }
        public List<Comment> GetRepliesForComment(int parentCommentId)
        {
            var replies = _context.Comment
                .Where(c => c.ParentCommentId == parentCommentId)
                .OrderBy(c => c.Ngaytao)
                .ToList();

            return replies;
        }
    }
}
