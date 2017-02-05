using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoMode.API.Model
{
    public class ExamModel
    {
        public string RegistryCodeStudent { get; set; }
        public decimal ExamScore { get; set; }
        public int ExamNumber { get; set; }
        public DateTime Date { get; set; }
    }
}
