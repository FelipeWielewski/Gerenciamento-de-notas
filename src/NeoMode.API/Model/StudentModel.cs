using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoMode.API.Model
{
    public class StudentModel
    {
        /// <summary>
        /// Nome completo
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Número da matricula
        /// </summary>
        public string RegistryCode { get; set; }
    }
}
