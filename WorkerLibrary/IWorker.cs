using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerLibrary
{
    interface IWorker
    {
        int Hour { get; }
        int Salary { get; }
        string SecondName { get; }
        
    }
}
