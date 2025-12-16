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
    /// Логика взаимодействия для TagsPage.xaml
    /// </summary>
    public partial class TagsPage : Page
    {
        Tag _tag = new();
        TagService service = new();
        bool isEdit = false;

        public TagsPage(Tag? tag=null)
        {
            InitializeComponent();
            if (tag != null)
            {
                _tag = tag;
                isEdit = true;
            }
            DataContext = _tag;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (isEdit)
                service.Commit();
            else
                service.Add(_tag);
            Back(sender, e);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
