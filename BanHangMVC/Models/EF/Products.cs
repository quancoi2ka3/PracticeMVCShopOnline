using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanHangMVC.Models.EF
{
    [Table("tb_Products")]
    public class Products : CommonAbstract
    {
        public Products()
        {
            this.ProductImage = new HashSet<ProductImage>();
            this.OrderDetail = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        public string? Alias { get; set; }
        [StringLength(500)]
        public string ProductCode { get; set; }
        public string Description { get; set; }
        [DataType(DataType.MultilineText)]
        public string Detail { get; set; }
        public string Image { get; set; }
        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá không hợp lệ")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Sai định dạng số")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Display(Name = "Giá khuyến mại")]
        [Required(ErrorMessage = "Vui lòng nhập giá khuyến mại")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá khuyến mại không hợp lệ")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Sai định dạng số")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceSale { get; set; }
        public int Quantity { get; set; }
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }
        public bool IsFeature { get; set; }
        public bool IsHot { get; set; }
        public int ProductCategoryID { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeyword { get; set; }
        public bool IsActive { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
