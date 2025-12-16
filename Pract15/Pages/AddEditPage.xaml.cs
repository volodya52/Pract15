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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage :Page
    {
        public Product _products = new();
        private ProductsService service = new( );
        public bool isEdit = false;
        public AddEditPage (Product? _editProduct=null)
        {
            InitializeComponent( );
            if (_editProduct != null)
            {
                _products = _editProduct;
                isEdit = true;
            }
            DataContext = _products;
        }

        private void Back(object sender, EventArgs e)
        {
            NavigationService.GoBack( );
        }

        private void SaveProduct(object sender, EventArgs e)
        {
            if (isEdit)
                service.Commit( );
            else
                service.Add(_products);
            NavigationService.GoBack( );

        }
    }
}
