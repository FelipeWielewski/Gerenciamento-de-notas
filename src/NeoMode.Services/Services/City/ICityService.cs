using NeoMode.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoMode.Service.Services
{
    public interface ICityService
    {
        IEnumerable<City> GetAll();
        City GetById(int Id);
        City GetByCode(string Initials);
        void InsertCity(City CityToInsert);
        bool UpdateCity(City CityToUpdate);

    }
}
