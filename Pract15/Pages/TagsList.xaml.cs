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
    /// Логика взаимодействия для TagsList.xaml
    /// </summary>
    public partial class TagsList : Page
    {
        public Tag? current { get; set; } = null;
        public TagService service { get; set; } = new();
        public TagsList()
        {
            InitializeComponent();
        }

        public void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public void Add(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TagsPage(current));
        }

        public void Edit(object sender, RoutedEventArgs e)
        {
            if (current == null)
            {
                MessageBox.Show("Выберите тэг для редактирования");
            }
            else
            {
                NavigationService.Navigate(new TagsPage(current));
            }
        }

        public void Delete(object sender, RoutedEventArgs e)
        {
            if (current != null)
            {
                if(MessageBox.Show("Хотите удалить тэг?", "Удаление", MessageBoxButton.YesNo)==MessageBoxResult.Yes)
                {
                    service.Remove(current);
                    MessageBox.Show("Тэг успешно удален", "Успешно");
                }
            }
            else
            {
                MessageBox.Show("Выберите тэг для удаления", "Удаление");
            }
        }
    }
}
