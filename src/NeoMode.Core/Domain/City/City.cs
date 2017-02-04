using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoMode.Core.Domain
{
    public class City
    {
        public int Id { get; set; }
        /// <summary>
        /// Sigla da cidade
        /// </summary>
        public string Initials { get; set; }
        /// <summary>
        /// Nome da cidade
        /// </summary>
        public string Description { get; set; }
    }
}
