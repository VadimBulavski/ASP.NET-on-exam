using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository
    {
        IEnumerable<Race> GetAllRaces();
        Race GetRace(int id);
        IEnumerable<Car> GetAllCars();
        Car GetCar(int id);

        IEnumerable<Race> GetNameRace(string name);
    }
}
