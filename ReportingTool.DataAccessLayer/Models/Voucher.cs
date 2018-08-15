using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReportingTool.DataAccessLayer.Models
{
    public class Voucher
    {
        public int VoucherId { get; set; }
        //one to many
        public Organization Customer { get; set; }
        //one to many crosscheck
        public Organization SoldBy { get; set; }
        [Required]
        public DateTime VoucherDate { get; set; }
        [Required]
        public int Amount { get; set; }
        //one to many
        public VoucherType VoucherType { get; set; }
    }
}
