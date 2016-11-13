using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject
{
    public class Car
    {
        [Key]
        public int CarID { get; set; }
        public string NameCar { get; set; }
        public double Speed { get; set; }
        public double Distance { get; set; }
        public double DeltaAcceleration { get; set; }
        public int AccelerationInterval { get; set; }
        public int DurationOfAcceleration { get; set; }

        public Race RaceID { get; set; }
    }
}
