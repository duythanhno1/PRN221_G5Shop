namespace PRN221_MVC.Models {
    public class CommentViewModel {
        public Guid ID { get; set; }
        public string Content { get; set; }
        public bool isDeleted { get; set; }
        public long productId { get; set; }
    }
}
