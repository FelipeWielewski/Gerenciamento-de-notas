using Microsoft.EntityFrameworkCore;
using NeoMode.Core;
using NeoMode.Core.Domain.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoMode.Services.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly ApplicationDbContext _dbContext;
        public SchoolService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<School> GetAll()
        {
            return _dbContext.School.ToList();
        }
        public School GetById(int Id)
        {
            return _dbContext.School.Where(X => X.Id == Id).ToList().FirstOrDefault();
        }

        public void InsertSchool(School SchoolToInsert)
        {
            var newSchool = new School();

            newSchool.ControlKey = SchoolToInsert.ControlKey;
            newSchool.Description = SchoolToInsert.Description;
            newSchool.SecondaryKey = SchoolToInsert.SecondaryKey;
            newSchool.Id = 0;

            //Salvando
            _dbContext.School.Add(newSchool);

            _dbContext.SaveChanges();
        }
        public bool UpdateSchool(School SchoolToUpdate)
        {
            try
            {
                var old = GetById(SchoolToUpdate.Id);
                old.ControlKey = SchoolToUpdate.ControlKey;
                old.Description = SchoolToUpdate.Description;
                old.SecondaryKey = SchoolToUpdate.SecondaryKey;

                _dbContext.Entry(old).State = EntityState.Modified;

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
