using NeoMode.Core.Domain.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoMode.Services.Services
{
    public interface ISchoolService
    {
        IEnumerable<School> GetAll();
        School GetById(int Id);
        void InsertSchool(School SchoolToInsert);
        bool UpdateSchool(School SchoolToUpdate);
        School GetByPrimaryKey(string PrimaryKey, string SecondaryKey);
    }
}
