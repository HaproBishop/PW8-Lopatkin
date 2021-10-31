using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkerLibrary;

namespace PrototypePW8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Table.ItemsSource = TableForWorkers.CreateTableForWorker().DefaultView; //Добавление шаблона таблицы
        }
        WorkerSalaryScale workersalaryscale = new WorkerSalaryScale();
        WorkerSalaryPerHour workersalaryperhour = new WorkerSalaryPerHour();
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AboutProgram_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Практическая работа №8. Задание 8. Создать интерфейс – работник. Создать классы - служащий с почасовой оплатой, служащий с окладом.Классы должны включать конструкторы, функцию для формирования строки информации о работнике.Определить функцию начисления зарплаты.Сравнение производить по фамилии.", "About Program", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void AddWorker_Click(object sender, RoutedEventArgs e)
        {
            bool ProveHours = int.TryParse(Hours.Text, out int hours);
            if (ProveHours)//Использована упрощенная запись для проверки значения bool(true - двигаемся дальше по коду)
            {
                if (TableForWorkers.ProveSecondName(SecondName.Text)) MessageBox.Show("Worker is had into table", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    if (CheckSalary.IsChecked == true)
                    {
                        workersalaryperhour.AddWorkerInformation(SecondName.Text, hours);//Использование метода для заполнения полей значениями
                        Table.ItemsSource = TableForWorkers.AddWorker(workersalaryperhour).DefaultView;//Занесение информации о работнике в таблицу
                    }
                    else
                    {
                        workersalaryscale.AddWorkerInformation(SecondName.Text, hours);
                        Table.ItemsSource = TableForWorkers.AddWorker(workersalaryscale).DefaultView;
                    }
                    workersalaryscale = new WorkerSalaryScale();
                }
            }
            else MessageForUser();
        }

        private void CompareSalaries_Click(object sender, RoutedEventArgs e)
        {
            if ((SecondName.Text.ToString() != OtherSecondName.Text.ToString()) && TableForWorkers.ProveSecondName(SecondName.Text) && TableForWorkers.ProveSecondName(OtherSecondName.Text))
            {//Проверка на равенство друх фамилий, а также проверка на наличие таких фамилий
                int result;
                string answer;
                WorkerSalaryScale otherworkersalaryscale = new WorkerSalaryScale();
                WorkerSalaryPerHour otherworkersalaryperhour = new WorkerSalaryPerHour(); ;
                TableForWorkers.GetWorkerInfo(SecondName.Text, ref workersalaryperhour, ref workersalaryscale);//Получение информации из таблицы в объект
                TableForWorkers.GetWorkerInfo(OtherSecondName.Text, ref otherworkersalaryperhour, ref otherworkersalaryscale);//Для второго объекта на сравнение
                if (SecondName.Text == workersalaryperhour.SecondName)//Сопоставление текущего имени с добавленным в объект
                    if (OtherSecondName.Text == otherworkersalaryperhour.SecondName)
                    {
                        result = workersalaryperhour.CompareTo(otherworkersalaryperhour);//Занесение результата сравнения
                        answer = CompareResults(result, SecondName.Text, OtherSecondName.Text, workersalaryperhour.Salary, otherworkersalaryperhour.Salary);
                    }//Внесение данных результата, сделанных в другом методе, в answer
                    else
                    {
                        result = workersalaryperhour.CompareTo(otherworkersalaryscale);
                        answer = CompareResults(result, SecondName.Text, OtherSecondName.Text, workersalaryperhour.Salary, otherworkersalaryscale.Salary);
                    }
                else if (OtherSecondName.Text == otherworkersalaryperhour.SecondName)
                {
                    result = workersalaryscale.CompareTo(otherworkersalaryperhour);
                    answer = CompareResults(result, SecondName.Text, OtherSecondName.Text, workersalaryscale.Salary, otherworkersalaryperhour.Salary);
                }
                else
                {
                    result = workersalaryscale.CompareTo(otherworkersalaryscale);
                    answer = CompareResults(result, SecondName.Text, OtherSecondName.Text, workersalaryscale.Salary, otherworkersalaryscale.Salary);
                }
                WorkerWithMoreSalary.Text = answer;//Вывод результата сравнения в поле для уведомления пользователя о результатах
            }
            else MessageForUserAboutSecondNames();
        }

        private void CloneWorkerInfo_Click(object sender, RoutedEventArgs e)
        {
            if ((SecondName.Text != "") && (OtherSecondName.Text != "") && (TableForWorkers.ProveSecondName(OtherSecondName.Text) == false))
            {//Проверка на наличие пустых полей и другого имени в таблице
                TableForWorkers.GetWorkerInfo(SecondName.Text, ref workersalaryperhour, ref workersalaryscale);
                WorkerSalaryScale cloneworkersalaryscale;
                WorkerSalaryPerHour cloneworkersalaryperhour;
                if (workersalaryscale.SecondName == SecondName.Text)//Сравнение для правильного приведения
                {
                    cloneworkersalaryscale = (WorkerSalaryScale)workersalaryscale.Clone();//Выполнение клонирования объекта и конвертация в класс
                    cloneworkersalaryscale.SecondName = OtherSecondName.Text;//Присваивание нового имени
                    Table.ItemsSource = TableForWorkers.AddWorker(cloneworkersalaryscale).DefaultView;
                }
                else
                {
                    cloneworkersalaryperhour = (WorkerSalaryPerHour)workersalaryperhour.Clone();
                    cloneworkersalaryperhour.SecondName = OtherSecondName.Text;
                    Table.ItemsSource = TableForWorkers.AddWorker(cloneworkersalaryperhour).DefaultView;//Внесение объекта в таблицу(его данных)
                }
            }
            else MessageForUser();
        }
        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            bool ProveAllSalary = int.TryParse(AllSalary.Text, out int value);
            if (ProveAllSalary)
            {
                if (TableForWorkers.ProveSecondName(SecondName.Text))
                {
                    TableForWorkers.GetWorkerInfo(SecondName.Text, ref workersalaryperhour, ref workersalaryscale);
                    if (workersalaryscale.SecondName == SecondName.Text)//Проверка для правильного ориентира класса
                    {
                        workersalaryscale.PaySalary(value);
                        Table.ItemsSource = TableForWorkers.UpdateWorker(SecondName.Text, workersalaryscale).DefaultView;
                    }
                    else
                    {
                        workersalaryperhour.PaySalary(value);
                        Table.ItemsSource = TableForWorkers.UpdateWorker(SecondName.Text, workersalaryperhour).DefaultView;
                    }
                }
                else MessageForUser();
            }
            else MessageForUser();
        }
        /// <summary>
        /// Используется для сборки результата в сообщение в виде строки пользователю
        /// </summary>
        /// <param name="result">Результат(-1, 0, 1)</param>
        /// <param name="secondname">Фамилия работника</param>
        /// <param name="othersecondname">Фамилия другого работника</param>
        /// <param name="salary">Зарплата первого работника</param>
        /// <param name="othersalary">Зарплата второго работника</param>
        /// <returns>Строка с результатом</returns>
        private string CompareResults(int result, string secondname, string othersecondname, int salary, int othersalary)
        {
            if (result == 1) return $"{secondname} have more salary - {salary}";
            if (result == -1) return $"{othersecondname} have more salary - {othersalary}";
            return $"Salaries of {secondname} and {othersecondname} are equal - {salary}";
        }

        private void SecondName_TextChanged(object sender, TextChangedEventArgs e)
        {//Для очистки значений при изменении начальных значений
            Hours.Clear();
            WorkerWithMoreSalary.Clear();
        }

        private void OtherSecondName_TextChanged(object sender, TextChangedEventArgs e)
        {
            WorkerWithMoreSalary.Clear();
        }
        private void MessageForUser()//Используется для упрощенного написания кода
        {
            MessageBox.Show("Your values are not correct! Please, enter supported values! Read \"Support\" for more details!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void SecondName_GotFocus(object sender, RoutedEventArgs e)//Изменение кнопки для дефолта Enter
        {
            Pay.IsDefault = false;
            AddWorker.IsDefault = true;
        }

        private void AllSalary_GotFocus(object sender, RoutedEventArgs e)
        {
            Pay.IsDefault = true;
            AddWorker.IsDefault = false;
        }

        private void Support_Click(object sender, RoutedEventArgs e)//Расписывание об особенностях программы
        {
            MessageBox.Show("1) You can enter Second Name with length - 20\n2) You can enter Salary(SalaryPerHour) with length - 8, hours - 4\n3) " +
                "You must enter new second name into string \"Other Second Name\" for clone data of other person\n4) " +
                "If you want add person with salary per hour then you can stay \"check\" into checkbox\n5) " +
                "All operations linked into tab \"Worker Control\"","Support", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DeleteWorker_Click(object sender, RoutedEventArgs e)
        {
            if ((SecondName.Text != "") && TableForWorkers.ProveSecondName(SecondName.Text))//Проверка на пустоту и наличие фамилии
            {
                TableForWorkers.DeleteWorker(SecondName.Text);//Удаление из таблицы значения
            }
            else MessageForUserAboutSecondNames();
        }
        private void MessageForUserAboutSecondNames()
        {
            MessageBox.Show("Second Names can not be equal!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
