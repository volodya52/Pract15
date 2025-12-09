using Pract15.Models;
using Pract15.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        public string searchQuery { get; set; } = null!;
        public Pract15DatabaseContext db=DbService.Instance.Context;
        public ObservableCollection<Product> products { get; set; } = new();
        
        public Products(bool isManager)
        {
            InitializeComponent();
            if (isManager == true)
            {
                MessageBox.Show("Вы зашли как менеджер");
            }
        }

        public void LoadList(object sender, EventArgs e)
        {
            products.Clear();
            foreach(var product in products)
            {
                products.Add(product);
            }
        }
    }
}
