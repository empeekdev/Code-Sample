using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace TravelCompany.DataAccess
{
    [Table("Agency")]
    [Serializable]
    public class Agency
    {
        // The empty constructor is needed for the xml serialization/deserialization
        public Agency() { }

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
