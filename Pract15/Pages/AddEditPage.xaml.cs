using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage :Page, INotifyPropertyChanged
    {
        private Product _mainProduct;
        private ObservableCollection<Category> _categories;
        private ObservableCollection<Brand> _brands;
        private ObservableCollection<TagCheck> _tags;
        private string _selectedCategoryName;
        private string _selectedTagsText;
        public Product MainProduct
        {
            get => _mainProduct;
            set
            {
                if (_mainProduct != value)
                {
                    _mainProduct = value;
                    OnPropertyChanged( );
                    
                }
            }
        }

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged( );
                
            }
        }

        public ObservableCollection<Brand> Brands
        {
            get => _brands;
            set
            {
                _brands = value;
                OnPropertyChanged( );
            }
        }

        public ObservableCollection<TagCheck> Tags
        {
            get => _tags;
            set
            {
                _tags = value;
                OnPropertyChanged( );
            }
        }

        public string SelectedCategoryName
        {
            get => _selectedCategoryName;
            set
            {
                if (_selectedCategoryName != value)
                {
                    _selectedCategoryName = value;
                    OnPropertyChanged( );
                }
            }
        }

        public string SelectedTagsText
        {
            get => _selectedTagsText;
            set
            {
                if (_selectedTagsText != value)
                {
                    _selectedTagsText = value;
                    OnPropertyChanged( );
                }
            }
        }
        public class TagCheck :INotifyPropertyChanged
        {
            public int Id { get; set; }
            public string Name { get; set; }

            private bool _isSelected;
            public bool IsSelected
            {
                get => _isSelected;
                set
                {
                    if (_isSelected != value)
                    {
                        _isSelected = value;
                        OnPropertyChanged( );
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public AddEditPage (Product _product)
        {
            InitializeComponent( );
            MainProduct = new Product
            {
                Name = _product.Name,
                Description = _product.Description,
                Price = _product.Price,
                Stock = _product.Stock,
                Rating = _product.Rating,
                CreatedAt = _product.CreatedAt,
                BrandId = _product.BrandId,
                CategoryId = _product.CategoryId,
                Brand = _product.Brand,
                Tags = _product.Tags,
                Category = _product.Category

            };
            DataContext = this;
            Loaded += Page_Loaded;
        }

        private void LoadData ()
        {
            try
            {
                using var context = new Pract15DatabaseContext( );

                var categories = context.Categories.ToList( );
                Categories = new ObservableCollection<Category>(categories);

                var brands = context.Brands.ToList( );
                Brands = new ObservableCollection<Brand>(brands);

                var tags = context.Tags.ToList( );
                var tagChecks = tags.Select(t => new TagCheck
                {
                    Id = t.Id,
                    Name = t.Name,
                    IsSelected = false
                }).ToList( );

                Tags = new ObservableCollection<TagCheck>(tagChecks);

                if (MainProduct.Id > 0)
                {
                    var fullProduct = context.Products
                        .Include(p => p.Tags)
                        .FirstOrDefault(p => p.Id == MainProduct.Id);

                    if (fullProduct != null && fullProduct.Tags != null)
                    {
                        foreach (var tagCheck in Tags)
                        {
                            tagCheck.IsSelected = fullProduct.Tags.Any(t => t.Id == tagCheck.Id);
                        }
                    }
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back(object sender, EventArgs e)
        {
            NavigationService.GoBack( );
        }

        private void SaveProduct(object sender, EventArgs e)
        {
            using var context = new Pract15DatabaseContext( );

            if (MainProduct.Id == 0)
            {
                var selectedTags = Tags
                    .Where(t => t.IsSelected)
                    .Select(t => context.Tags.Find(t.Id))
                    .Where(t => t != null)
                    .ToList();
                var newProduct = new Product
                {
                    Name = MainProduct.Name,
                    Description = MainProduct.Description,
                    Price = MainProduct.Price,
                    Stock = MainProduct.Stock,
                    Rating = MainProduct.Rating,
                    CreatedAt = MainProduct.CreatedAt,
                    BrandId = MainProduct.BrandId,
                    CategoryId = MainProduct.CategoryId,
                    Tags = selectedTags
                };
                MainProduct.Tags = selectedTags;
                context.Products.Add(newProduct);

                
                
            }
            else
            {
                var existingProduct = context.Products
                    .Include(p => p.Tags)
                    .Include(p => p.Brand)
                    .Include(p => p.Category) 
                    .FirstOrDefault(p => p.Id == MainProduct.Id);

                if (existingProduct != null)
                {
                    existingProduct.Name = MainProduct.Name;
                    existingProduct.Description = MainProduct.Description;
                    existingProduct.Price = MainProduct.Price;
                    existingProduct.Stock = MainProduct.Stock;
                    existingProduct.Rating = MainProduct.Rating;
                    existingProduct.CreatedAt = MainProduct.CreatedAt;
                    existingProduct.CategoryId = MainProduct.CategoryId;
                    existingProduct.BrandId = MainProduct.BrandId;

                    var selectedTags = Tags
                        .Where(t => t.IsSelected)
                        .Select(t => context.Tags.Find(t.Id))
                        .Where(t => t != null)
                        .ToList();

                    existingProduct.Tags.Clear();
                    foreach (var tag in selectedTags)
                    {
                        existingProduct.Tags.Add(tag);
                    }
                }
            }

            context.SaveChanges( );
            MessageBox.Show("Товар успешно сохранен", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);

            NavigationService.GoBack( );

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(MainProduct) && MainProduct != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Page_Loaded (object sender, RoutedEventArgs e)
        {
            LoadData( );
        }
    }
}
