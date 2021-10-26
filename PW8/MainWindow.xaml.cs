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
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AboutProgram_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Практическая работа №8. Задание 8. Создать интерфейс – работник. Создать классы - служащий с почасовой оплатой, служащий с окладом.Классы должны включать конструкторы, функцию для формирования строки информации о работнике.Определить функцию начисления зарплаты.Сравнение производить по фамилии.", "About Program", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
