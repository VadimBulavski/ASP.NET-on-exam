using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IService
    {
        IEnumerable<Race> GetAllRaces();
        Race GetRace(int id);
        IEnumerable<Car> GetAllCars();
        Car GetCar(int id);

        IEnumerable<Race> GetNameRace(string name);
    }
}
