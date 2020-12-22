using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ELRDDataAccessLibrary.Models
{
    public class BaseUnit
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Callsign { get; set; }

        public int CrewCount { get; set; }

        [Required]
        public int UnitTye { get; set; }
    }
}
