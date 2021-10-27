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
            if (Table.Items.IndexOf(SecondName.Text) != -1) MessageBox.Show("Worker is had into table", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                bool ProveHours = int.TryParse(Hours.Text, out int hours);
                bool ProveAllSalary = int.TryParse(AllSalary.Text, out int allsalary);
                if (ProveHours == true && ProveAllSalary == true)
                {
                    if (CheckSalary.IsChecked == true)
                    {
                        workersalaryperhour.AddWorkerInformation(SecondName.Text, hours, allsalary);
                    }
                    else workersalaryscale.AddWorkerInformation(SecondName.Text, hours, allsalary);
                }
                else MessageBox.Show("You entered uncorrectly values! Try again, please!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
