using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoMode.Core.Domain.ExamConfig
{
    public class ExamConfig
    {
        public int Id { get; set; }
        /// <summary>
        /// Quantidade de Exames por periodo
        /// </summary>
        public int QuantityExam { get; set; }
        /// <summary>
        /// Valido desde
        /// </summary>
        public DateTime ValidYearFrom { get; set; }
        /// <summary>
        /// Valido até
        /// </summary>
        public DateTime? ValidYearTo { get; set; }
        /// <summary>
        /// Nota média para ser aprovado
        /// </summary>
        public decimal AverageScore { get; set; }
    }
}
