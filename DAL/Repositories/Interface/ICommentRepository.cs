using DAL.Entities;

namespace DAL.Repositories.Interface {
    public interface ICommentRepository {
        void Create(Comment comment);
        void Update(Comment comment);
    }
}
