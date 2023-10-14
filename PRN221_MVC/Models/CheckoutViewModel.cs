using Microsoft.AspNetCore.Mvc;

namespace PRN221_MVC.Models
{
    [BindProperties]
    public class CheckoutViewModel
    {
        public AccountData Account { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public float Total { get; set; }

        public CheckoutViewModel(AccountData account, List<OrderDetail> orderDetails)
        {
            Account = account;
            OrderDetails = orderDetails;
            Total = orderDetails == null ? 0 : orderDetails.Sum(od => od.UnitPrice * od.Quantity);
        }

        public class AccountData
        {
            public AccountData(string name, string email, string? address) { 
                Name = name;
                Email = email;
                Address = address;
            }
            public string Name { get; set; }
            public string Email { get; set; }   
            public string? Address { get; set; }
        }
        public class OrderDetail
        {
            public OrderDetail(long productID, string name, int quantity, float unitPrice)
            {
                ProductID= productID;
                Name = name;
                Quantity = quantity;
                UnitPrice = unitPrice;
            }
        
            public long ProductID { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
            public float UnitPrice { get; set; }
        }
    }
}
