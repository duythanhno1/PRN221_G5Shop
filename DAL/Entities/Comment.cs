using System.ComponentModel.DataAnnotations;

namespace DAL.Entities {
    public class Comment {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string Content { get; set; }
        public bool isDeleted { get; set; }
        public string UserId { get; set; }
        public long ProductId { get; set; }
    }
}
