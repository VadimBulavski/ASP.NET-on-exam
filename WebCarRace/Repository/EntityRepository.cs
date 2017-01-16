using RaceContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EntityRepository:IRepository
    {
        private RaceCarContext _context = null;
        public EntityRepository(RaceCarContext cont)
        {
            _context = cont;
        }
        public IEnumerable<DomainObject.Race> GetAllRaces()
        {
            return _context.Races.Select(r => r).ToList();
        }

        public DomainObject.Race GetRace(int id)
        {
            return _context.Races.Where(r => r.RaceID == id).FirstOrDefault();
        }


        public IEnumerable<DomainObject.Car> GetAllCars()
        {
            return _context.Cars.Select(c => c).ToList();
        }

        DomainObject.Car IRepository.GetCar(int id)
        {
            return _context.Cars.Where(c => c.CarID == id).FirstOrDefault();
        }


        public IEnumerable<DomainObject.Race> GetNameRace(string name)
        {
            return _context.Races.Where(r => r.Name.Contains(name)).ToList();
        }

        public DomainObject.Car RemoveCar(int id)
        {
            return _context.Cars.Remove(_context.Cars.Where(s => s.CarID == id).FirstOrDefault());
        }

        public DomainObject.Race RemoveRace(int id)
        {
            return _context.Races.Remove(_context.Races.Where(s => s.RaceID == id).FirstOrDefault());
        }
    }
}
