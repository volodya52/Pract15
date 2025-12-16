using Pract15.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract15.Service
{
    public class CategoryService
    {
        private readonly Pract15DatabaseContext _db=DbService.Instance.Context;
        public static ObservableCollection<Category> Categories { get; set; } = new();
        public void GetAll()
        {
            var categories=_db.Categories.ToList();
            Categories.Clear();
            foreach(var category in categories)
                Categories.Add(category);
        }

        public CategoryService()
        {
            GetAll();
        }

        public int Commit() => _db.SaveChanges();

        public void Add(Category category)
        {
            var _category = new Category
            {
                Name = category.Name
            };
            _db.Add<Category>(_category);
            Commit();
            Categories.Add(_category);
        }

        public void Remove(Category category)
        {
            _db.Remove<Category>(category);
            if(Commit()>0)
                if(Categories.Contains(category))
                    Categories.Remove(category);
        }


    }
}
