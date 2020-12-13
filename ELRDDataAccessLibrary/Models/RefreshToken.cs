using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ELRDDataAccessLibrary.Models
{
    public class RefreshToken
    {
        [Key]
        public Guid Token { get; set; }

        public string JwtId { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }
        public bool Invalidated { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User Owner { get; set; }
    }
}
