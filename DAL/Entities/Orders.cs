using System.ComponentModel.DataAnnotations;

namespace DAL.Entities {
    public class Orders {

        [Key]
        public Guid ID { get; set; }

        public float Total { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool isDeleted { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
