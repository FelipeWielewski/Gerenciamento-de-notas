using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoMode.Core.Model
{
    public class ApplicationUser
    {
        /// <summary>
        /// Informação primaria
        /// </summary>
        public string InfoPrimary { get; set; }
        /// <summary>
        /// Informação secundaria
        /// </summary>
        public string InfoSecundary { get; set; }
        /// <summary>
        /// 0 - UserSystem | 1 - SchoolSystem
        /// </summary>
        public int Type { get; set; }
    }
}
