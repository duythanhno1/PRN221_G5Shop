using DAL.Entities;
using DAL.Infacstucture;
using DAL.Repositories.Interface;

namespace DAL.Repositories.Implements {
    public class CommentRepository : FRMDbContextBase<Comment>, ICommentRepository {
        private readonly FRMDbContext _dbContext;
        public CommentRepository(IDbFactory dbFactory) : base(dbFactory) {
            _dbContext = dbFactory.Init();
        }

        void ICommentRepository.Create(Comment comment) {
            _dbContext.Add(comment);
            _dbContext.SaveChanges();
        }

        public void Update(Comment comment) {
            Comment old = _dbContext.Comment.Find(comment.ID);
            if (old != null) {
                old.Content = comment.Content;
                _dbContext.Comment.Update(old);
                _dbContext.SaveChanges();
            }
        }
    }
}
