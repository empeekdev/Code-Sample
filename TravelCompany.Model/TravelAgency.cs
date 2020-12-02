﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.Model
{
    [Table("TravelAgency")]
    public class TravelAgency
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public Guid UUID { get; set; }

        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        public IEnumerable<Agent> Agents { get; set; }
    }
}
