using NeoMode.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoMode.Services.Services
{
    public interface IExamService
    {
        IEnumerable<Exam> GetAll();
        Exam GetById(int Id);
        IEnumerable<Exam> GetExamsByStudentId(int studentId);
        IEnumerable<Exam> GetExamsFromYearByStudentId(int studentId, DateTime dateYear);
        void InsertExam(Exam ExamToInsert);
        bool UpdateExam(Exam ExamToUpdate);
    }
}
