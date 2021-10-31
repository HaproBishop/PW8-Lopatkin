using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerLibrary
{/// <summary>
/// Класс для создания объекта и информацией работника (Фамилия, Часы, Зарплата)
/// </summary>
    public class WorkerSalaryScale : IWorker, IComparable, ICloneable
    {
        int _hours, _salary;
        public string SecondName { get; set; }
        public int Hours { get => _hours; set => _hours = ProveValue(value) ? value : throw new Exception("Ошибка! Введено некорректно значение!"); }
        public virtual int Salary { get => _salary; set => _salary = ProveValue(value) ? value : throw new Exception("Ошибка! Введено некорректно значение!"); }
        public WorkerSalaryScale() { }
        public WorkerSalaryScale(string secondname, int hours, int salary)
        {
            SecondName = secondname;
            Hours = hours;
            Salary = salary;
        }
        private protected bool ProveValue(int value)
        {
            return value >= 0;
        }
        public void AddWorkerInformation(string secondname, int hours)
        {
            SecondName = secondname;
            Hours = hours;
        }
        public int CompareTo(object obj)
        {
            var twoworker = (WorkerSalaryScale)obj;
            if (Salary > twoworker.Salary) return 1;
            if (Salary < twoworker.Salary) return -1;
            return 0;
        }
        public virtual object Clone()
        {
            return new WorkerSalaryScale(SecondName, Hours, Salary);
        }
        public virtual void PaySalary(int salary)
        {
            Salary = salary;
        }
    }
}
