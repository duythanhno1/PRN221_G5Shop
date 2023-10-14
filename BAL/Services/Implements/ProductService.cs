using DAL;
using DAL.Entities;
using DAL.Infacstucture;
using DAL.Repositories.Interface;

namespace BAL.Services.Implements {
    public class ProductService : IProductService {
        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService() {
        }

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public List<Product> Filter(int id) {
            using (var context = new FRMDbContext()) {
                var products = context.Product
                                .Where(p => p.Category.ID == id)
                                .ToList();
                return products;
            }
        }

        public List<Product> Search(string searchItems) {
            using (var context = new FRMDbContext()) {
                var results = context.Product
                                .Where(p => p.Name.Contains(searchItems) || p.Category.Name.Equals(searchItems))
                                .ToList();
                return results;
            }
        }
        public Product? GetProductById(long id)
        {
            return _productRepository.GetProductAsync(id).Result;
        }

        public List<Product> sortPro(int sortId, List<Product> product) {
            List<Product> sortList = new List<Product>();

            switch (sortId) {
                case 1:
                    sortList = product.OrderBy(p => p.Price).ToList();
                    break;
                case 2:
                    sortList = product.OrderBy(p => p.Name).ToList();
                    break;
                case 3:
                    sortList = product.OrderByDescending(p => p.Price).ToList();
                    break;
                case 4:
                    sortList = product.OrderByDescending(p => p.Name).ToList();
                    break;
                default:
                    sortList = product;
                    break;
            }
            return sortList;
        }
    }
}
