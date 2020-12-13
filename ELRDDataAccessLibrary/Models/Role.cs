using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ELRDDataAccessLibrary.Models
{
    public class Role
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string UserRole { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
