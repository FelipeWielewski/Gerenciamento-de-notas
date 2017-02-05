using NeoMode.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoMode.Model
{
    public class StudentModel
    {
        public string FullName { get; set; }
        public string RegistryCode { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public int? CityId { get; set; }
        public bool? Approved { get; set; }
        public int Id { get; set; }
        public string Phone { get; set; }
        public List<Exam> Exams { get; set; }
    }
}
