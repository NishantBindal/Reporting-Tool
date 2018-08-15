using ReportingTool.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ReportingTool.DataAccessLayer
{
    public class User
    {
        public int UserId { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required]
        public string UserName { get; set; }
        [Column(TypeName = "varchar(100)")]
        [Required]
        public string Email { get; set; }
        [Column(TypeName = "varchar(10)")]
        [Required]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string FirstName { get; set; }
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string LastName { get; set; }
        //one to many
        public Role Role { get; set; }
        public int OrganizationId { get; set; }
        //one to many
        public Organization Organization { get; set; }

    }
}
