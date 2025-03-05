using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanHangMVC.Models.EF
{
    [Table("tb_ProductCategory")]
    public class ProductCategory : CommonAbstract
    {
        public ProductCategory()
        {
            this.Products = new HashSet<Products>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Alias { get; set; }
        [StringLength(250)]
        public string? Icon { get; set; }
        [StringLength(250)]
        public string? SeoTitle { get; set; }
        [StringLength(500)]
        public string SeoDescription { get; set; }
        [StringLength(250)]
        public string? SeoKeyword { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
