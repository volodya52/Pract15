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
    /// Логика взаимодействия для CategoryPage.xaml
    /// </summary>
    public partial class CategoryPage : Page
    {
        public Category _category { get; set; } = new();
        public CategoryService service { get; set; } = new();
        bool isEdit = false;
        public CategoryPage(Category? category=null)
        {
            InitializeComponent();
            if(category != null)
            {
                _category = category;
                isEdit = true;
            }
            DataContext = _category;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                service.Commit();
                MessageBox.Show("Информация о категории изменена", "Успешно");
            }
            else
            {
                service.Add(_category);
                MessageBox.Show("Новая категория добавлена", "Успешно");
            }
            Back(sender,e);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
