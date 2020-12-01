using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelCompany.Model
{
    [Table("TravelAgency")]
    public class TravelAgency
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
