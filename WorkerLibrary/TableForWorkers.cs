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
            res.Columns.Add("Second Name", typeof(string));
            res.Columns.Add("Hours", typeof(string));
            res.Columns.Add("Salary", typeof(string));
            res.Columns.Add("Salary Per Hour", typeof(string));           
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
            row[4] = "Unknown";
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
            row[4] = "Unknown";
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
        public static object CloneWorkerInfo(string secondname)
        {
            DataRow row;
            for (int i = 0; i < res.Rows.Count; i++)
            {
                row = res.Rows[i];
                if ((string)row[1] == secondname) return GiveWorkerInfo(row);
            }
            return null;
        }
        private static object GiveWorkerInfo(DataRow row)
        {
            WorkerSalaryPerHour workerperhour;
            WorkerSalaryScale worker;
            bool TryFindSalaryPerHour = int.TryParse((string)row[4], out int sph);
            if (TryFindSalaryPerHour)
            {
                workerperhour = new WorkerSalaryPerHour((string)row[1], (int)row[2], (int)row[4]);
                return workerperhour;
            }
            else
            {
                worker = new WorkerSalaryScale((string)row[1], (int)row[2], (int)row[3]);
                return worker;
            }            
        }
        public static void FindWorker(ref WorkerSalaryPerHour worker)
        {
            
        }
        public static void FindWorker(ref WorkerSalaryScale worker)
        { 

        }
    }
}
