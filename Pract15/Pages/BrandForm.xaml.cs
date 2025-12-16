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
    /// Логика взаимодействия для BrandForm.xaml
    /// </summary>
    public partial class BrandForm : Page
    {
        Brand _brand = new();
        BrandService service = new();
        bool isEdit = false;

        public BrandForm(Brand? brand=null)
        {
            InitializeComponent();
            if(brand != null)
            {
                _brand = brand;
                isEdit = true;
            }
            DataContext = _brand;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                service.Commit();
                MessageBox.Show("Данные бренда обновлены", "Успешно");
            }
            else
            {
                service.Add(_brand);
                MessageBox.Show("Бренд добавлен", "Добавление успешно");
            }
            Back(sender,e);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
