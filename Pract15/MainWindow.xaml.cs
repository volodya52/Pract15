using Pract15.Models;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pract15
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Product> products { get; set; } = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void LoadList(object sender, EventArgs e)
        {
            products.Clear();
            foreach (var product in products)
            {
               products.Add(product);
            }
        }
    }
}