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
        public City City { get; set; }
        public List<Exam> Exams { get; set; }
        public string ApprovedText
        {
            get
            {
                if (Approved == null)
                    return "Não disponivel";
                else if (Approved == true)
                    return "Aprovado";
                else
                    return "Reprovado";
            }
        }
        public string AverageScore
        {
            get
            {
                if (Exams != null && Exams.Count() > 0)
                {
                    return (Exams.Sum(x => x.Score) / 4).ToString();
                }
                return "-";
            }
        }
        public string TotalScore
        {
            get
            {
                if (Exams != null && Exams.Count() > 0)
                {
                    return Exams.Sum(x => x.Score).ToString();
                }
                return "-";
            }
        }
    }
}
