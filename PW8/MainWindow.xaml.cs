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
            Table.ItemsSource = TableForWorkers.CreateTableForWorker().DefaultView;
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
            if (ProveHours)
            {
                if (TableForWorkers.ProveSecondName(SecondName.Text)) MessageBox.Show("Worker is had into table", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    if (CheckSalary.IsChecked == true)
                    {
                        workersalaryperhour.AddWorkerInformation(SecondName.Text, hours);
                        Table.ItemsSource = TableForWorkers.AddWorker(workersalaryperhour).DefaultView;
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
            {
                int result;
                string answer;
                WorkerSalaryScale otherworkersalaryscale = new WorkerSalaryScale();
                WorkerSalaryPerHour otherworkersalaryperhour = new WorkerSalaryPerHour(); ;
                TableForWorkers.GiveWorkerInfo(SecondName.Text, ref workersalaryperhour, ref workersalaryscale);
                TableForWorkers.GiveWorkerInfo(OtherSecondName.Text, ref otherworkersalaryperhour, ref otherworkersalaryscale);
                if (SecondName.Text == workersalaryperhour.SecondName)
                    if (OtherSecondName.Text == otherworkersalaryperhour.SecondName)
                    {
                        result = workersalaryperhour.CompareTo(otherworkersalaryperhour);
                        answer = CompareResults(result, SecondName.Text, OtherSecondName.Text, workersalaryperhour.Salary, otherworkersalaryperhour.Salary);
                    }
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
                WorkerWithMoreSalary.Text = answer;
            }
            else MessageForUserAboutSecondNames();
        }

        private void CloneWorkerInfo_Click(object sender, RoutedEventArgs e)
        {
            if ((SecondName.Text != "") && (OtherSecondName.Text != "") && (TableForWorkers.ProveSecondName(OtherSecondName.Text) == false))
            {
                TableForWorkers.GiveWorkerInfo(SecondName.Text, ref workersalaryperhour, ref workersalaryscale);
                WorkerSalaryScale cloneworkersalaryscale;
                WorkerSalaryPerHour cloneworkersalaryperhour;
                if (workersalaryscale.SecondName == SecondName.Text)
                {
                    cloneworkersalaryscale = (WorkerSalaryScale)workersalaryscale.Clone();
                    cloneworkersalaryscale.SecondName = OtherSecondName.Text;
                    Table.ItemsSource = TableForWorkers.AddWorker(cloneworkersalaryscale).DefaultView;
                }
                else
                {
                    cloneworkersalaryperhour = (WorkerSalaryPerHour)workersalaryperhour.Clone();
                    cloneworkersalaryperhour.SecondName = OtherSecondName.Text;
                    Table.ItemsSource = TableForWorkers.AddWorker(cloneworkersalaryperhour).DefaultView;
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
                    TableForWorkers.GiveWorkerInfo(SecondName.Text, ref workersalaryperhour, ref workersalaryscale);
                    if (workersalaryscale.SecondName == SecondName.Text)
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
        private string CompareResults(int result, string secondname, string othersecondname, int salary, int othersalary)
        {
            if (result == 1) return $"{secondname} have more salary - {salary}";
            if (result == -1) return $"{othersecondname} have more salary - {othersalary}";
            return $"Salaries of {secondname} and {othersecondname} are equal - {salary}";
        }

        private void SecondName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Hours.Clear();
            WorkerWithMoreSalary.Clear();
        }

        private void OtherSecondName_TextChanged(object sender, TextChangedEventArgs e)
        {
            WorkerWithMoreSalary.Clear();
        }
        private void MessageForUser()
        {
            MessageBox.Show("Your values are not correct! Please, enter supported values! Read \"Support\" for more details!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void SecondName_GotFocus(object sender, RoutedEventArgs e)
        {
            Pay.IsDefault = false;
            AddWorker.IsDefault = true;
        }

        private void AllSalary_GotFocus(object sender, RoutedEventArgs e)
        {
            Pay.IsDefault = true;
            AddWorker.IsDefault = false;
        }

        private void Support_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteWorker_Click(object sender, RoutedEventArgs e)
        {
            if ((SecondName.Text != "") && TableForWorkers.ProveSecondName(SecondName.Text))
            {
                TableForWorkers.DeleteWorker(SecondName.Text);
            }
            else MessageForUserAboutSecondNames();
        }
        private void MessageForUserAboutSecondNames()
        {
            MessageBox.Show("Second Names can not be equal!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
