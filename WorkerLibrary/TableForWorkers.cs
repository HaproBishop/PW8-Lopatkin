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
        private static DataTable res;//Таблица с данными
        private static int j = 0;//Порядковый номер
        private static int i = 0;//Используется для сохранения значения о найденной строке с фамилией
        //Создание шаблона
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
        public static DataTable AddWorker(WorkerSalaryPerHour worker)//Добавление работника
        {
            var row = res.NewRow();
            row[0] = ++j;
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
            row[0] = ++j;
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
            row[0] = ++j;
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
            row[0] = ++j;
            row[1] = secondname;
            row[2] = hours.ToString();
            row[3] = salary.ToString();
            row[4] = "Unknown";
            res.Rows.Add(row);            
            return res;
        }
        public static DataTable DeleteWorker(string secondname)
        {
            DataRow row;
            for (i = 0; i < res.Rows.Count; i++)
            {
                row = res.Rows[i];
                if ((string)row[1] == secondname) res.Rows[i].Delete();//Сравнение фамилии в таблице(приведено к string из DataRow; Данные получены из таблицы
            }
            j--;//Уменьшение порядка из-за удаления строки в таблице
            return res;
        }
        /// <summary>
        /// Используется для обновления информации в таблице (Добавление зарплаты работникам)
        /// </summary>
        /// <param name="secondname"></param>
        /// <param name="worker">Несет в себе зарплату в час</param>
        /// <returns>Возвращение объекта класса DataTable в DataGrid после обращения(Является источником данных)</returns>
        public static DataTable UpdateWorker(string secondname, WorkerSalaryPerHour worker)
        {
            DataRow row;
            for (i = 0; i < res.Rows.Count; i++)
            {
                row = res.Rows[i];
                if ((string)row[1] == secondname)
                {
                    row[4] = worker.SalaryPerHour;
                    object[] news = new object[5];                   
                    news[0] = row[0];
                    news[1] = row[1];
                    news[2] = row[2];
                    news[3] = worker.Salary;
                    news[4] = row[4];
                    res.Rows[i].ItemArray = news;                                     
                }
            }
            return res;
        }
        /// <summary>
        /// Используется для обновления информации в таблице (Добавление зарплаты работникам)
        /// </summary>
        /// <param name="secondname"></param>
        /// <param name="worker">Несет в себе зарплату</param>
        /// <returns>Возвращение объекта класса DataTable в DataGrid после обращения(Является источником данных)</returns>
        public static DataTable UpdateWorker(string secondname, WorkerSalaryScale worker)
        {
            DataRow row;
            for (int i = 0; i < res.Rows.Count; i++)
            {
                row = res.Rows[i];
                if ((string)row[1] == secondname)
                {
                    row[3] = worker.Salary;
                    object[] news = new object[5];
                    news[0] = row[0];
                    news[1] = row[1];
                    news[2] = row[2];
                    news[3] = row[3];
                    news[4] = row[4];
                    res.Rows[i].ItemArray = news;     //Изменяем значения в строке               
                }
            }
            return res;
        }
        public static bool ProveSecondName(string secondname)//Проверка на наличие фамилии в таблице
        {
            DataRow row; 
            for (i = 0; i < res.Rows.Count; i++)
            {
                row = res.Rows[i];
                if ((string)row[1] == secondname) return true;
            }
            return false;            
        }/// <summary>
        /// Получение информации о работнике из таблицы
        /// </summary>
        /// <param name="secondname"></param>
        /// <param name="workerperhour"></param>
        /// <param name="worker"></param>
        public static void GetWorkerInfo(string secondname, ref WorkerSalaryPerHour workerperhour, ref WorkerSalaryScale worker)
        {
            ReturnObject(secondname, ref workerperhour, ref worker);
        }
        public static void GetWorkerInfo(string secondname, ref WorkerSalaryScale worker, ref WorkerSalaryPerHour workerperhour)
        {
            ReturnObject(secondname, ref workerperhour, ref worker);
        }
        private static void ReturnObject(string secondname, ref WorkerSalaryPerHour workerperhour, ref WorkerSalaryScale worker)
        {//Получение данных из таблицы посредством поиска строки с нужной фамилией и присваивание значениий относительно наличия строки "unknown"
            if (ProveSecondName(secondname))
            {
                DataRow row = res.Rows[i];
                if((string)row[1] == secondname) if ((string)row[4] == "Unknown") worker = new WorkerSalaryScale(row[1].ToString(), Convert.ToInt32(row[2]), Convert.ToInt32(row[3]));
                else workerperhour = new WorkerSalaryPerHour(row[1].ToString(), Convert.ToInt32(row[2]), Convert.ToInt32(row[4]));                
            }
        }        
    }
}
