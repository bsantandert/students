using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Models
{
    public class Entity
    {
        private string _id;
        private DateTime _createdDate;
        private DateTime _lastModifiedDate;

        public Entity() { }

        public Entity(string id, DateTime createdDate, DateTime lastModifiedDate)
        {
            Id = id;
            CreatedDate = createdDate;
            LastModifiedDate = lastModifiedDate;
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        public DateTime LastModifiedDate
        {
            get { return _lastModifiedDate; }
            set { _lastModifiedDate = value; }
        }
    }
}
