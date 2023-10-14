using BAL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;

namespace BAL.Services.Implements {
    public class CommentService : ICommentService {
        private readonly ICommentRepository commentRepository;

        public CommentService(ICommentRepository commentRepository) {
            this.commentRepository = commentRepository;
        }

        public void Create(Comment comment) {
            commentRepository.Create(comment);
        }

        public void Update(Comment comment) {
            commentRepository.Update(comment);
        }
    }
}
