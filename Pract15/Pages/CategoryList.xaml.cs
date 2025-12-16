using Pract15.Models;
using Pract15.Service;
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

namespace Pract15.Pages
{
    /// <summary>
    /// Логика взаимодействия для CategoryList.xaml
    /// </summary>
    public partial class CategoryList : Page
    {
        public Category current { get; set; } = null;
        public CategoryService service { get; set; } = new();

        public CategoryList()
        {
            InitializeComponent();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CategoryPage());
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CategoryPage(current));
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (current == null)
            {
                MessageBox.Show("Выберите категорию для удаления", "Ошибка");
            }
            else
            {
                if(MessageBox.Show("Вы точно хотите удалить категорию?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    service.Remove(current);
                    MessageBox.Show("Категория удалена", "Успешно");
                }
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
