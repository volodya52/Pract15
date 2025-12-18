using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pract15.Models;
using Pract15.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private string _searchQuery;
        public string searchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
                productsView?.Refresh();
            }
        }

        public Pract15DatabaseContext db = DbService.Instance.Context;
        public DbService service { get; set; } = null;

        private Product _product;
        public Product product
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged();
            }
        }

        public ProductsService pService { get; set; } = new();

        private ObservableCollection<Product> _products = new();
        public ObservableCollection<Product> products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        private Brand _selectedBrand;
        public Brand SelectedBrand
        {
            get => _selectedBrand;
            set
            {
                _selectedBrand = value;
                OnPropertyChanged();

                // Обновляем фильтр
                if (productsView != null)
                {
                    productsView.Refresh();
                }
            }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory= value;
                OnPropertyChanged();
                if(productsView != null)
                {
                    productsView.Refresh();
                }
            }
        }

        public BrandService brandService { get; set; } = new();
        public ICollectionView productsView { get; set; }

        private string _filterFrom;
        public string filterFrom
        {
            get => _filterFrom;
            set
            {
                _filterFrom = value;
                OnPropertyChanged();
                productsView?.Refresh();
            }
        }

        private string _filterTo;
        public string filterTo
        {
            get => _filterTo;
            set
            {
                _filterTo = value;
                OnPropertyChanged();
                productsView?.Refresh();
            }
        }






        public Products(bool? isManager)
        {
            InitializeComponent();

            if (isManager == true)
            {
                MessageBox.Show("Вы зашли как менеджер");
                DeleteButton.Visibility = Visibility.Visible;
                TagsButton.Visibility = Visibility.Visible;
                BrandsButton.Visibility = Visibility.Visible;
                AddButton.Visibility = Visibility.Visible;
                CategoryButton.Visibility = Visibility.Visible;
                
            }
            DataContext = this;

            productsView = CollectionViewSource.GetDefaultView(products);
            productsView.Filter = FilterProducts;
        }

        

        public void Page_Loaded (object sender, RoutedEventArgs e)
        {
            using var context = new Pract15DatabaseContext();
            var loadedProducts = context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Tags)
                .ToList();

            products.Clear();
            foreach (var product in loadedProducts)
            {
                products.Add(product);
            }
            if (product == null)
            {
                product = new Product();
            }
        }

        private void DeleteButton_Click (object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "Удалить", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                if (ProductsList.SelectedItem is Product selectedProduct)
                {
                    
                    using (var context = new Pract15DatabaseContext())
                    {
                        context.Products.Remove(selectedProduct);
                        context.SaveChanges();

                        
                        products.Remove(selectedProduct);
                        MessageBox.Show("Запись удалена");
                    }
                }
            }
        }

        private void ComboBox_SelectionChanged (object sender, SelectionChangedEventArgs e)
        {
            productsView.SortDescriptions.Clear( );
            var cb = (ComboBox) sender;
            var selected = (ComboBoxItem) cb.SelectedItem;
            switch (selected.Tag)
            {
                case "Наименование":
                    productsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                    break;
                case "ЦенаМинус":
                    productsView.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Descending));
                    break;
                case "ЦенаПлюс":
                    productsView.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Ascending));
                    break;
                case "КоличествоМинус":
                    productsView.SortDescriptions.Add(new SortDescription("Stock", ListSortDirection.Descending));
                    break;
                case "КоличествоПлюс":
                    productsView.SortDescriptions.Add(new SortDescription("Stock", ListSortDirection.Ascending));
                    break;
            }
        }

        public bool FilterProducts(object obj)
        {
            if (obj is not Product)
                return false;
            var product = (Product) obj;
            if (searchQuery != null && !product.Name.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase))
                return false;
            if (!filterFrom.IsNullOrEmpty() && Convert.ToInt32(filterFrom) > product.Price)
                return false;
            if (!filterTo.IsNullOrEmpty() && Convert.ToInt32(filterTo) < product.Price)
                return false;
            if (SelectedBrand != null && SelectedBrand.Id != 0 &&
            (product.Brand == null || product.Brand.Id != SelectedBrand.Id))
                return false;
            if (SelectedCategory != null && SelectedCategory.Id != 0 && (product.Category == null || product.Category.Id != SelectedCategory.Id))
                return false;
            return true;
        }

        private void TextBox_TextChanged (object sender, TextChangedEventArgs e)
        {
            productsView.Refresh( );
        }

        private void ProductsList_MouseDoubleClick (object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddEditPage(product));
        }

        private void GoTags(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TagsList());
        }

        private void GoBrands(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BrandList());
        }

        private void GoCategories(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CategoryList());
        }

        private void AddEditProduct(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPage(product));
        }

        

       

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
