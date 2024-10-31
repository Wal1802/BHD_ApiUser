using BHD.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHD.Domain.Models
{
    public class Phone : Entity
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string CityCode { get; set; }
        public string CountryCode { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
