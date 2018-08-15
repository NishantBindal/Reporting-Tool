using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ReportingTool.DataAccessLayer.Models
{
    public class VoucherType
    {
        public int VoucherTypeId { get; set; }
        [Column(TypeName = "varchar(100)")]
        [Required]
        public string Type { get; set; }
    }
}
