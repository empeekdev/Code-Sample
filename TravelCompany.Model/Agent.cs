using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace TravelCompany.Model
{
    [Table("Agent")]
    [Serializable]
    public class Agent
    {
        public Agent() { }

        [XmlIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public long Id { get; set; }

        [Key]
        [Required]
        public Guid UUID { get; set; }

        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [XmlIgnore]
        public TravelAgency TravelAgency { get; set; }
    }
}
