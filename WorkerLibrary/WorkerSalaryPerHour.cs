using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerLibrary
{/// <summary>
/// Класс для создания объекта и информацией работника (Фамилия, Часы, Зарплата(расчет по часам), Зарплата в час)
/// </summary>
    public class WorkerSalaryPerHour : WorkerSalaryScale
    {        
        private int _salaryperhour;
        public override int Salary { get => SalaryPerHour * Hours;}
        public int SalaryPerHour { get => _salaryperhour; set => _salaryperhour = ProveValue(value) ? value : throw new Exception("Ошибка! Введено некорректно значение!"); }
        public WorkerSalaryPerHour() : base() { }
        public WorkerSalaryPerHour(string secondname, int hours, int salaryperhour) : base(secondname, hours, salaryperhour)
        {
            SalaryPerHour = salaryperhour;
        }        

        public override object Clone()
        {
            return new WorkerSalaryPerHour(SecondName, Hours, SalaryPerHour);
        }
        public override void PaySalary(int salaryperhour)
        {
            SalaryPerHour = salaryperhour;
        }
    }
}
