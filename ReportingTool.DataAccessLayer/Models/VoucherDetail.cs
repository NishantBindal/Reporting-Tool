using ReportingTool.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingTool.DataAccessLayer
{
    public class VoucherDetail
    {
        public int VoucherDetailId { get; set; }
        //one to many
        public Product ProductName { get; set; }
        public int Quantity { get; set; }
        //one to many
        public Voucher VoucherId { get; set; }
    }
}
