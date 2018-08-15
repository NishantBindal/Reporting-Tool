using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ReportingTool.DataAccessLayer.Models
{
    public class Organization
    {
        public int OrganizationId { get; set; }
        [Column(TypeName = "varchar(100)")]
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string Address { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string GstNumber { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string PanNumber { get; set; }
        //crosscheck
        public User ContactPerson { get; set; }
    }
}
