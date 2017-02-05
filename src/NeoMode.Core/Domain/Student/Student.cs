using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoMode.Core.Domain
{
    public class Student
    {
        public int Id { get; set; }
        /// <summary>
        /// Nome completo
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Número da matricula
        /// </summary>
        public string RegistryCode { get; set; }
        /// <summary>
        /// URL da imagem do perfil
        /// </summary>
        public string ProfileImage { get; set; }
        /// <summary>
        /// E-mail de contato
        /// </summary>
        public string Email { get; set; }
        public int? CityId { get; set; }
        public virtual City City { get; set; }
    }
}
