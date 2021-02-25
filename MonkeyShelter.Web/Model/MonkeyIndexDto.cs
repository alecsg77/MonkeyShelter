using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonkeyShelter.Web.Model
{
    public class MonkeyIndexDto
    {
        public int Total { get; set; }
        public IEnumerable<MonkeyDto> Monkeys { get; set; }
    }
}
