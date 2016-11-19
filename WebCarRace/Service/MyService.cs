using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MyService : IService
    {
        private IRepository _repository = null;
        public MyService(IRepository repo)
        {
            _repository = repo;
        }

        public IEnumerable<DomainObject.Race> GetAllRaces()
        {
            return _repository.GetAllRaces();
        }

        public DomainObject.Race GetRace(int id)
        {
            return _repository.GetRace(id);
        }

        public IEnumerable<DomainObject.Car> GetAllCars()
        {
            return _repository.GetAllCars();
        }

        public DomainObject.Car GetCar(int id)
        {
            return _repository.GetCar(id);
        }

        public IEnumerable<DomainObject.Race> GetNameRace(string name)
        {
            return _repository.GetNameRace(name);
        }
    }
}
