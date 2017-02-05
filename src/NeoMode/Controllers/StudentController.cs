using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NeoMode.Services;
using NeoMode.Model;
using NeoMode.Services.Services;
using NeoMode.Core.Domain;
using NeoMode.Core.Domain.ExamConfig;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NeoMode.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IExamService _examService;
        private readonly IExamConfigService _examConfigService;
        private readonly IHostingEnvironment _environment;
        public StudentController(IStudentService studentService, IExamService examService, IExamConfigService examConfigService, IHostingEnvironment environment)
        {
            this._studentService = studentService;
            this._examService = examService;
            this._examConfigService = examConfigService;
            this._environment = environment;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            if (!AuthenticationHelp.isLogged())
                return RedirectToAction("Index", "Login");

            var model = new ListStudentModel();
            model.Students = _studentService.GetAll().ToList();

            return View(model);
        }
        public IActionResult Edit(int Id)
        {
            if (!AuthenticationHelp.isLogged())
                return RedirectToAction("Index", "Login");

            var model = new StudentModel();
            var configExams = _examConfigService.GetConfigByDate(DateTime.Now);

            var student = _studentService.GetById(Id);
            if (student != null)
            {
                model.FullName = student.FullName;
                model.RegistryCode = student.RegistryCode.Trim();
                model.Email = student.Email;
                model.ProfileImage = student.ProfileImage;
                model.CityId = student.CityId;
                model.Id = student.Id;
                model.Phone = student.Phone;

                var exams = _examService.GetExamsFromYearByStudentId(student.Id, DateTime.Now.Date);
                if (exams != null)
                {
                    model.Exams = exams.ToList();
                    model.Approved = VerifyStatus(exams.ToList(), configExams);
                }
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(StudentModel model, IFormFile file)
        {
            if (!AuthenticationHelp.isLogged())
                return RedirectToAction("Index", "Login");
            try
            {
                if (file != null)
                {
                    var imageUrl = await Upload(file, model.RegistryCode);
                    model.ProfileImage = imageUrl;
                }

                _studentService.UpdateStudent(new Student()
                {
                    RegistryCode = model.RegistryCode,
                    Id = model.Id,
                    Email = model.Email,
                    FullName = model.FullName,
                    ProfileImage = model.ProfileImage,
                    CityId = model.CityId,
                    Phone = model.Phone
                });
                TempData["Message"] = "Atualizado com sucesso!";
            }
            catch (Exception e)
            {
                TempData["Message"] = "Ocorreu um erro!";
            }
            return RedirectToAction("Edit", new { Id = model.Id });
        }
        private bool? VerifyStatus(List<Exam> exams, ExamConfig config)
        {
            if (exams != null && exams.Count() > 0)
            {
                var points = exams.Sum(x => x.Score);
                var examsPending = config.QuantityExam - exams.Count();

                if (points >= config.AverageScore)
                {
                    //Atingiu os pontos
                    return true;
                }
                else if ((points + (examsPending * config.MaxScore) >= config.AverageScore))
                {
                    //Não é possivel dizer
                    return null;
                }
                else if ((points + (examsPending * config.MaxScore) < config.AverageScore))
                {
                    return false;
                }


            }
            return null;
        }

        public async Task<string> Upload(IFormFile file, string RegistryCode)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, (RegistryCode + ".png")), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            return Path.Combine(Request.Scheme+"://"+Request.Host.Value + "/uploads/", (RegistryCode + ".png"));
        }
    }
}
