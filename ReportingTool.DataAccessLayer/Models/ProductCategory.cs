using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ReportingTool.DataAccessLayer.Models
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        [Column(TypeName = "varchar(100)")]
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }
    }
}
