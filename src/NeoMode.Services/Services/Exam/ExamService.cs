using NeoMode.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NeoMode.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace NeoMode.Services.Services
{
    public class ExamService : IExamService
    {
        private readonly ApplicationDbContext _dbContext;
        public ExamService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Exam> GetAll()
        {
            return _dbContext.Exam.ToList();
        }
        public Exam GetById(int Id)
        {
            return _dbContext.Exam.Where(X => X.Id == Id).ToList().FirstOrDefault();
        }
        public IEnumerable<Exam> GetExamsByStudentId(int studentId)
        {
            return _dbContext.Exam.Where(x => x.StudentId == studentId).ToList();
        }
        public IEnumerable<Exam> GetExamsFromYearByStudentId(int studentId, DateTime dateYear)
        {
            return _dbContext.Exam.Where(x => x.StudentId == studentId && x.Date.Year == dateYear.Year).ToList();
        }
        public void InsertExam(Exam ExamToInsert)
        {
            var newExam = new Exam();

            newExam.Date = ExamToInsert.Date;
            newExam.ExamNumber= ExamToInsert.ExamNumber;
            newExam.Score = ExamToInsert.Score;
            newExam.StudentId = ExamToInsert.StudentId;
            newExam.Id = 0;

            //Salvando
            _dbContext.Exam.Add(newExam);

            _dbContext.SaveChanges();
        }
        public bool UpdateExam(Exam ExamToUpdate)
        {
            try
            {
                var old = GetById(ExamToUpdate.Id);
                old.Date = ExamToUpdate.Date;
                old.ExamNumber = ExamToUpdate.ExamNumber;
                old.Score = ExamToUpdate.Score;
                old.StudentId = ExamToUpdate.StudentId;

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
