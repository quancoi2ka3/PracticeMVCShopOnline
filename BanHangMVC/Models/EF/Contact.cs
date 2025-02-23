using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanHangMVC.Models.EF
{
    [Table("tb_Contact")]
    public class Contact : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [StringLength(150, ErrorMessage = "Không được vượt quá 150 kí tự")]
        public string Name { get; set; }
        [StringLength(150, ErrorMessage = "Không được vượt quá 150 kí tự")]
        public string Email { get; set; }
        [StringLength(4000)]
        public string Website { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}
