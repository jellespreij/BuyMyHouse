using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Timer
    {
        public Schedule Schedule { get; set; }
        public bool IsPastDue { get; set; }
    }
}
