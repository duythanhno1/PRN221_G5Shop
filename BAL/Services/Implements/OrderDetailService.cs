using DAL.Infacstucture;
using DAL.Repositories.Interface;

namespace BAL.Services.Implements {
    public class OrderDetailService : IOrderDetailService {
        private IOrderDetailRepository _orderDetailRepository;
        private IUnitOfWork _unitOfWork;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork) {
            _orderDetailRepository = orderDetailRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
