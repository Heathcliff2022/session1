using FSharp.Data.Runtime.StructuralTypes;
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

namespace Черновик
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ЧерновикEntities Whitelist;

        private int _currentPage = 1;
        private int _countAgents = 15;
        private int _maxPages;

        public MainWindow()
        {
            InitializeComponent();
            Whitelist = new ЧерновикEntities();
            SpisokMaterial.ItemsSource = MainWindow.Whitelist.Тип_материала.ToList();
            SpisokMaterial.ItemsSource = MainWindow.Whitelist.Материал.ToList();
            //UpdateAgents();
            Refresh();
        }

        List<Материал> listAgent;
        private void Refresh()
        {
            listAgent = MainWindow.Whitelist.Материал.ToList();
            _maxPages = (int)Math.Ceiling(listAgent.Count * 1.0 / _countAgents);
            var listAgentPage = listAgent.Skip((_currentPage - 1) * _countAgents).Take(_countAgents).ToList();
            TxtCurrentPage.Text = _currentPage.ToString();
            LblTotalPages.Content = "из " + _maxPages;
            SpisokMaterial.ItemsSource = listAgentPage;
        }

        private void GoToPreviousPage(object sender, RoutedEventArgs e)
        {
            if (_currentPage <= 1) _currentPage = 1;
            else _currentPage--;
            Refresh();
        }

        private void GoToNextPage(object sender, RoutedEventArgs e)
        {
            if (_currentPage >= _maxPages) _currentPage = _maxPages;
            else _currentPage++;
            Refresh();
        }

        private void btBack(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Sort.ItemsSource = new List<string> { "возрастание", "убывание" };
            Filtr.ItemsSource = new List<string> { "0% - 5%", "5% - 15%", "15% - 30%", "30% - 70%", "70% - 100%", "Все" };
        }

        private void Filtr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var x = MainWindow.Whitelist.Материал.ToList();
            //switch (Sort.SelectedIndex)
            //{
            //    case 0:
            //        x = x.OrderBy(p => p.Discount).ToList();
            //        SpisokMaterial.ItemsSource = x;
            //        break;
            //    case 1:
            //        x = x.OrderByDescending(p => p.Cost).ToList();
            //        SpisokMaterial.ItemsSource = x;
            //        break;
            //    case 2:
            //        x = x.OrderBy(p => p.Discount).ToList();
            //        SpisokMaterial.ItemsSource = x;
            //        break;
            //    case 3:
            //        x = x.OrderBy(p => p.Discount).ToList();
            //        SpisokMaterial.ItemsSource = x;
            //        break;
            //    case 4:
            //        x = x.OrderBy(p => p.Discount).ToList();
            //        SpisokMaterial.ItemsSource = x;
            //        break;
            //    case 5:
            //        x = MainWindow.Whitelist.Материал.ToList();
            //        SpisokMaterial.ItemsSource = x;
            //        break;
            //}
        }

        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var x = MainWindow.Whitelist.Материал.ToList();
            switch (Sort.SelectedIndex)
            {
                case 0:
                    x = x.OrderBy(p => p.Цена).ToList();
                    SpisokMaterial.ItemsSource = x;
                    break;
                case 1:
                    x = x.OrderByDescending(p => p.Цена).ToList();
                    SpisokMaterial.ItemsSource = x;
                    break;
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var x = MainWindow.Whitelist.Материал.ToList();
            var y = MainWindow.Whitelist.Материал.ToList();
            string seachText = tbSearch.Text;
            if (!string.IsNullOrWhiteSpace(seachText))
            {
                x = x.Where(p => p.Наименование.ToLower().StartsWith(seachText.ToLower())).ToList();
                //y = y.Where(p => p.Description.ToLower().StartsWith(seachText.ToLower())).ToList();
                SpisokMaterial.ItemsSource = x;
            }
            else SpisokMaterial.ItemsSource = MainWindow.Whitelist.Материал.ToList();
        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            var editButton = sender as Button; //Какая кнопка нажата(выбрана)
            var nService = editButton.DataContext as Материал;
            //Service nService = new Service();
            //EditYslug win = new EditYslug(nService);
            //win.ShowDialog();

        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            var obj = SpisokMaterial.SelectedItems.Cast<Материал>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить запись?", "Внимание",
            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    MainWindow.Whitelist.Материал.RemoveRange(obj);
                    MainWindow.Whitelist.SaveChanges();
                    MessageBox.Show("Готово");
                    SpisokMaterial.ItemsSource = MainWindow.Whitelist.Материал.ToList();
                }
                catch
                {
                    MessageBox.Show("Ошибка, попробуйте еще раз");
                }

            }
        }

        private void Bt1_Click(object sender, RoutedEventArgs e)
        {
            var editButton = sender as Button; //Какая кнопка нажата(выбрана)
            var nService = editButton.DataContext as Материал;
            //Service nService = new Service();
            //EditYslug win = new EditYslug(nService);
            //win.ShowDialog();
        }
    }
}

