using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoMode.Core;
using NeoMode.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace NeoMode.Service.Services
{
    public class CityService : ICityService
    {
        private readonly ApplicationDbContext _dbContext;
        public CityService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        

        public string Test()
        {
            return "teste dEPENDENCY";
        }
        public IEnumerable<City> GetAll()
        {
            return _dbContext.Cities.ToList();
        }
        public City GetById(int Id)
        {
            return _dbContext.Cities.Where(X => X.Id == Id).ToList().FirstOrDefault();
        }
        public City GetByCode(string Initials)
        {
            return _dbContext.Cities.Where(X => X.Initials == Initials).ToList().FirstOrDefault();
        }
        public void InsertCity(City CityToInsert)
        {
            var newCity = new City();

            newCity.Initials = CityToInsert.Initials;
            newCity.Description = CityToInsert.Description;
            newCity.Id = 0;

            //Salvando
            _dbContext.Cities.Add(newCity);

            _dbContext.SaveChanges();
        }
        public bool UpdateCity(City CityToUpdate)
        {
            try
            {
                var cityOld = GetById(CityToUpdate.Id);
                cityOld.Initials = CityToUpdate.Initials;
                cityOld.Description = CityToUpdate.Description;
                
                _dbContext.Entry(cityOld).State = EntityState.Modified;
                
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
