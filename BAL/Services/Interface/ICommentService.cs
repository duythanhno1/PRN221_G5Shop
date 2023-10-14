using DAL.Entities;

namespace BAL.Services.Interface {
    public interface ICommentService {
        void Create(Comment comment);
        void Update(Comment comment);
    }
}
