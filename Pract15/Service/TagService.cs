using Pract15.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract15.Service
{
    public class TagService
    {
        private readonly Pract15DatabaseContext _db=DbService.Instance.Context;

        public static ObservableCollection<Tag> Tags { get; set; } = new();

        public TagService()
        {
            GetAll();
        }

        public void GetAll()
        {
            var _tags=_db.Tags.ToList();
            Tags.Clear();
            foreach (var tag in _tags)
                Tags.Add(tag);
        }

        public void Add(Tag tag)
        {
            var _tag = new Tag
            {
                Name = tag.Name
            };
            _db.Add<Tag>(_tag);
            Commit();
            Tags.Add(tag);
        }

        public int Commit()=>_db.SaveChanges();

        public void Remove(Tag tag)
        {
            _db.Remove<Tag>(tag);
            if(Commit()>0)
                if(Tags.Contains(tag))
                    Tags.Remove(tag);
        }
    }
}
