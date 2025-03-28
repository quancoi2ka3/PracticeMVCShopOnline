﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanHangMVC.Models.EF
{
    [Table("tb_Order")]
    public class Order : CommonAbstract
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
