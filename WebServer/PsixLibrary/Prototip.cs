using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsixLibrary;

public class Prototip
{
    public int Id { get; set; }
    public virtual Test Test { get; set; }
    public virtual Question Question { get; set; }
    public virtual Answer Answer { get; set; }
    public Prototip() { }
}
