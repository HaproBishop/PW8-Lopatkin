using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerLibrary
{
    public class WorkerSalaryPerHour : IWorker, IComparable, ICloneable
    {
        int _hours, _salaryperhour;
        public string SecondName { get; set; }
        public int Hours { get => _hours; set => _hours = ProveValue(value) ? value : throw new Exception("Ошибка! Введено некорректно значение!"); }
        public int Salary { get => SalaryPerHour * Hours; }
        public int SalaryPerHour { get => _salaryperhour; set => _salaryperhour = ProveValue(value) ? value : throw new Exception("Ошибка! Введено некорректно значение!"); }
        public WorkerSalaryPerHour() { }
        public WorkerSalaryPerHour(string secondname, int hours, int salaryperhour)
        {
            SecondName = secondname;
            Hours = hours;
            SalaryPerHour = salaryperhour;
        }
        public void AddWorkerInformation(string secondname, int hours)
        {
            SecondName = secondname;
            Hours = hours;
        }
        private bool ProveValue(int value)
        {
            return value >= 0;
        }
        public int CompareTo(object obj)
        {
            WorkerSalaryPerHour worker = (WorkerSalaryPerHour)obj;
            return Salary - worker.Salary;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public void PaySalary(int salaryperhour)
        {
            SalaryPerHour = salaryperhour;
        }
    }
}
