using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsixLibrary
{
    public class Result
    {
        public int Id { get; set; }
        public virtual Prototip Prototip { get; set; }
        public virtual Answer Answer { get; set; }
        public virtual User User { get; set; }
        public Result() { }
    }
}
