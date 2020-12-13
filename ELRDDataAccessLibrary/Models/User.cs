using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ELRDDataAccessLibrary.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [MaxLength(100)]
        public string Firstname { get; set; }
        
        [MaxLength(100)]
        public string Lastname { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
