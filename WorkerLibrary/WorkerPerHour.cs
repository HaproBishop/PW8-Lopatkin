using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerLibrary
{
    public class WorkerPerHour : IWorker
    {
        int _hours, _salary;
        string _secondname;
        public int Hour { get => _hours; }
        public int Salary { get => _salary; }
        public string SecondName { get => _secondname; }
    }
}
