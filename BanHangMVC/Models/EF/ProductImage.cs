using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanHangMVC.Models.EF
{
    [Table("tb_ProductImage")]
    public class ProductImage
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string Image { get; set; }
        public bool IsDefault { get; set; }
        public virtual Products Products { get; set; }

    }
}
