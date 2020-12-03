using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace TravelCompany.Model
{
    [Table("TravelAgency")]
    [Serializable]
    public class TravelAgency
    {
        public TravelAgency() { }

        [XmlIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Key]
        [Required]
        public Guid UUID { get; set; }

        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        [XmlArrayItem()]
        public List<Agent> Agents { get; set; }
    }
}
