using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ReportingTool.DataAccessLayer.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Column(TypeName = "varchar(100)")]
        [Required]
        public string Name { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
