using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerLibrary
{
    interface IWorker
    {
        string SecondName { get; set; }
        int Hours { get; set; }
        int Salary { get; }
        void PaySalary();
    }
}
