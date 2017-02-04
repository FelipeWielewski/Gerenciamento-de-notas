using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoMode.Core.Domain
{
    public class Exam
    {
        public int Id { get; set; }
        /// <summary>
        /// Número da prova no periodo
        /// </summary>
        public int ExamNumber { get; set; }
        /// <summary>
        /// Nota na prova
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// Referente a data de
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// ID do Estudante
        /// </summary>
        public int StudentId { get; set; }
    }
}
