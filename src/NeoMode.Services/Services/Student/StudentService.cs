using Microsoft.EntityFrameworkCore;
using NeoMode.Core;
using NeoMode.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoMode.Services.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _dbContext;
        public StudentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Student> GetAll()
        {
            return _dbContext.Student.ToList();
        }
        public Student GetById(int Id)
        {
            return _dbContext.Student.Where(X => X.Id == Id).ToList().FirstOrDefault();
        }
        public IEnumerable<Student> SearchStudentByName(string queryName)
        {
            return _dbContext.Student.Where(X => X.FullName.Contains(queryName)).ToList();
        }
        public Student GetByEmail(string Email)
        {
            return _dbContext.Student.Where(X => X.Email == Email).ToList().FirstOrDefault();
        }
        public Student GetByRegistryCode(string RegistryCode)
        {
            return _dbContext.Student.Where(X => X.RegistryCode == RegistryCode).ToList().FirstOrDefault();
        }
        public bool UpdateProfileImageByUserId(int userId, string newProfileImageUrl)
        {
            try
            {
                var old = GetById(userId);
                old.ProfileImage = newProfileImageUrl;

                return UpdateStudent(old);
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public void InsertStudent(Student StudentToInsert)
        {
            var newStudent = new Student();

            newStudent.CityId = StudentToInsert.CityId;
            newStudent.Email = StudentToInsert.Email;
            newStudent.FullName = StudentToInsert.FullName;
            newStudent.ProfileImage = StudentToInsert.ProfileImage;
            newStudent.RegistryCode = StudentToInsert.RegistryCode;
            newStudent.Id = 0;

            //Salvando
            _dbContext.Student.Add(newStudent);

            _dbContext.SaveChanges();
        }
        public bool UpdateStudent(Student StudentToUpdate)
        {
            try
            {
                var old = GetById(StudentToUpdate.Id);
                old.CityId = StudentToUpdate.CityId;
                old.Email = StudentToUpdate.Email;
                old.FullName = StudentToUpdate.FullName;
                old.ProfileImage = StudentToUpdate.ProfileImage;
                old.RegistryCode = StudentToUpdate.RegistryCode;

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
