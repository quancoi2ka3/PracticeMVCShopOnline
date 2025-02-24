using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanHangMVC.Models.EF
{
    [Table("tb_Category")]
    public class Category : CommonAbstract
    {
        public Category()
        {
            this.News = new HashSet<New>();
            this.Posts = new HashSet<Post>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        [StringLength(150)]
        public string Title { get; set; }
        [StringLength(500)]
        public string? Alias { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        [StringLength(250)]
        public string? SeoTitle { get; set; }
        [StringLength(250)]
        public string? SeoDescription { get; set; }
        [StringLength(150)]
        public string? SeoKeyword { get; set; }
        public bool IsActive { get; set; }
        public int Position { get; set; } = 1;
        public ICollection<New> News { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
