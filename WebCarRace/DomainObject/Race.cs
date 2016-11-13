using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject
{
    public class Race
    {
        [Key]
        public int RaceID{ get; set; }
        public string Name { get; set; }
        public double Distance { get; set; }

        public List<Car> Cars { get; set; }
    }
}
