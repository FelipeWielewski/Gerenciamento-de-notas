using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoMode.Core.Domain.School
{
    public class School
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ControlKey { get; set; }
        public string SecondaryKey { get; set; }
    }
}
