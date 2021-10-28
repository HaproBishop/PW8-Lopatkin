using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace WorkerLibrary
{
    //Класс для связывания массива с элементом DataGrid
    //для визуализации данных 
    public static class TableForWorkers
    {
        private static DataTable res;
        private static int i = 0;
        //Метод для одномерного массива
        public static DataTable CreateTableForWorker()
        {            
            res = new DataTable();
            res.Columns.Add("№", typeof(string));
            res.Columns.Add("Фамилия", typeof(string));
            res.Columns.Add("Часы", typeof(string));
            res.Columns.Add("Зарплата", typeof(string));
            res.Columns.Add("Зарплата за час", typeof(string));           
            return res;
        }
        public static DataTable AddWorker(WorkerSalaryPerHour worker)
        {
            var row = res.NewRow();
            row[0] = ++i;
            row[1] = worker.SecondName;
            row[2] = worker.Hours.ToString();
            row[3] = worker.Salary.ToString();
            row[4] = worker.SalaryPerHour.ToString();
            res.Rows.Add(row);
            return res;
        }
        public static DataTable AddWorker(WorkerSalaryScale worker)
        {            
            var row = res.NewRow();
            row[0] = ++i;
            row[1] = worker.SecondName;
            row[2] = worker.Hours.ToString();
            row[3] = worker.Salary.ToString();
            row[4] = "Отсутствует";
            res.Rows.Add(row);
            return res;
        }
        public static DataTable AddWorker(string secondname, int hours, int salary, int salaryperhour)
        {            
            var row = res.NewRow();
            row[0] = ++i;
            row[1] = secondname;
            row[2] = hours.ToString();
            row[3] = salary.ToString();
            row[4] = salaryperhour.ToString();
            res.Rows.Add(row);
            return res;
        }
        public static DataTable AddWorker(string secondname, int hours, int salary)
        {
            var row = res.NewRow();
            row[0] = ++i;
            row[1] = secondname;
            row[2] = hours.ToString();
            row[3] = salary.ToString();
            row[4] = "Отсутствует";
            res.Rows.Add(row);            
            return res;
        }
        public static bool ProveSecondName(string secondname)
        {
            DataRow row; 
            for (int i = 0; i < res.Rows.Count; i++)
            {
                row = res.Rows[i];
                if ((string)row[1] == secondname) return true;
            }
            return false;            
        }
    }
}
