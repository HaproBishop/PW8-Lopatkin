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
        public int Salary { get => _salary; set => _salary = ProveValue(value) ? value : throw new Exception("Ошибка! Введено некорректно значение!"); }
        public WorkerSalaryScale() { }
        public WorkerSalaryScale(string secondname, int hours, int salary)
        {
            SecondName = secondname;
            Hours = hours;
            Salary = salary;
        }

        private bool ProveValue(int value)
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
            WorkerSalaryScale worker = (WorkerSalaryScale)obj;
            return Salary - worker.Salary;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
        public void PaySalary(int salary)
        {
            Salary = salary;
        }
    }
}
