using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ReportingTool.DataAccessLayer.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string RoleType { get; set; }
    }
}
