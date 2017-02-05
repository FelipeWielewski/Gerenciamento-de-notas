using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NeoMode.Services.Services;
using Microsoft.AspNetCore.Authorization;
using NeoMode.API.Model;
using NeoMode.Core.Domain;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NeoMode.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ExamController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IExamService _examService;
        public ExamController(IStudentService studentService, IExamService examService)
        {
            this._studentService = studentService;
            this._examService = examService;
        }

        [AllowAnonymous]
        [HttpGet]
        public void TestInsertExamForStudent(string testeNewExam)
        {
            var student = _studentService.GetById(1);
            InsertExamForStudent(new ExamModel()
            {
                Date = DateTime.Now,
                ExamNumber = new Random().Next(1, 4),
                ExamScore = Convert.ToDecimal(new Random().NextDouble() * 10),
                RegistryCodeStudent = student.RegistryCode
            });
        }

        /// <summary>
        /// Insere uma nova nota para o aluno
        /// </summary>
        /// <param name="ExamModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertExamForStudent(ExamModel ExamModel)
        {
            try
            {
                var student = _studentService.GetByRegistryCode(ExamModel.RegistryCodeStudent);
                if (student != null)
                {
                    var exam = _examService.GetExamsFromYearByStudentId(student.Id, ExamModel.Date);
                    if (exam == null || (exam != null && exam.Where(X => X.ExamNumber == ExamModel.ExamNumber).FirstOrDefault() == null))
                    {
                        _examService.InsertExam(new Exam()
                        {
                            Date = ExamModel.Date,
                            ExamNumber = ExamModel.ExamNumber,
                            Id = 0,
                            Score = ExamModel.ExamScore,
                            StudentId = student.Id
                        });
                        return Ok();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return BadRequest();
        }
    }
}
