using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanHangMVC.Models.EF
{
    [Table("tb_New")]
    public class New : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required(ErrorMessage = "Bạn không được để trống tiêu đề tin")]
        [StringLength(150)]
        public string Title { get; set; }
        public string? Alias { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [DataType(DataType.MultilineText)]
        public string Detail { get; set; }
        public string Image { get; set; }
        public int CategoryID { get; set; }
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoKeyword { get; set; }
        public virtual Category? Category { get; set; }
    }
}
