using NeoMode.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoMode.Services.Services
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAll();
        Student GetById(int Id);
        IEnumerable<Student> SearchStudentByName(string queryName);
        Student GetByEmail(string Email);
        Student GetByRegistryCode(string RegistryCode);
        bool UpdateProfileImageByUserId(int userId, string newProfileImageUrl);
        void InsertStudent(Student StudentToInsert);
        bool UpdateStudent(Student StudentToUpdate);
    }
}
