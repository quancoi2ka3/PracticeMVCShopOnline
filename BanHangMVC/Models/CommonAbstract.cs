﻿namespace BanHangMVC.Models
{
    public abstract class CommonAbstract
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
