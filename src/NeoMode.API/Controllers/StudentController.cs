using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NeoMode.Services.Services;
using NeoMode.API.Model;
using System.Net.Http;
using System.Net;
using NeoMode.Core.Domain;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NeoMode.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IExamService _examService;
        public StudentController(IStudentService studentService, IExamService examService)
        {
            this._studentService = studentService;
            this._examService = examService;
        }

        [AllowAnonymous]
        [HttpGet]
        public void TestInsertNewUser(string testeNewUser)
        {
            InsertStudent(new StudentModel()
            {
                FullName = "Teste",
                RegistryCode = (new Random().Next(1, 5000)).ToString().PadLeft(10)
            });
        }
        /// <summary>
        /// Insere um novo aluno
        /// </summary>
        /// <param name="Student"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertStudent(StudentModel Student)
        {
            try
            {
                var result = _studentService.GetByRegistryCode(Student.RegistryCode);
                if (result == null)
                {
                    _studentService.InsertStudent(new Student()
                    {
                        RegistryCode = Student.RegistryCode,
                        Email = "",
                        FullName = Student.FullName,
                        ProfileImage = "",
                        Id = 0                        
                    });

                    return Ok();
                }
            }
            catch (Exception e)
            {

            }
            return BadRequest();
        }
        

    }
}
